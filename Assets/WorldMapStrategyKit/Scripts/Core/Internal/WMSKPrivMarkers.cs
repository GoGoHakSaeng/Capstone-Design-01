// World Map Strategy Kit for Unity - Main Script
// (C) Kronnect Technologies SL
// Don't modify this script - changes could be lost if you upgrade to a more recent version of WMSK

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WorldMapStrategyKit.Poly2Tri;


namespace WorldMapStrategyKit {

    public partial class WMSK : MonoBehaviour {

        enum DragState {
            None,
            CanStartDrag,
            Dragging
        }

        struct MarkerContext {
            public bool leftButtonPressed;
            public bool rightButtonPressed;
            public bool leftButtonReleased;
            public bool rightButtonReleased;
            public MarkerClickHandler draggingObject;
            public DragState dragState;
            public Vector3 dragStartClick;
            public Vector3 dragStartPosition;
            public bool isDragging => dragState == DragState.Dragging;
        }

        static List<MarkerClickHandler> interactiveMarkers = new List<MarkerClickHandler>();

        public static void RegisterInteractiveMarker(MarkerClickHandler handler) {
            interactiveMarkers.Add(handler);
        }

        public static void UnregisterInteractiveMarker(MarkerClickHandler handler) {
            interactiveMarkers.Remove(handler);
        }

        MarkerContext markerContext;

        int lastMarkerOverIndex;
        bool markerDragging => markerContext.dragState != DragState.None;
        Vector2 lastMarkerLocalPositionCheck;

        void CheckInteractiveMarkers() {

            int markersCount = interactiveMarkers.Count;
            if (markersCount == 0) return;

            markerContext.leftButtonPressed = input.GetMouseButtonDown(0);
            markerContext.rightButtonPressed = input.GetMouseButtonDown(1);
            markerContext.leftButtonReleased = input.GetMouseButtonUp(0);
            markerContext.rightButtonReleased = input.GetMouseButtonUp(1);
            bool checkEnterExit = (OnMarkerMouseEnter != null || OnMarkerMouseExit != null) && lastMouseMapLocalHitPos != lastMarkerLocalPositionCheck;

            if (checkEnterExit || markerContext.leftButtonPressed || markerContext.rightButtonPressed || markerContext.leftButtonReleased || markerContext.rightButtonReleased) {

                lastMarkerLocalPositionCheck = lastMouseMapLocalHitPos;
                Vector3 lastMarkerLocalPositionCheckWS = transform.TransformPoint(lastMouseMapLocalHitPos);

                for (int k = 0; k < markersCount; k++) {

                    int i = (k + lastMarkerOverIndex) % markersCount;
                    MarkerClickHandler handler = interactiveMarkers[i];
                    if (handler == null || !handler.isActiveAndEnabled) continue;

                    handler.isMouseOver = TestCursorOverMarker(handler, lastMarkerLocalPositionCheckWS);
                    if (handler.isMouseOver) {
                        if (markerContext.leftButtonPressed) {
                            if (handler.captureClickEvents) { FlyToCancel(); IgnoreClickEvents(); }
                            if (OnMarkerMouseDown != null) OnMarkerMouseDown(handler, 0);
                            if (handler.allowDrag && markerContext.draggingObject == null) {
                                markerContext.draggingObject = handler;
                                markerContext.dragStartClick = transform.TransformPoint(cursorLocation);
                                markerContext.dragState = DragState.CanStartDrag;
                            }
                        }
                        if (markerContext.rightButtonPressed) {
                            if (handler.captureClickEvents) { FlyToCancel(); IgnoreClickEvents(); }
                            if (OnMarkerMouseDown != null) OnMarkerMouseDown(handler, 1);
                        }
                        if (markerContext.rightButtonReleased) {
                            if (handler.captureClickEvents) { FlyToCancel(); IgnoreClickEvents(); }
                            if (OnMarkerMouseUp != null) OnMarkerMouseUp(handler, 1);
                        }
                        if (!handler.wasInside) {
                            if (OnMarkerMouseEnter != null) OnMarkerMouseEnter(handler);
                        }
                        if (markerContext.leftButtonReleased && markerContext.dragState != DragState.Dragging) {
                            if (handler.captureClickEvents) { FlyToCancel(); IgnoreClickEvents(); }
                            if (OnMarkerMouseUp != null) OnMarkerMouseUp(handler, 0);
                        }
                    } else {
                        if (handler.wasInside) {
                            if (OnMarkerMouseExit != null) OnMarkerMouseExit(handler);
                        }
                    }

                    handler.wasInside = handler.isMouseOver;
                    if (handler.isMouseOver) {
                        lastMarkerOverIndex = i;
                        break;
                    }
                }
            }

            // drag handling
            if (markerContext.dragState != DragState.None) {
                if (markerContext.draggingObject == null) {
                    AbortDrag();
                } else {
                    Vector3 delta = transform.TransformPoint(cursorLocation) - markerContext.dragStartClick;
                    switch (markerContext.dragState) {
                        case DragState.CanStartDrag:
                            if (markerContext.leftButtonReleased) {
                                AbortDrag();
                            } else if (delta.sqrMagnitude > 0) {
                                StartDrag();
                            }
                            break;
                        case DragState.Dragging:
                            if (markerContext.leftButtonReleased) {
                                if (markerContext.draggingObject.captureClickEvents) { FlyToCancel(); IgnoreClickEvents(); }
                                EndDrag();
                            } else {
                                markerContext.draggingObject.transform.position = markerContext.dragStartPosition + delta;
                            }
                            break;
                    }
                }
            }
        }

        bool TestCursorOverMarker(MarkerClickHandler handler, Vector3 worldPosition) {

            if (handler == null) return false;

            Bounds bounds = handler.markerRenderer.bounds;

            if (handler.transform.parent == null) {
                return bounds.Contains(worldPosition);
            }

            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            Transform t = transform;
            min = t.InverseTransformPoint(min);
            max = t.InverseTransformPoint(max);

            return min.x <= lastMouseMapLocalHitPos.x && max.x >= lastMouseMapLocalHitPos.x &&
                   min.y <= lastMouseMapLocalHitPos.y && max.y >= lastMouseMapLocalHitPos.y;

        }

        void AbortDrag() {
            markerContext.draggingObject = null;
            markerContext.dragState = DragState.None;
        }

        void StartDrag() {
            CancelMapDrag();
            markerContext.dragStartPosition = markerContext.draggingObject.transform.position;
            markerContext.dragState = DragState.Dragging;
            if (OnMarkerDragStart != null) OnMarkerDragStart(markerContext.draggingObject);
        }

        void EndDrag() {
            markerContext.dragState = DragState.None;
            if (OnMarkerDragEnd != null) OnMarkerDragEnd(markerContext.draggingObject);
            markerContext.draggingObject = null;
        }

        bool CancelDrag(bool returnToStartPosition) {
            if (markerContext.draggingObject == null) return false;
            if (returnToStartPosition) {
                markerContext.draggingObject.transform.position = markerContext.dragStartPosition;
            }
            AbortDrag();
            return true;
        }

    }

}
using UnityEngine;
using System;


namespace WorldMapStrategyKit {


    public class MarkerClickHandler : MonoBehaviour {

        public bool captureClickEvents = true;
        public bool allowDrag = true;

        [NonSerialized]
        public bool isMouseOver;

        [NonSerialized]
        public bool wasInside;

        [NonSerialized]
        public Renderer markerRenderer;

        Rect localRect;

        void Start() {
            markerRenderer = GetComponentInChildren<Renderer>();
            WMSK.RegisterInteractiveMarker(this);
        }

        private void OnDestroy() {
            WMSK.UnregisterInteractiveMarker(this);
        }

    }

}


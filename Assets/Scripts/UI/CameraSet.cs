using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class CameraSet : MonoBehaviour
{
    WMSK map;
    public RectTransform panel;
    public float panelScaling = 1f;
    GUIStyle labelStyle;

    void Start()
    {
        map = WMSK.instance;
        labelStyle = new GUIStyle();
        labelStyle.alignment = TextAnchor.MiddleLeft;
        labelStyle.normal.textColor = Color.white;
        GUIResizer.Init(800, 200);
        map.windowRect = new Rect(0.315f, 0.18f, 0.1f, 0.1f);
        map.SetZoomLevel(0.1f);
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 500, 30), "Test Text", labelStyle);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class CameraSeeMapSetting : MonoBehaviour
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

        map.windowRect = new Rect(0.305f, 0.165f, 0.1f, 0.1f); // pos x, pos y, length, height 
        map.SetZoomLevel(0.1f);
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 500, 30), "", labelStyle);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

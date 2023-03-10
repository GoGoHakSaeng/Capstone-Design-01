using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

public class asdfds : MonoBehaviour
{
    WMSK map;

    void Start()
    {
        map = WMSK.instance;
        map.windowRect = new Rect(0.3f, 0.175f, 0.3f, 0.3f);
        map.SetZoomLevel(0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

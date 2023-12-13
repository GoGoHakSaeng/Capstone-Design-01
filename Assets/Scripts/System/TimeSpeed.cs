using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using WorldMapStrategyKit;

public class TimeSpeed : MonoBehaviour
{
    public float Speed;
    public TMP_Text ButtonNum;

    public void OnSpeedClick()
    {
        WMSK.instance.timeSpeed = Speed;
    }
}

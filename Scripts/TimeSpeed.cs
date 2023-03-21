<<<<<<< HEAD
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
        Debug.Log(Speed = Convert.ToSingle(ButtonNum.text));
        WMSK.instance.timeSpeed = Speed;
    }
}
=======
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
        Debug.Log(Speed = Convert.ToSingle(ButtonNum.text));
        WMSK.instance.timeSpeed = Speed;
    }
}
>>>>>>> 68f8efb2a846b970304649f1d187e665da684a14

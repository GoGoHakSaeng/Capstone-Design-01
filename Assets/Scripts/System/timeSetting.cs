using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WorldMapStrategyKit;
using TMPro;

public class timeSetting : MonoBehaviour
{
    string ButtonText;
    private bool check;
    public TMP_Text button_Text;

    public void Start()
    {
        ButtonText = "정지";
        check = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            TimeSetting();
        }
    }

    public void OnClick()
    {
        TimeSetting();
    }

    void TimeSetting()
    {
        if (check == false)
        {
            ButtonText = "계속";
            check = true;
        }
        else
        {
            ButtonText = "정지";
            check = false;
        }
        button_Text.text = ButtonText;
        WMSK.instance.paused = check;
    }
}

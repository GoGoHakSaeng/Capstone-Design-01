<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WorldMapStrategyKit;
using TMPro;

public class timeSetting : MonoBehaviour
{
    public string ButtonText;
    private bool check;
    public TMP_Text button_Text;

    public void Start()
    {
        ButtonText = "Stop";
        check = false;
    }

    public void OnClick()
    {
        if (check == false)
        {
            ButtonText = "Start";
            check = true;
        } else
        {
            ButtonText = "Stop";
            check = false;
        }
        button_Text.text = ButtonText;
        WMSK.instance.paused = check;
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WorldMapStrategyKit;
using TMPro;

public class timeSetting : MonoBehaviour
{
    public string ButtonText;
    private bool check;
    public TMP_Text button_Text;

    public void Start()
    {
        ButtonText = "Stop";
        check = false;
    }

    public void OnClick()
    {
        if (check == false)
        {
            ButtonText = "Start";
            check = true;
        } else
        {
            ButtonText = "Stop";
            check = false;
        }
        button_Text.text = ButtonText;
        WMSK.instance.paused = check;
    }
}
>>>>>>> 68f8efb2a846b970304649f1d187e665da684a14

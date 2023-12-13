using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CountryInfo_Flagbutton : MonoBehaviour
{
    public GameObject FlagButton_Panel;
    bool isFlagPaenlOn = false;

    private void Start()
    {
    }

    public void onFlagClick()
    {
        if (isFlagPaenlOn == false)
        {
            FlagButton_Panel.SetActive(true);
            isFlagPaenlOn = true;
        } else
        {
            FlagButton_Panel.SetActive(false);
            isFlagPaenlOn = false;
        }

        
    }

}

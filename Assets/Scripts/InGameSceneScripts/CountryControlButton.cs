using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryControlButton : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] otherPanel;
    private int count;
    

    // Start is called before the first frame update
    void Start()
    {
        count = otherPanel.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBtnClick()
    {
        if (panel.activeSelf == false)
        {
            for (int i = 0; i < count; i++)
            {
                if(otherPanel[i].activeSelf == true)
                {
                    otherPanel[i].SetActive(false);
                }
            }
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}

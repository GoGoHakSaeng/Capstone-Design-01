using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject ESCMenu;

    public GameObject[] CountryControlPanels;
    
    private bool isPanelOn;

    // Start is called before the first frame update
    void Start()
    {
        isPanelOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 메뉴
        {
            isPanelOn = false;
            PanelCheck();
            if (ESCMenu.activeSelf == false)
            {
                if (isPanelOn == true)
                {
                    PanelTurnOff();
                } else
                {
                    Pause();
                }
                
            }
            else
            {
                Resume();
            }
        }
        if (Input.GetMouseButtonDown(1)) // If this code make error when Mouse right button down then delete this part
        {
            isPanelOn = false;
            PanelCheck();
            if (isPanelOn == true)
            {
                PanelTurnOff();
            }

        }
    }
    void Resume()
    {
        ESCMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        ESCMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void PanelCheck()
    {
        for (int i=0; i<CountryControlPanels.Length; i++)
        {
            if (CountryControlPanels[i].activeSelf == true)
            {
                isPanelOn = true;
            }
        }
    }

    void PanelTurnOff()
    {
        for (int i = 0; i < CountryControlPanels.Length; i++)
        {
            if (CountryControlPanels[i].activeSelf == true)
            {
                CountryControlPanels[i].SetActive(false);
            }
        }
    }
}

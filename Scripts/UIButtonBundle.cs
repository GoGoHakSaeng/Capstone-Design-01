using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class UIButtonBundle : MonoBehaviour
{
    [SerializeField] public GameObject[] panels;
    [SerializeField] public Button[] buttons;

    private int currentPanelIndex = -1;

    public void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    public void OnButtonClick(int index)
    {
        if (currentPanelIndex == index)
        {
            return;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            if(i == index)
            {
                panels[i].SetActive(true);
            } else
            {
                panels[i].SetActive(false);
            }
        }

        currentPanelIndex = index;
    }

}

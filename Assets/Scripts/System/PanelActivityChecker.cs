using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YemaekHan;

public class PanelActivityChecker : MonoBehaviour
{
    public static PanelActivityChecker instance;
    public GameObject[] panels;
    public bool isArmyPanelOn = false;

    private int currentIndex = -1;
    private List<bool> panelsActivity = new List<bool>();

    private void Awake()
    {
        if (PanelActivityChecker.instance == null)
        {
            PanelActivityChecker.instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i<panels.Length; i++)
        {
            panelsActivity.Add(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < panels.Length; i++) // 지정한 패널 중에서
        {
            if (panels[i].activeSelf == true) // 선택한 패널이 활성화 되면
            {
                currentIndex = i; // 패널 인덱스를 저장한 후
                if (currentIndex == 3)
                {
                    isArmyPanelOn = true;
                } else
                {
                    isArmyPanelOn = false;
                }
                
                for (int j = 0; j < panelsActivity.Count; j++)
                {
                    if (j == currentIndex) // 해당 패널의 bool값을 true로 전환하고
                    {
                        panelsActivity[currentIndex] = true;
                    } else // 해당 패널이 아닐 시 패널을 비활성화, bool값을 false로 지정
                    {
                        panels[j].SetActive(false);
                        panelsActivity[j] = false;
                    }
                }
            } else
            {
                currentIndex = -1; // 창을 닫으면 인덱스 초기화
                isArmyPanelOn = false;
            }
        }
    }

    public void updateActivity(int panelNumber)
    {
        currentIndex = panelNumber;
        for (int i = 0; i < panelsActivity.Count; i++)
        {
            if (i == currentIndex) // 받은 인덱스의 패널 bool값을 true로 전환하고
            {
                panelsActivity[currentIndex] = true;
            }
            else // 해당 패널이 아닐 시 패널을 비활성화, bool값을 false로 지정
            {
                panels[i].SetActive(false);
                panelsActivity[i] = false;
            }
        }
    }
}

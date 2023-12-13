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
        for (int i = 0; i < panels.Length; i++) // ������ �г� �߿���
        {
            if (panels[i].activeSelf == true) // ������ �г��� Ȱ��ȭ �Ǹ�
            {
                currentIndex = i; // �г� �ε����� ������ ��
                if (currentIndex == 3)
                {
                    isArmyPanelOn = true;
                } else
                {
                    isArmyPanelOn = false;
                }
                
                for (int j = 0; j < panelsActivity.Count; j++)
                {
                    if (j == currentIndex) // �ش� �г��� bool���� true�� ��ȯ�ϰ�
                    {
                        panelsActivity[currentIndex] = true;
                    } else // �ش� �г��� �ƴ� �� �г��� ��Ȱ��ȭ, bool���� false�� ����
                    {
                        panels[j].SetActive(false);
                        panelsActivity[j] = false;
                    }
                }
            } else
            {
                currentIndex = -1; // â�� ������ �ε��� �ʱ�ȭ
                isArmyPanelOn = false;
            }
        }
    }

    public void updateActivity(int panelNumber)
    {
        currentIndex = panelNumber;
        for (int i = 0; i < panelsActivity.Count; i++)
        {
            if (i == currentIndex) // ���� �ε����� �г� bool���� true�� ��ȯ�ϰ�
            {
                panelsActivity[currentIndex] = true;
            }
            else // �ش� �г��� �ƴ� �� �г��� ��Ȱ��ȭ, bool���� false�� ����
            {
                panels[i].SetActive(false);
                panelsActivity[i] = false;
            }
        }
    }
}

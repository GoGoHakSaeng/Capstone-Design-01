using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

public class AttributeSet : MonoBehaviour
{
    WMSK map;

    int id;
    string countryName;
    Country country;
    City capital;
    int gold;
    int population;
    int popularSentiment;
    bool isPlayer;

    void Start()
    {
        map = WMSK.instance;

        map.OnCountryClick += (int countryIndex, int regionIndex, int buttonIndex) =>
        {
            setCountryAttr(countryIndex);
            if (capital == null)
            {
                Debug.Log("������ �������� �ʾҽ��ϴ�.");
            } else
            {
                printLog();
            }
            
        };
        

        
    }

    void setCountryAttr(int Index)
    {
        id = Index;
        country = map.GetCountry(id);
        countryName = country.name;
        capital = map.GetCountryCapital(id);
        gold = country.attrib["gold"];
        population = country.attrib["population"];
        popularSentiment = country.attrib["popular Sentiment"];
        isPlayer = country.attrib["isPlayer"];
    }

    void printLog()
    {
        Debug.Log("�÷��̾ " + countryName + "(��)�� ������.");
        Debug.Log("���� �ε��� : " + id);
        Debug.Log("���� ���� : " + capital.name);
        Debug.Log("��� : " + gold);
        Debug.Log("�η� : " + population);
        Debug.Log("������ : " + popularSentiment);
    }
}

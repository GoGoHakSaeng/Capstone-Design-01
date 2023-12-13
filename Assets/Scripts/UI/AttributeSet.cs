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
                Debug.Log("수도가 지정되지 않았습니다.");
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
        Debug.Log("플레이어가 " + countryName + "(을)를 선택함.");
        Debug.Log("국가 인덱스 : " + id);
        Debug.Log("국가 수도 : " + capital.name);
        Debug.Log("골드 : " + gold);
        Debug.Log("인력 : " + population);
        Debug.Log("안정도 : " + popularSentiment);
    }
}

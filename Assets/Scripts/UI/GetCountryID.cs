using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class GetCountryID : MonoBehaviour
{
    int UserIndex;
    WMSK map;
    public GameObject[] GetDontDestroyOnLoadObjects() // 게임 오브젝트 받아오는 함수
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            Object.DontDestroyOnLoad(temp);
            UnityEngine.SceneManagement.Scene dontDestroyOnLoad = temp.scene;
            Object.DestroyImmediate(temp);
            temp = null;

            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if (temp != null)
            {
                Object.DestroyImmediate(temp);
            }
        }
    }

    void Start()
    {
        map = WMSK.instance;
        int index;
        if ( GetDontDestroyOnLoadObjects().Length > 0 )
        {
            GameObject[] playerCountry = GetDontDestroyOnLoadObjects(); // 게임 오브젝트 받음
            index = playerCountry[0].GetComponent<CountryChange>().countryID;
            Destroy(playerCountry[0]); // DontDestoryOnLoad 삭제
        } else
        {
            index = 0;
        }

        CountrySet.setUserCountry(index);
        TargetCountry.setTargetCountry(CountrySet.getUserCountry);
        GetComponent<ChangeCountryImage>().OnStartScene(index);
        UserIndex = index;
    }

    public List<int> GetAiFactions()
    {
        List<int> AIFactions = new List<int>();
        for (int i = 0; i<map.countries.Length; i++)
        {
            if (i != UserIndex)
            {
                AIFactions.Add(i);
            }
        }

        return AIFactions;
    }

    public int GetUserID()
    {
        return UserIndex;
    }
}
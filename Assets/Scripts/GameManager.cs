using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class GameManager : MonoBehaviour
{
    WMSK map;

    GetCountryID getCountryID;

    public List<City> KoguryeoCities = new List<City>();
    public List<City> BaekjeCities = new List<City>();
    public List<City> GayaCities = new List<City>();
    public List<City> SilaCities = new List<City>();

    private void Start()
    {
        map = WMSK.instance;
        getCountryID = FindAnyObjectByType<GetCountryID>();
    }

    private void Update()
    {

    }

    public List<City> getUserCities()
    {
        switch (getCountryID.GetUserID())
        {
            case 0:
                return KoguryeoCities;
            case 1:
                return BaekjeCities;
            case 2:
                return GayaCities;
            case 3:
                return SilaCities;
            default:
                return KoguryeoCities;
        }
    }
}

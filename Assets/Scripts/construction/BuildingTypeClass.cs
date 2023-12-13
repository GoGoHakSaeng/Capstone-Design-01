using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class BuildingTypeClass : MonoBehaviour
{
    // Building Type
    public string Farm = "farm";
    public string Barrack = "barrack";
    public string TradingSpot = "trading";
    // Price
    public int FarmPrice = 1000;
    public int BarrackPrice = 1000;
    public int TradingPrice = 1000;
    // Turn Gold
    public int FarmTurnGold = 100;
    public int BarrackTurnGold = -100;
    public int TradingTurnGold = 200;
    // Other
    public int ConsTime { get; set; } = 10;

    GameManager gameManager;
    List<City> UserCities = new List<City>();
    Dictionary<string, string> Buildings;

    void setUserCities()
    {
        UserCities.Clear();
        GetCountryID getCountryID = FindAnyObjectByType<GetCountryID>();
        foreach (City city in WMSK.instance.cities)
        {
            if (city.countryIndex == getCountryID.GetUserID())
            {
                UserCities.Add(city);
            }
        }
    }

    public void CheckAllCitiesAndGetTurnGold() // 모든 도시 검사해서 건물 수에 맞는 턴 골드 계산
    {
        gameManager = FindAnyObjectByType<GameManager>();
        setUserCities();
        int BuildingMaintenaceGold = 0;
        foreach (City city in UserCities)
        {
            Buildings = city.attrib["construction"].ToDictionary();
            foreach (KeyValuePair<string, string> kvp in Buildings)
            {
                switch (kvp.Value)
                {
                    case "farm":
                        BuildingMaintenaceGold += FarmTurnGold;
                        continue;
                    case "barrack":
                        BuildingMaintenaceGold += BarrackTurnGold;
                        continue;
                    case "trading":
                        BuildingMaintenaceGold += TradingTurnGold;
                        continue;
                }
            }
        }
        GoldSystem goldSystem = FindAnyObjectByType<GoldSystem>();
        goldSystem.BuildingMaintenanceGoldUpdate(BuildingMaintenaceGold);
    }
    public int BuildingTurnGold(string typeName)
    {
        switch (typeName)
        {
            case "farm":
                return FarmTurnGold;
            case "barrack":
                return BarrackTurnGold;
            case "trading":
                return TradingTurnGold;
            default:
                return 0;
        }
    }

    public int BuildingPrice(string typeName)
    {
        switch (typeName)
        {
            case "farm":
                return FarmPrice;
            case "barrack":
                return BarrackPrice;
            case "trading":
                return TradingPrice;
            default:
                return 0;
        }
    }

    public int Contruction_Time(int value)
    {
        ConsTime = value;
        return ConsTime;
    }

    // Effect Of CountryChoice
    public void ConstructionTime_Effect(int EffectConsTime)
    {
        ConsTime = EffectConsTime;   
    }

    /*
    void BarrackEffect(Vector2 ArmyPosition)
    {
        gameManager = FindAnyObjectByType<GameManager>();
        List<City> OnCity;
        int BarrackBuff = 0;
        foreach (City city in UserCities)
        {
            Buildings = city.attrib["construction"].ToDictionary();
            foreach (KeyValuePair<string, string> kvp in Buildings)
            {
                switch (kvp.Value)
                {
                    case "farm":
                        continue;
                    case "barrack":
                        BarrackBuff += 5;
                        continue;
                    case "trading":
                        continue;
                }
            }
        }
    }*/
}

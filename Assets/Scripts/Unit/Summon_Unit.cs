using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using WorldMapStrategyKit;
using YemaekHan;

public class Summon_Unit : MonoBehaviour
{
    private WMSK map;

    public Button SummonButton;
    public GameObject[] UnitManager;

    private GameObjectAnimator unit;
    private GameObject newUnit;
    private Transform TargetList;

    private int cityIndex;
    private int countryIndex;
    private string countryName;
    private int currentProvinceIndex;
    private bool isHaveBarrack; // 도시에 barrack 건물이 있어야만 유닛 소환 가능

    private static int unitID;

    private SetArmyTypeAndSize setArmyTypeAndSize;
    private GoldSystem goldSystem;
    private ManpowerSystem manpowerSystem;
    
    public void Start()
    {
        map = WMSK.instance;
        setArmyTypeAndSize = FindAnyObjectByType<SetArmyTypeAndSize>();
        goldSystem = FindObjectOfType<GoldSystem>();
        manpowerSystem = FindAnyObjectByType<ManpowerSystem>();
        unitID = 1;

        countryIndex = CountrySet.getUserCountryIndex;
    }

    private void Update()
    {
        SummonButtonLock();
    }

    public void SummonUnit()
    {
        // 국가를 확인하고 해당 진영에 유닛생성
        cityIndex = GameObject.Find("Select City").GetComponent<SelectCity>().selectedCity;
        string country = CountrySet.getUserCountryName;

        Army newArmy = new Army();

        newArmy.ID = unitID++ ;
        newArmy.InfantrySize = setArmyTypeAndSize.InfantrySize;
        newArmy.ArcherSize = setArmyTypeAndSize.ArcherSize;
        newArmy.CavalrySize = setArmyTypeAndSize.CavalrySize;
        newArmy.TotalSize = setArmyTypeAndSize.TotalArmySize;
        goldSystem.ArmyMaintenance += newArmy.TotalPrice(); // Gold
        manpowerSystem.CalculateManpower(newArmy.TotalSize);

        SetList(country);
        countryName = country;

        newUnit = Instantiate(Resources.Load<GameObject>("Prefabs/Pawn_User"), transform.position, Quaternion.identity, TargetList);
        UnitStat unitStatComponent = newUnit.GetComponent<UnitStat>(); // UnitStat으로 정보 넘기기
        unitStatComponent.army = newArmy;

        SetFaction(country);
        
        
        UnitSelect armyComponent = newUnit.GetComponent<UnitSelect>(); // UnitSelect로 정보 넘기기

        if (armyComponent != null)
        {
            armyComponent.setArmy(newArmy); // 군대 정보 설정
        }

        UnitPlacement(newUnit);

        UnitPosition.instance.setUnitPosition(countryName, newArmy.ID, currentProvinceIndex);
    }

    public void UnitPlacement(GameObject newUnit)
    {
        Vector2 cityPosition = map.cities[cityIndex].unity2DLocation;
        unit = newUnit.GetComponent<UnitStat>().unit;

        if (unit != null)
            DestroyImmediate(unit);

        unit = newUnit.WMSK_MoveTo(cityPosition);
        unit.type = (int)UNIT_TYPE.AIR;
        unit.autoRotation = true;
        unit.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;

        Vector2 currentVector = unit.currentMap2DLocation;
        currentProvinceIndex = map.GetProvinceIndex(currentVector);

        newUnit.GetComponent<UnitStat>().unit = unit;

    }

    private void SummonButtonLock()
    {
        int selectedCity = -1;
        selectedCity = GameObject.Find("Select City").GetComponent<SelectCity>().selectedCity;
        string CityName = GameObject.Find("Select City").GetComponent<SelectCity>().CityName;
        int TotalArmySize = GameObject.Find("ArmyTypeAndSize").GetComponent<SetArmyTypeAndSize>().TotalArmySize;

        if (selectedCity != -1)
        {
            SummonOnBarrack(selectedCity);
        }

        if (CityName == null || selectedCity <= -1 || TotalArmySize <= 0 || TotalArmySize > manpowerSystem.GetCurrentManpower() || isHaveBarrack == false)
        {
            SummonButton.interactable = false; // 조건이 맞지 않으면 유닛 배치 불가
        }
        else
        {

            SummonButton.interactable = true;
        }
    }

    private void SummonOnBarrack(int selectedCityIndex)
    {
        isHaveBarrack = false;
        Dictionary<string, string> Buildings = map.cities[selectedCityIndex].attrib["construction"].ToDictionary();
        foreach (KeyValuePair<string, string> kvp in Buildings)
        {
            if (kvp.Value == "barrack")
            {
                isHaveBarrack = true;
                break;
            } else
            {
                isHaveBarrack = false;
            }
        }
    }

    private void SetFaction(string country)
    {
        switch (country)
        {
            case "ko":
                newUnit.GetComponent<UnitStat>().faction = Faction.Koguryeo;
                break;
            case "ba":
                newUnit.GetComponent<UnitStat>().faction = Faction.Baekje;
                break;
            case "si":
                newUnit.GetComponent<UnitStat>().faction = Faction.Sila;
                break;
            case "ga":
                newUnit.GetComponent<UnitStat>().faction = Faction.Gaya;
                break;
        }
    }

    private void SetList(string country)
    {
        switch (country)
        {
            case "ko":
                TargetList = UnitManager[0].transform ;
                break;
            case "ba":
                TargetList = UnitManager[1].transform;
                break;
            case "si":
                TargetList = UnitManager[2].transform;
                break;
            case "ga":
                TargetList = UnitManager[3].transform;
                break;
        }
    }
}
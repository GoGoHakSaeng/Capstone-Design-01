using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;
using static UnityEngine.UI.CanvasScaler;

public class EachAIManager : MonoBehaviour
{
    GameManager gameManager;

    public int Faction_ID { get; set; } // Do Not Change This Value
    Faction faction;

    List<City> AI_CityList = new List<City>();

    WMSK map;

    int RandomSeed;
    void getRandomSeed() // Change Random Seed
    {
        RandomSeed = (int)System.DateTime.Now.Ticks + Faction_ID;
        UnityEngine.Random.InitState(RandomSeed);
    }

    private int running_Time;
    public int Running_Time // Detect value change
    {
        get { return running_Time; }
        set
        {
            if (running_Time != value)
            {
                running_Time = value;
                // Script
                getRandomSeed();

                time++;
                RandomEventMaker(5);

                if (time == 3)
                {
                    AIArmySizeLimit++; // Add manpower per Turn, This Value that add per turn is Temporary
                }
            }
        }
    }
    int time = 0;

    GameObject AIArmyUnit;
    GameObjectAnimator unitAnimator;

    public Transform UnitManager;

    public bool isDuringWar = false; // 전쟁 시 true, 평화 시 false // 기본 값 false

    // Start is called before the first frame update
    void Start()
    {
        map = WMSK.instance;

        gameManager = FindAnyObjectByType<GameManager>();

        setFaction();
        getAICountryCity();
        getAIManpower();
    }

    void Update()
    {
        Running_Time = DisplayTime.instance.realTime;
    }

    void setFaction()
    {
        switch (Faction_ID)
        {
            case 0:
                faction = Faction.Koguryeo;
                break;
            case 1:
                faction = Faction.Baekje;
                break;
            case 2:
                faction = Faction.Gaya;
                break;
            case 3:
                faction = Faction.Sila;
                break;
        }
    }


    void getAICountryCity()
    {
        AI_CityList.Clear();

        foreach (City city in map.cities)
        {
            if (city.countryIndex == Faction_ID)
            {
                AI_CityList.Add(city);
            }
        }
    }

    void RandomEventMaker(int TurnTime)
    {
        int EventNum = UnityEngine.Random.Range(0, 3);

        if (TurnTime == time)
        {
            switch (EventNum)
            {
                case 0:
                    Debug.Log("AI 건설 : " + Faction_ID + " 국가");
                    AIConstruction();
                    break;
                case 1:
                    Debug.Log("AI 군대 : " + Faction_ID + " 국가");
                    AIArmySummon();
                    break;
                case 2:
                    Debug.Log("AI 패스 : " + Faction_ID + " 국가");
                    break;
            }
            time = 0;
        }
    }

    // Randomly Select AI Country's City
    City getRandomCity()
    {
        getAICountryCity();
        if (AI_CityList != null && AI_CityList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, AI_CityList.Count);
            return AI_CityList[randomIndex];
        } else { return null;  }
    }

    // AI construction
    void AIConstruction()
    {
        JSONObject AICityJSON = getRandomCity().attrib["construction"];

        int count = AICityJSON.Count;
        string slotname = "slot" + count.ToString();
        AICityJSON.AddField(slotname, AIBuildingType());

        Debug.Log(AICityJSON);
    }

    string AIBuildingType()
    {
        int BuildTypeNum = UnityEngine.Random.Range(0, 3);
        switch (BuildTypeNum)
        {
            case 0:
                return "farm";
            case 1:
                return "barrack";
            case 2:
                return "trading";
            default:
                return "";
        }
    }

    // AI Army
    int AIArmyMaxCount;
    int AIArmyCount = 0; // AI 군대 개수 생성 시 ++ / 제거 시 --
    int AIArmyID = 1;
    void AIArmySummon()
    {
        if (AIArmySizeLimit > 0 && AIArmyCount < AIArmyMaxCount)
        {
            Army AIArmy = new Army();

            AIArmy.InfantrySize = UnityEngine.Random.Range(0, AIArmySizeLimit / 10 * 6); // 군대 인력의 60%
            AIArmy.ArcherSize = UnityEngine.Random.Range(0, AIArmySizeLimit / 10 * 2); // 군대 인력의 20%
            AIArmy.CavalrySize = UnityEngine.Random.Range(0, AIArmySizeLimit / 10 * 2); // 군대 인력의 20%
            AIArmy.TotalSize = AIArmy.InfantrySize + AIArmy.ArcherSize + AIArmy.CavalrySize;
            AIArmySizeLimit -= AIArmy.TotalSize;

            if (AIArmy.TotalSize > 0)
            {
                AIArmy.ID = AIArmyID++;

                AIArmyUnit = Instantiate(Resources.Load<GameObject>("Prefabs/Pawn_AI"), transform.position, Quaternion.identity, UnitManager);
                UnitStat unitStatComponent = AIArmyUnit.GetComponent<UnitStat>();
                unitStatComponent.army = AIArmy;

                AIArmyUnit.GetComponent<UnitStat>().faction = faction;

                AIUnitSelect armyComponent = AIArmyUnit.GetComponent<AIUnitSelect>(); // UnitSelect로 정보 넘기기

                if (armyComponent != null)
                {
                    armyComponent.setArmy(AIArmy); // 군대 정보 설정
                }

                UnitPlacement(AIArmyUnit);
                AIArmyCount++;
            }
        }
    }

    void UnitPlacement(GameObject newUnit)
    {
        Vector2 cityPosition = getRandomCity().unity2DLocation;
        unitAnimator = newUnit.GetComponent<UnitStat>().unit;

        if (unitAnimator != null)
            DestroyImmediate(unitAnimator);

        unitAnimator = newUnit.WMSK_MoveTo(cityPosition);
        unitAnimator.type = (int)UNIT_TYPE.AIR;
        unitAnimator.autoRotation = true;
        unitAnimator.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;

        Vector2 currentVector = newUnit.WMSK_GetMap2DPosition();
        currentVector = unitAnimator.currentMap2DLocation;
        int currentProvinceIndex = map.GetProvinceIndex(currentVector);

        newUnit.GetComponent<UnitStat>().unit = unitAnimator;
    }

    int AIManpower = 0;
    int AIArmySizeLimit;
    void getAIManpower() // 초기 인력값 설정
    {
        Country country = map.countries[Faction_ID];
        AIManpower = country.attrib["population"] / 1000;

        AIArmyMaxCount = AIManpower / 10;

        AIArmySizeLimit = AIManpower / 10 * 5;
    }

    // AI Politics

    List<Faction> EnenmyCountry = new List<Faction>();
    public void startWar()
    {
        isDuringWar = true;

    }
}

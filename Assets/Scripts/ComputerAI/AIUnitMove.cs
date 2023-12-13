using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class AIUnitMove : MonoBehaviour
{
    private WMSK map;

    public GameObjectAnimator currentUnit; // 움직일 유닛 오브젝트

    Vector2 currentVector;
    int currentProvinceIndex; // 유닛이 현재 위치하고 있는 프로벤스의 인덱스
    List<Vector2> aroundProvince = new List<Vector2>(); // 주변 프로빈스 좌표

    int AIfaction_ID;

    private int running_Time;
    private int Running_Time // Detect value change
    {
        get { return running_Time; }
        set
        {
            if (running_Time != value)
            {
                running_Time = value;
                // Script
                PreventionUnitOverlap();
                DuringWar();
            }
        }
    }

    void Start()
    {
        map = WMSK.instance;
        currentUnit = GetComponent<UnitStat>().unit;

        setCountryID();
    }

    void Update()
    {
        Running_Time = DisplayTime.instance.realTime;
    }

    void setCountryID()
    {
        Faction faction = currentUnit.gameObject.GetComponent<UnitStat>().faction;

        switch (faction)
        {
            case Faction.Koguryeo:
                AIfaction_ID = 0;
                break;
            case Faction.Baekje:
                AIfaction_ID = 1;
                break;
            case Faction.Sila:
                AIfaction_ID = 3;
                break;
            case Faction.Gaya:
                AIfaction_ID = 2;
                break;
        }
    }

    void PreventionUnitOverlap() // AI 유닛 겹침 제거
    {
        //currentUnit.Stop();
        currentVector = currentUnit.currentMap2DLocation;
        currentProvinceIndex = map.GetProvinceIndex(currentVector);

        // 겹치는 거리 임계치를 현재 유닛의 크기로 설정
        float currentUnitRadius = currentUnit.GetComponent<Collider>().bounds.extents.magnitude;
        float overlapThreshold = currentUnitRadius;

        int layerMask = LayerMask.GetMask("Units");

        Collider[] colliders = Physics.OverlapSphere(currentUnit.transform.position, 0.01f, layerMask);
        foreach (Collider collider in colliders) // Check Overlap
        {
            UnitStat currentUnitStat = currentUnit.gameObject.GetComponent<UnitStat>();
            UnitStat otherUnitStat = collider.gameObject.GetComponent<UnitStat>();

            if (collider.gameObject != currentUnit.gameObject && otherUnitStat.faction == currentUnitStat.faction) // 겹치는 유닛이 같은 팩션일 때
            {
                GetAroundProvinces(currentProvinceIndex);
                if (otherUnitStat.GetArmyDamage() > currentUnitStat.GetArmyDamage()) // 공격력이 높은 유닛이 도시에 남는다.
                {
                    currentUnit.MoveTo(aroundProvince[UnityEngine.Random.Range(0, aroundProvince.Count)], 0.75f);
                }
                else
                {
                    otherUnitStat.unit.MoveTo(aroundProvince[UnityEngine.Random.Range(0, aroundProvince.Count)], 0.75f);
                }
            }
            
        }
    }

    void DuringWar()
    {
        currentVector = currentUnit.currentMap2DLocation;
        currentProvinceIndex = map.GetProvinceIndex(currentVector);

        bool isAttackArmy = true;

        foreach (City city in map.cities)
        {
            if (city.unity2DLocation == currentVector)
            {
                isAttackArmy = false;
                break;
            }
        }

        if (isAttackArmy == true)
        {
            foreach (Country country in map.countries)
            {
                int countryIndex = map.GetCountryIndex(country);
                if (map.GetCountry(AIfaction_ID).attrib["relationState"][countryIndex] == 2)
                {
                    GetProvincesforWar(currentProvinceIndex, countryIndex);

                    if (aroundProvince.Count > 0)
                    {
                        currentUnit.MoveTo(aroundProvince[UnityEngine.Random.Range(0, aroundProvince.Count)], 1f);

                        if (map.GetProvince(currentVector).countryIndex != AIfaction_ID)
                        {
                            currentVector = currentUnit.currentMap2DLocation;
                            currentProvinceIndex = map.GetProvinceIndex(currentVector);
                            map.CountryTransferProvince(AIfaction_ID, currentProvinceIndex, true);
                        }
                    }
                    else if (aroundProvince.Count == 0)
                    {
                        Vector2 EnemyCapital = map.GetCountryCapital(countryIndex).unity2DLocation;
                        currentUnit.MoveTo(EnemyCapital, 2.5f);
                    }

                }
            }
        }
    }

    void GetAroundProvinces(int provinceIndex)
    {
        aroundProvince.Clear();

        foreach (int neibourIndex in map.provinces[provinceIndex].neighbours) // Fill Around Provinces
        {
            if (map.provinces[provinceIndex].countryIndex == map.provinces[neibourIndex].countryIndex)
            {
                Vector2 center = map.provinces[neibourIndex].center;
                aroundProvince.Add(center);
            }
        }
    }

    void GetProvincesforWar(int provinceIndex, int EnemyID)
    {
        aroundProvince.Clear();

        foreach (int neibourIndex in map.provinces[provinceIndex].neighbours) // Fill Around Provinces
        {
            if (EnemyID == map.provinces[neibourIndex].countryIndex)
            {
                Vector2 center = map.provinces[neibourIndex].center;
                aroundProvince.Add(center);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class UnitPathFinding : MonoBehaviour
{
    public GameObject unit0;
    public GameObjectAnimator unit; // 움직일 유닛 오브젝트
    private WMSK map;
    private Vector2 currentVector;
    private List<Vector2> waypoints = new List<Vector2>();
    private int HereunitID;
    private int userCountryIndex;
    private int currentProvinceIndex; // 유닛이 현재 위치하고 있는 프로벤스의 인덱스
    private int unitSpeed = 5;

    public bool _isSelected = false;
    private bool isSelected
    {
        get { return _isSelected; }
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
            }
        }
    }

    private int running_Time;
    private int count = 0;
    private int Running_Time // Detect value change
    {
        get { return running_Time; }
        set
        {
            if (running_Time != value)
            {
                running_Time = value;
                // Script
                
                if(waypoints.Count > 0)
                {
                    count++;
                    
                    if (count % unitSpeed == 1)
                    {
                        
                        StartCoroutine(MoveUnit());
                        
                    } else if (count == 2)
                    {
                        count = 0;
                    }
                } else
                {
                    count = 0;
                }
            }
        }
    }

    void Start()
    {
        map = WMSK.instance;
        unit = unit0.GetComponent<UnitStat>().unit;
        unit.terrainCapability = TERRAIN_CAPABILITY.OnlyGround; // 유닛이 땅 위로 이동할 수 있도록 설정함
        userCountryIndex = CountrySet.getUserCountryIndex;

        HereunitID = unit.GetComponent<UnitStat>().army.ID;

        map.OnProvinceClick += (int provinceIndex, int regionIndex, int buttonIndex) =>
        {
            if (_isSelected == true && buttonIndex == 1) // If Unit Deleted this part make error
            {
                unit.Stop();
                currentVector = unit.currentMap2DLocation;
                currentProvinceIndex = map.GetProvinceIndex(currentVector);
                if (waypoints != null)
                {
                    waypoints.Clear();
                }
                pathFinding(provinceIndex);
                
            }
        };
    }

    void Update()
    {
        Running_Time = DisplayTime.instance.realTime;
        isSelected = GetComponent<UnitSelect>().isUnitSelected;
    }

    public void pathFinding(int provinceIndex) // 패스파인딩 함수
    {
        int endPointCountryIndex;
        List<int> pathList = new List<int>();

        try
        {
            endPointCountryIndex = map.GetProvince(provinceIndex).countryIndex;
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
            return;
        }

        if (endPointCountryIndex != userCountryIndex) // 도착지가 자신의 영토인지 확인
        {
            if (map.GetCountry(endPointCountryIndex).attrib["relationState"][userCountryIndex] != 2 && CountrySet.getUserCountry.attrib["relationState"][endPointCountryIndex] != 2)
            { // 전쟁상태인지 확인
                return;
            }
        }

        // currentProvinceIndex = map.GetProvinceIndex(currentVector);
        if (currentProvinceIndex == provinceIndex) // 유닛의 현재 위치와 이동하려는 위치가 같을 때 아무 일도 하지 않도록 함
        {
            return;
        }

        if (pathList != null)
        {
            pathList.Clear();
        }

        pathList = map.FindRoute(map.GetProvince(currentVector), map.GetProvince(provinceIndex)); // 유닛의 현재 위치와 이동 위치 사이의 이동 동선을 얻음
        int index = 0;
        int tryCount = 0;
        while(index < pathList.Count)
        {
            Country provinceCountry = GetCountryFromAsset(pathList[index]); // 해당 프로빈스의 국가

            if (provinceCountry.attrib["relationState"][userCountryIndex] == 2 || provinceCountry == CountrySet.getUserCountry)
            { // 이동하려는 프로빈스가 전쟁중인 국가거나 본인의 국가일 때

                waypoints.Add(GetProvinceCenterFromAsset(pathList[index])); // 이동경로 리스트에 경로 추가
                index++;
            } else
            {
                tryCount++;
                if (tryCount > 10)
                {
                    Debug.Log("10번 초과");
                    break;
                } // 10번 이상 전쟁중이지 않은 국가 프로빈스를 만날 시 에러문 출력 후 종료

                int[] neighbourList = GetProvinceFromAsset(pathList[index - 1]).neighbours; // 이웃 프로빈스 리스트
                int targetIndex = GetNearNeighbourProvinceIndex(neighbourList, pathList, index, provinceIndex); // 도착지 프로빈스와 가장 가까운 이웃 프로빈스를 찾고, 인덱스를 저장

                if (targetIndex == -1) // 저장한 프로빈스 인덱스가 없을 시 종료함
                {
                    Debug.Log("경로찾기 실패. 에러");

                    break;
                }
                currentVector = GetProvinceCenterFromAsset(targetIndex); // 전쟁중이지 않은 국가 프로빈스 이전의 프로빈스를 현재 위치로 지정
                currentProvinceIndex = map.GetProvinceIndex(currentVector);

                index = 0; // 경로 순서를 초기화함
                pathList = map.FindRoute(map.GetProvince(currentVector), map.GetProvince(provinceIndex)); // 새로운 경로 찾기를 시도함
            }
            
        }
        UnitProvincePlaced(currentProvinceIndex);
        waypoints.RemoveAt(0);
        //StartCoroutine(MoveUnit()); // 이동 시작
    }

    public void UnitProvincePlaced(int provinceIndex)
    {
        UnitPositionInfo unitInfo = UnitPosition.instance.provincesIndexes.Find(x => x.unitID == HereunitID);
        unitInfo.provinceIndex = provinceIndex;
    }
 
    Country GetCountryFromAsset(int provinceIndex) // 프로빈스 인덱스를 기반으로 프로빈스 소유 국가를 가져옴
    {
        int countryIndex = map.provinces[provinceIndex].countryIndex;
        Country country = map.countries[countryIndex];
        return country;
    }

    Province GetProvinceFromAsset(int provinceIndex) // 프로빈스 인덱스를 기반으로 프로빈스를 가져옴
    {
        Province province = map.provinces[provinceIndex];
        return province;
    }

    Vector2 GetProvinceCenterFromAsset(int provinceIndex) // 프로빈스 인덱스를 기반으로 프로빈스 중앙 벡터값을 가져옴
    {
        Vector2 center = map.provinces[provinceIndex].center;
        return center;
    }

    IEnumerator MoveUnit() // 유닛이 움직이는 도중 이동경로 삭제 시 생기는 에러 방지, 이동 후 좌표 수정
    {
        /*
        unit.MoveTo(waypoints, 0.75f);

        yield return new WaitUntil(() => !unit.isMoving);

        currentVector = unit.currentMap2DLocation;
        currentProvinceIndex = map.GetProvinceIndex(currentVector);
        */
        unit.MoveTo(waypoints[0], 0.75f);
        yield return new WaitUntil(() => !unit.isMoving);

        currentVector = unit.currentMap2DLocation;
        currentProvinceIndex = map.GetProvinceIndex(currentVector);
        waypoints.RemoveAt(0);
        // 적국 프로빈스 편입 과정
        if (map.GetProvince(currentProvinceIndex).countryIndex != CountrySet.getUserCountryIndex)
        {
            if (map.GetCountry(map.provinces[currentProvinceIndex].countryIndex).attrib["relationState"][userCountryIndex] == 2)
            {
                map.CountryTransferProvince(CountrySet.getUserCountryIndex, currentProvinceIndex, true);
                
            }
        }
    }

    int GetNearNeighbourProvinceIndex(int[] neighbourList, List<int> pathList, int index, int provinceIndex) // 인접 프로빈스 리스트를 받아 도착지와 가장 가까운 프로빈스를 찾아 그 프로빈스 인덱스를 리턴함
    {
        int targetIndex = -1;
        float distance = 1000f;

        for (int i = 0; i < neighbourList.Length; i++)
        {
            if (neighbourList[i] != pathList[index] && neighbourList[i] != pathList[index - 2] && !waypoints.Contains(GetProvinceCenterFromAsset(neighbourList[i])))
            {
                if (GetCountryFromAsset(neighbourList[i]) == CountrySet.getUserCountry || GetCountryFromAsset(neighbourList[i]).attrib["relationState"][userCountryIndex] == 2)
                {
                    Vector2 pointA = GetProvinceCenterFromAsset(neighbourList[i]);
                    if (Vector2.Distance(GetProvinceCenterFromAsset(provinceIndex), pointA) < distance)
                    {
                        distance = Vector2.Distance(GetProvinceCenterFromAsset(provinceIndex), pointA);
                        targetIndex = neighbourList[i];
                    }
                }
            }
        }

        return targetIndex;
    }
}
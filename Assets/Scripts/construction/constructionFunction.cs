using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;
using YemaekHan;

public class constructionFunction : MonoBehaviour
{
    public Button[] conButtonList;
    public Slider Slider;

    private WMSK map;

    GoldSystem goldSystem;
    BuildingTypeClass buildingTypeClass;

    JSONObject ClickedCity;

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
                
                if (isDuringConstruction == true)
                {
                    time++;
                    Under_Construction(time);
                } else
                {
                    Slider.value = 0f;
                    time = 0;
                }
                getisDuringConstruction();
                CheckConstructionBuff();
            }
        }
    }
    int time = 0; 
    bool isDuringConstruction = false;
    int ConstructionBuffTime = 0;

    private void Start()
    {
        map = WMSK.instance;
        goldSystem = FindObjectOfType<GoldSystem>();
        buildingTypeClass = FindObjectOfType<BuildingTypeClass>();//Attribute of Building

        for (int i=0; i<conButtonList.Length; i++)
        {
            int index = i;
            conButtonList[index].onClick.AddListener(() => onButtonsClick(index));
        }
    }

    private void Update()
    {
        Running_Time = DisplayTime.instance.realTime;
        if (TargetCity.getTargetCity != null) // targetCity != null
        {
            if (TargetCity.getTargetCityJSON.Count >= 10 || isDuringConstruction == true)
            {
                for (int i = 0; i < conButtonList.Length; i++)
                {
                    conButtonList[i].interactable = false;
                }
            }
            else
            {
                for (int i = 0; i < conButtonList.Length; i++)
                {
                    conButtonList[i].interactable = true;
                }
            }
        }
    }

    private void onButtonsClick(int index) // Click Building Image
    {
        string typeName = "";
        switch(index)
        {
            case 0:
                typeName = "farm";
                break;
            case 1:
                typeName = "barrack";
                break;
            case 2:
                typeName = "trading";
                break;
        }
        ButtonAction(typeName);
    }

    string buildingType = "";
    private void ButtonAction(string building)
    {
        isDuringConstruction = true;
        buildingType = building;
        //buildingTypeClass = FindObjectOfType<BuildingTypeClass>();//Attribute of Building
        ClickedCity = TargetCity.getTargetCityJSON;

        goldSystem.PayGold(buildingTypeClass.BuildingPrice(building)); //Pay Construction Cost
    }

    private void Under_Construction(int time)
    {
        if (time <= buildingTypeClass.ConsTime)
        {
            Slider.value = ((float)time / (float)buildingTypeClass.ConsTime);
        }
        else if (time > buildingTypeClass.ConsTime)
        {
            int count = ClickedCity.Count;
            string slotname = "slot" + count.ToString();
            ClickedCity.AddField(slotname, buildingType);

            // About Gold
            //goldSystem.BuildingMaintenance += buildingTypeClass.BuildingTurnGold(buildingType);
            buildingTypeClass.CheckAllCitiesAndGetTurnGold();
            isDuringConstruction = false;
            Slider.value = 0.99f;
        }
    }

    private void getisDuringConstruction()
    {
        AlarmManager alarmManager = FindAnyObjectByType<AlarmManager>();
        alarmManager.isDuringConstruction = isDuringConstruction;
    }

    void CheckConstructionBuff() // Check Construction Buff by Country Choice
    {
        if (buildingTypeClass.ConsTime != 10)
        {
            Debug.Log("건설 버프 실행");
            ConstructionBuffTime++;
            if (ConstructionBuffTime >= 60)
            {
                buildingTypeClass.ConsTime = 10;
            }
        }
    }
}

/* Backup ButtonAction Function
private void ButtonAction(string building)
{
    isDuringConstruction = true;

    int count = TargetCity.getTargetCityJSON.Count;
    string slotname = "slot" + count.ToString();
    TargetCity.getTargetCityJSON.AddField(slotname, building);

    // About Gold
    buildingTypeClass = new BuildingTypeClass(); //Attribute of Building
    goldSystem.BuildingMaintenance += buildingTypeClass.BuildingTurnGold(building);
}*/
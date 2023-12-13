using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;
using YemaekHan;

public class ButtonDataControl : MonoBehaviour
{
    public Button[] CountryChoiceButtons;

    public GameObject CountryChoicePanel; // Info Panel
    public TMP_Text Panel_CountryChoiceName;
    public TMP_Text Panel_CountryChoiceDesc;
    public TMP_Text Panel_CountryChoiceTime;
    public Button FinalButton;

    public GameObject CompleteCountryChoicePanel; // complete Panel
    public TMP_Text CompleteCountryChoiceText;
    public Button OkButton;

    private int running_Time;
    private int Time = 0;
    private int DoneTime;
    private bool isFinalButtonClick = false;

    CountryChoiceEffects Effect;

    public int Running_Time // Detect In Game Time change
    {
        get { return running_Time; }
        set
        {
            if (running_Time != value)
            {
                running_Time = value;
                // Script
                if (isFinalButtonClick == true)
                {
                    TimeChecker();
                } 
                isDuringNationalFocus();
            }
        }
    }

    private int currentBtnIndex = 0; // 현재 중점 인덱스 번호 0~9
    private TreeData myTree;

    private int MyCounty;
    private List<CountryTree> countryInfo;

    void Start()
    {
        var loadedJson = Resources.Load<TextAsset>("CountryChoice");
        myTree = JsonUtility.FromJson<TreeData>(loadedJson.ToString());

        Effect = FindObjectOfType<CountryChoiceEffects>();

        ButtonsLock();
        getCountryInfo();
        setButton();
        OnButtonClick();
        OkButton.onClick.AddListener(() => TimeisDoneTime());
    }

    private void Update()
    {
        Running_Time = DisplayTime.instance.realTime;
    }

    void getCountryInfo() // Get player country
    {
        MyCounty = CountrySet.getUserCountryIndex;
        switch (MyCounty)
        {
            case 0:
                countryInfo = myTree.Koguryeo;
                break;
            case 1:
                countryInfo = myTree.Baekje;
                break;
            case 2:
                countryInfo = myTree.Gaya;
                break;
            case 3:
                countryInfo = myTree.Sila;
                break;
        }
    }

    void setButton() // Button name setting
    {
        for (int i = 0; i < CountryChoiceButtons.Length; i++)
        {
            CountryChoiceButtons[i].GetComponentInChildren<TMP_Text>().text = countryInfo[i].name;
        }
    }

    void OnButtonClick() // Button click function
    {
        for (int i = 0; i < CountryChoiceButtons.Length; i++)
        {
            int index = i;
            CountryChoiceButtons[index].onClick.AddListener(() => setPanel(index));
        }
        FinalButton.onClick.AddListener(() => finalButtonOnclick()); // Final button function
    }

    void setPanel(int index) // Panel name and desc setting
    {
        DoneTime = countryInfo[index].time;

        Panel_CountryChoiceName.text = countryInfo[index].name;
        Panel_CountryChoiceDesc.text = countryInfo[index].description;
        Panel_CountryChoiceTime.text = Time.ToString() + " / " + DoneTime.ToString();
        CountryChoicePanel.SetActive(true);
    }

    void finalButtonOnclick() // Final Button click
    {
        isFinalButtonClick = true;
        CountryChoicePanel.SetActive(false);
    }

    void ButtonsLock() // Lock buttons other than the current button
    {
        for (int i = 0; i < CountryChoiceButtons.Length; i++)
        {
            bool isCurrent = i == currentBtnIndex ? true : false;
            CountryChoiceButtons[i].interactable = isCurrent;
        }
    }

    void isDuringNationalFocus()
    {
        AlarmManager alarmManager = FindAnyObjectByType<AlarmManager>();
        alarmManager.isDuringNationalFocus = isFinalButtonClick;

        //Debug.Log("진행중인 중점 없음");
    }

    void TimeChecker()
    {
        Time++;

        if (Time >= 5) // Time == DoneTime
        {
            isFinalButtonClick = false;
            CompleteCountryChoiceText.text = countryInfo[currentBtnIndex].description;
            CompleteCountryChoicePanel.SetActive(true);
        }
    }

    void TimeisDoneTime() // OKButton Click Event Function
    {
        CompleteCountryChoicePanel.SetActive(false);

        // Country Choice Complete Effect
        List<EffectData> effects = countryInfo[currentBtnIndex].effect;

        if (effects != null)
        {
            foreach (EffectData effectData in effects)
            {
                string effectName = effectData.effectName;
                int effectValue = effectData.effectValue;
                CompleteEffect(effectName, effectValue);
            }
        }

        currentBtnIndex = currentBtnIndex + 1;
        ButtonsLock(); // Click Finalbutton then Unlock next button
        Time = 0;

        OkButton.onClick.RemoveListener(() => TimeisDoneTime());
    }

    void CompleteEffect(string effectName, int effectValue)
    {
        switch (effectName)
        {
            case "Gold":
                Effect.CountryChoice_Gold(effectValue);
                break;
            case "PST":
                Effect.CountryChoice_PST(effectValue);
                break;
            case "Manpower":
                Effect.CountryChoice_Manpower(effectValue);
                break;
            case "Construction_Time":
                Effect.CountryChoice_Construction(effectValue);
                break;
            case "TurnGold":
                Effect.CountryChoice_TurnGold();
                break;
            case "Army":
                Effect.CountryChoice_Army();
                break;
            case "ArmyMaintenence":
                Effect.CountryChoice_ArmyMaintenence();
                break;
            case "Diplomacy": // (effectValue == 0) => War, (effectValue == 1) => Aliance
                Effect.CountryChoice_Diplomacy(effectValue);
                break;
        }
    }
}

[System.Serializable]
class CountryTree
{
    public int index;
    public string name, description;
    public List<EffectData> effect;
    public int time;
}

[System.Serializable]
class EffectData
{
    public string effectName;
    public int effectValue;
}

[System.Serializable]
class TreeData
{
    public List<CountryTree> Koguryeo;
    public List<CountryTree> Baekje;
    public List<CountryTree> Sila;
    public List<CountryTree> Gaya;
}
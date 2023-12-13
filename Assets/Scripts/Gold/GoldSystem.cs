using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class GoldSystem : MonoBehaviour
{
    Gold gold = new Gold();
    public int ArmyMaintenance; //Almost Negative Number
    public int BuildingMaintenance; //Almost Positive Number

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
                GetTurnGold();
                CalculateGold();
            }
        }
    }

    private void Start()
    {
        GetGold();
        PST = FindObjectOfType<PublicSentimentSystem>();
    }
    private void Update()
    {
        //GetTurnGold();
        Running_Time = DisplayTime.instance.realTime;
        
    }

    void GetGold() // 초기값 설정
    {
        Country country = TargetCountry.getTargetCountry;
        gold.TotalGold = country.attrib["gold"];
        gold.BasicTurnGold = country.attrib["turnGold"];
        gold.TurnGold = ArmyMaintenance + BuildingMaintenance;
    }

    PublicSentimentSystem PST;
    void GetTurnGold() // Include PST Effect
    {
        int TurnGold = ArmyMaintenance + BuildingMaintenance + gold.BasicTurnGold;

        if (TurnGold > 0)
        {
            if (PST.GetCurrentPublicSentiment() > 80)
            {
                TurnGold = (int)(TurnGold * 1.5);
            }
            if (PST.GetCurrentPublicSentiment() < 40)
            {
                TurnGold = (int)(TurnGold * 0.7);
            }
        }

        gold.TurnGold = ArmyMaintenance + BuildingMaintenance + gold.BasicTurnGold; ;
    }

    void CalculateGold()
    {
        gold.TotalGold = gold.TotalGold + gold.TurnGold;
    }

    public int GetCurrentGold()
    {
        return gold.TotalGold;
    }

    public void BuildingMaintenanceGoldUpdate(int bgGold)
    {
        BuildingMaintenance = bgGold;
    }

    public void PayGold(int pay)
    {
        gold.TotalGold -= pay;
    }

    //Effect Of CountryChoice
    public void Gold_Effect(int EffectGold)
    {
        gold.TotalGold += EffectGold;
    }
}

public class Gold
{
    public int TotalGold { get; set; }
    public int TurnGold { get; set; }
    public int BasicTurnGold { get; set; }
}

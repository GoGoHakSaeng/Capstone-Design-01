using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class PublicSentimentSystem : MonoBehaviour
{
    PublicSentiment PST = new PublicSentiment();

    public GameObject GameOverPanel;

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
                SetMinusPST();
                CalculatePST();
                GameOverByPST();
            }
        }
    }

    private void Start()
    {
        GetPublicSentiment();
        goldSystem = FindObjectOfType<GoldSystem>();
    }

    private void Update()
    {
        Running_Time = DisplayTime.instance.realTime;
    }

    void GetPublicSentiment()
    {
        Country country = TargetCountry.getTargetCountry;
        PST.TotalPublicsentiment = country.attrib["popular Sentiment"] / 10;
    }

    public int GetCurrentPublicSentiment()
    {
        return PST.TotalPublicsentiment;
    }

    GoldSystem goldSystem;
    void SetMinusPST()
    {
        if (goldSystem.GetCurrentGold() < 0)
        {
            PST.MinusPST = (goldSystem.GetCurrentGold() / -200);
        }
        // 안정도를 올리는 무언가
    }

    void CalculatePST()
    {
        PST.TotalPublicsentiment = PST.TotalPublicsentiment - PST.MinusPST + PST.PlusPST;
    }

    void GameOverByPST()
    {
        if (PST.TotalPublicsentiment < 30)
        {
            GameOverPanel.SetActive(true);
        }
    }

    //Effect Of CountryChoice
    public void PublicSentiment_Effect(int EffectPST)
    {
        PST.TotalPublicsentiment += EffectPST;
    }
}

public class PublicSentiment
{
    public int TotalPublicsentiment { get; set; }
    public int PlusPST { get; set; } // Positive Num (Should use Minus(-) to Calculation)
    public int MinusPST { get; set; } // Postive Num
} 

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class UI_CountryInfo : MonoBehaviour
{
    private WMSK map;

    public TMP_Text Public_Sentiment;
    public TMP_Text Gold;
    public TMP_Text Population;

    GoldSystem goldSystem; // GoldSystem 객체 생성
    PublicSentimentSystem publicSentimentSystem; // PublicSentimentSystem 객체 생성
    ManpowerSystem manpowerSystem;

    // Start is called before the first frame update
    void Start()
    {
        map = WMSK.instance;
        goldSystem = FindObjectOfType<GoldSystem>(); // GoldSystem 객체 찾기
        publicSentimentSystem = FindAnyObjectByType<PublicSentimentSystem>();
        manpowerSystem = FindAnyObjectByType<ManpowerSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        setCountryInfo();
    }

    void setCountryInfo()
    {
        Country country = TargetCountry.getTargetCountry;

        Public_Sentiment.text = publicSentimentSystem.GetCurrentPublicSentiment().ToString() + " %"; // PublicSentimnetSystem의 GetCurrentPublicSentiment() 메서드 호출
        Gold.text = goldSystem.GetCurrentGold().ToString(); // GoldSystem의 GetCurrentGold() 메서드 호출
        Population.text = manpowerSystem.GetCurrentManpower().ToString() + " 천"; // ManpowerSystem의 GetCurrentManpower() 메서드 호출
        /*
        if (country == CountrySet.getUserCountry)
        {
            Public_Sentiment.text = publicSentimentSystem.GetCurrentPublicSentiment().ToString() + " %"; // PublicSentimnetSystem의 GetCurrentPublicSentiment() 메서드 호출
            Gold.text = goldSystem.GetCurrentGold().ToString(); // GoldSystem의 GetCurrentGold() 메서드 호출
            Population.text = manpowerSystem.GetCurrentManpower().ToString() + " 천"; // ManpowerSystem의 GetCurrentManpower() 메서드 호출
        }*/
    }
}

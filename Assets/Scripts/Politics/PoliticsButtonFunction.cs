using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class PoliticsButtonFunction : MonoBehaviour
{
    enum State
    {
        INDEX = 0,
        NORMAL,
        WAR,
        ALLIANCE,
        NONAGGRESSION,
        JUSTIFY
        // 해당 국가의 인덱스는 0, 기본 상태는 1, 전쟁 상태는 2, 동맹은 3, 불가침은 4, 정당화는 5를 뜻함
    }
    public static PoliticsButtonFunction instance;
    public bool[] isHaveJustify = new bool[] { false, false, false, false };

    private int userIndex;
    public int targetIndex;

    private void Awake()
    {
        if (PoliticsButtonFunction.instance == null)
        {
            PoliticsButtonFunction.instance = this;
        }
    }

    private void Update()
    {
        userIndex = CountrySet.getUserCountryIndex;
        targetIndex = TargetCountry.getTargetCountryIndex;
    }

    public bool declareWar()
    {
        if (isHaveJustify[targetIndex] == true)
        {
            CountrySet.getUserCountry.attrib["relationState"][targetIndex] = (int)State.WAR;
            TargetCountry.getTargetCountry.attrib["relationState"][userIndex] = (int)State.WAR;

            TargetCountry.getTargetCountry.attrib["friendship"][userIndex] = 0;
            isHaveJustify[targetIndex] = false;

            return true;
        } else
        {
            return false;
        }
    }

    public void suggestAlliance()
    {
        CountrySet.getUserCountry.attrib["relationState"][targetIndex] = (int)State.ALLIANCE;
        TargetCountry.getTargetCountry.attrib["relationState"][userIndex] = (int)State.ALLIANCE;
    }

    public void goodwillMotion()
    {
        TargetCountry.getTargetCountry.attrib["relationState"][userIndex] += 10;
        if (TargetCountry.getTargetCountry.attrib["relationState"][userIndex] >= 200)
        {
            TargetCountry.getTargetCountry.attrib["relationState"][userIndex] = 200;
        }
    }

    public void justifyWar()
    {
        CountrySet.getUserCountry.attrib["relationState"][targetIndex] = (int)State.JUSTIFY;
        isHaveJustify[targetIndex] = true;
    }

    public void concludeNonAggression()
    {
        CountrySet.getUserCountry.attrib["relationState"][targetIndex] = (int)State.NONAGGRESSION;
        TargetCountry.getTargetCountry.attrib["relationState"][userIndex] = (int)State.NONAGGRESSION;
    }

    public void clearState()
    {
        CountrySet.getUserCountry.attrib["relationState"][targetIndex] = (int)State.NORMAL;
        TargetCountry.getTargetCountry.attrib["relationState"][userIndex] = (int)State.NORMAL;
        isHaveJustify[targetIndex] = false;
    }

    public void clearUserState()
    {
        CountrySet.getUserCountry.attrib["relationState"][targetIndex] = (int)State.NORMAL;
        isHaveJustify[targetIndex] = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summon_ShilUnit : MonoBehaviour
{
    public GameObject unitPrefab; // 소환할 유닛의 프리팹

    public void SummonUnit()
    {
        // 유닛을 생성하고, 고구려 진영에 속하도록 설정
        GameObject newUnit = Instantiate(unitPrefab);
        newUnit.GetComponent<CharStats>().faction = Faction.Shilla;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryChoiceEffects : MonoBehaviour
{
    PublicSentimentSystem PSTSystem;
    GoldSystem goldSystem;
    ManpowerSystem manpowerSystem;
    BuildingTypeClass buildingType;
    public void CountryChoice_Gold(int gold)
    {
        Debug.Log("Gold Effect");

        goldSystem = FindObjectOfType<GoldSystem>();

        goldSystem.Gold_Effect(gold);
    }
    public void CountryChoice_PST(int PST)
    {
        Debug.Log("PST Effect");

        PSTSystem = FindObjectOfType<PublicSentimentSystem>();

        PSTSystem.PublicSentiment_Effect(PST);
    }
    public void CountryChoice_Manpower(int manpower)
    {
        Debug.Log("Manpower Effect");

        manpowerSystem = FindObjectOfType<ManpowerSystem>();

        manpowerSystem.Manpower_Effect(manpower);
    }
    public void CountryChoice_Construction(int constime)
    {
        Debug.Log("Construction Effect");

        buildingType = FindObjectOfType<BuildingTypeClass>();

        buildingType.ConstructionTime_Effect(constime);
    }
    public void CountryChoice_TurnGold()
    {
        Debug.Log("TurnGold Effect");
    }
    public void CountryChoice_Army()
    {
        Debug.Log("Army Power Effect");
    }
    public void CountryChoice_ArmyMaintenence()
    {
        Debug.Log("Army Maintenence Effect");
    }
    public void CountryChoice_Diplomacy(int type)
    {
        Debug.Log("Diplomacy Effect");

        switch (type)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }
    public void CountryChoice_CapitalRelocate()
    {
        Debug.Log("Capital Relocate Effect");
    }
    public GameObject SelectPanel;
    public List<Button> SelectButtons;
    public void CountryChoice_Select()
    {
        SelectPanel.SetActive(true);
        for (int i = 0; i<SelectButtons.Count; i++)
        {
            int index = i;
            SelectButtons[index].onClick.AddListener(() => { });
        }
    }
}

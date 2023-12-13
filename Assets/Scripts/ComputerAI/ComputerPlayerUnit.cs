using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class ComputerPlayerUnit : MonoBehaviour
{
    private static City targetCity;
    private static JSONObject jsonObject;
    GetCountryID getCountryID;

    // Functions of CPU

    List<int> AIfactions = new List<int>();
    List<GameObject> CPU = new List<GameObject>();
    public GameObject computerAIParent;
    public GameObject aiPrefab;
    public GameObject[] UnitManager;

    // Start is called before the first frame update
    void Start()
    {
        setAI();
    }

    void setAI()
    {
        getCountryID = FindAnyObjectByType<GetCountryID>();

        AIfactions = getCountryID.GetAiFactions();

        for (int i = 0; i < AIfactions.Count; i++)
        {
            GameObject aiObject = Instantiate(aiPrefab);
            CPU.Add(aiObject);

            aiObject.transform.parent = computerAIParent.transform;

            EachAIManager eachAI = aiObject.GetComponent<EachAIManager>();
            if (eachAI != null)
            {
                eachAI.Faction_ID = AIfactions[i];
                eachAI.UnitManager = UnitManager[AIfactions[i]].transform;
            }
        }
    }
}


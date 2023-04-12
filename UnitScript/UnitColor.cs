using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitColor : MonoBehaviour
{
    private Renderer unitRenderer;

    void Start()
    {
        unitRenderer = gameObject.GetComponent<Renderer>();    
    }

    void Update()
    {
        switch (GetComponent<CharStats>().faction)
        {
            case Faction.Goguryeo:
                unitRenderer.material.color = Color.red;
                break;
            case Faction.Baekje:
                unitRenderer.material.color = Color.green;
                break;
            case Faction.Shilla:
                unitRenderer.material.color = Color.blue;
                break;
            default:
                unitRenderer.material.color = Color.yellow;
                break;
        }
    } 
}
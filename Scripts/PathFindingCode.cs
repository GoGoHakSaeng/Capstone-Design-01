using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

public class PathFindingCode : MonoBehaviour
{
    public GameObject UnitObject;
    public Vector2 StartProvinceVector, SelectProvinceVector;

    public void Start()
    {
        //StartProvinceVector = 
        Debug.Log(UnitObject.WMSK_GetMap2DPosition());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

public class MouseCircle : MonoBehaviour
{
    public enum UNIT_TYPE
    {
        AIR = 1
    }

    public WMSK map;
    //public GameObjectAnimator airplane;
    public GameObject circle;
    /*
    public List<int> pathList;
    public Vector2 currentVector;
    public int currentProvinceIndex;
    */
    public float circleRadius = 50f, circleRingStart = 0, circleRingEnd = 1f;
    public Color circleColor = new Color(1f, 0.2f, 0.2f, 0.75f);
    public bool enableClickToMoveTank = true, showCircle = true;

    // Start is called before the first frame update
    public void Start()
    {
        map = WMSK.instance;

        map.OnProvinceClick += (int provinceIndex, int regionIndex, int buttonIndex) =>
        {
            if (enableClickToMoveTank)
            {
                //MoveTankWithPathFinding(provinceIndex);
            }
        };

        map.OnMouseMove += (float x, float y) =>
        {
            /*if (airplane.isMoving)
                return;*/
            UpdatePathLine(x, y);
        };

        map.CenterMap();

        // dummy.DropTankOnCity();
        
    }
    /*
    public void DropTankOnCity()
    {
        int cityIndex = map.GetCityIndex("Sibal", "Koguryeo");

        Vector2 cityPosition = map.cities[cityIndex].unity2DLocation;

        if (airplane != null)
            DestroyImmediate(airplane);
        GameObject airplaneG0 = Instantiate(Resources.Load<GameObject>("Tower/Pawn"));
        airplane = airplaneG0.WMSK_MoveTo(cityPosition);
        airplane.type = (int)UNIT_TYPE.AIR;
        airplane.autoRotation = true;
        airplane.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;

        dummy = GetComponent<UnitStat>();
        dummy.GetGameObject(airplane);
        dummy = null;

        // currentVector = airplaneG0.WMSK_GetMap2DPosition();

        map.FlyToLocation(cityPosition, 2.0f, 0.5f);
    }
    */
    /*
    public void MoveTankWithPathFinding(int provinceIndex)
    {
        List<Vector2> waypoints = new List<Vector2>();
        airplane.Stop();

        if (airplane == null)
        {
            DropTankOnCity();
            return;
        }

        currentProvinceIndex = map.GetProvinceIndex(currentVector);
        if (currentProvinceIndex == provinceIndex)
        {
            return;
        }

        pathList = map.FindRoute(map.GetProvince(currentVector), map.GetProvince(provinceIndex));
        airplane.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;

        for (int i = 0; i < pathList.Count; i++)
        {
            waypoints.Add(map.provinces[pathList[i]].center);
            pathList.Remove(i);
        }
        airplane.MoveTo(waypoints, 0.75f);

        currentVector = map.GetProvince(provinceIndex).center;
    }
    */
    public void UpdatePathLine(float x, float y)
    {
        Vector2 destination = new Vector2(x, y);

        UpdateCircle(destination);
    }

    public void UpdateCircle(Vector2 position)
    {
        if (circle != null)
        {
            Destroy(circle);
        }
        if (!showCircle)
            return;
        circle = map.AddCircle(position, circleRadius, circleRingStart, circleRingEnd, circleColor);
    }
}

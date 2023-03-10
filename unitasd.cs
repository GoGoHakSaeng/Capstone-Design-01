using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

public class unitasd : MonoBehaviour
{
    enum UNIT_TYPE
    {
        TANK = 1
    }

    WMSK map;
    GameObjectAnimator tank;
    GameObject circle;

    float circleRadius = 100f, circleRingStart = 0, circleRingEnd = 1f;
    Color circleColor = new Color(1f, 0.2f, 0.2f, 0.75f);
    bool enableClickToMoveTank = true, showCircle = true;

    // Start is called before the first frame update
    void Start()
    {
        map = WMSK.instance;

        map.OnProvinceClick += (int provinceIndex, int regionIndex, int buttonIndex) =>
        {
            if (enableClickToMoveTank)
            {
                MoveTankWithPathFinding(map.provinces[provinceIndex].centerRect);
                Debug.Log(map.provinces[provinceIndex].centerRect);
            }
        };
        /*map.OnClick += (float x, float y, int buttonIndex) =>
        {
            if (enableClickToMoveTank)
            {
                MoveTankWithPathFinding(new Vector2(x, y));
            }
        };*/

        map.OnMouseMove += (float x, float y) =>
        {
            if (tank.isMoving)
                return;
            UpdatePathLine(x, y);
        };

        map.CenterMap();

        DropTankOnCity();
    }

    void DropTankOnCity()
    {
        int cityIndex = map.GetCityIndex("Seoul", "South Korea");

        Vector2 cityPosition = map.cities[cityIndex].unity2DLocation;

        if (tank != null)
            DestroyImmediate(tank);
        GameObject tankG0 = Instantiate(Resources.Load<GameObject>("Tank/CompleteTank"));
        tank = tankG0.WMSK_MoveTo(cityPosition);
        tank.type = (int)UNIT_TYPE.TANK;
        tank.autoRotation = true;
        tank.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;

        map.FlyToLocation(cityPosition, 2.0f, 0.5f);
    }

    void MoveTankWithPathFinding(Vector2 destination)
    {
        if (tank == null)
        {
            DropTankOnCity();
            return;
        }

        tank.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;
        tank.MoveTo(destination, 0.01f);
    }

    void UpdatePathLine(float x, float y)
    {
        Vector2 destination = new Vector2(x, y);

        UpdateCircle(destination);
    }

    void UpdateCircle(Vector2 position)
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

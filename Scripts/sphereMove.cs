using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;

public class sphereMove : MonoBehaviour
{
    enum UNIT_TYPE
    {
        TANK = 1,
        SPHERE = 2
    }

    WMSK map;
    //GameObjectAnimator tank;
    GameObjectAnimator sphere;
    GameObject circle;

    float circleRadius = 50f, circleRingStart = 0, circleRingEnd = 1f;
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

        map.OnMouseMove += (float x, float y) =>
        {
            if (sphere.isMoving)
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

        if (sphere != null)
            DestroyImmediate(sphere);
        GameObject sphereG0 = Instantiate(Resources.Load<GameObject>("Sphere/Sphere"));
        sphere = sphereG0.WMSK_MoveTo(cityPosition);
        sphere.type = (int)UNIT_TYPE.SPHERE;
        sphere.autoRotation = true;
        sphere.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;

        map.FlyToLocation(cityPosition, 2.0f, 0.5f);
    }

    void MoveTankWithPathFinding(Vector2 destination)
    {
        if (sphere == null)
        {
            DropTankOnCity();
            return;
        }

        sphere.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;
        sphere.MoveTo(destination, 0.3f);
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

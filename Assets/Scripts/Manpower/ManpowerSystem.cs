using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class ManpowerSystem : MonoBehaviour
{
    Manpower manpower = new Manpower();

    // Start is called before the first frame update
    void Start()
    {
        GetManpower();
    }

    void GetManpower() // 초기값 설정
    {
        Country country = TargetCountry.getTargetCountry;
        manpower.TotalManpower = country.attrib["population"] / 1000;
    }

    public void CalculateManpower(int ArmyManpower)
    {
        manpower.TotalManpower = manpower.TotalManpower - ArmyManpower;
    }

    public int GetCurrentManpower()
    {
        return manpower.TotalManpower;
    }

    //Effect Of CountryChoice
    public void Manpower_Effect(int ManpowerEffect)
    {
        manpower.TotalManpower += ManpowerEffect;
    }
}

public class Manpower
{
    public int TotalManpower { get; set; }
}

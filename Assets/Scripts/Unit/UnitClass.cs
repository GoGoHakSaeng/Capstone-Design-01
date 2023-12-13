using Unity.Burst.Intrinsics;
using UnityEngine;

public class Army
{
    public int ID { get; set; }
    public int InfantrySize { get; set; }
    public int ArcherSize { get; set; }
    public int CavalrySize { get; set; }
    public int TotalSize { get; set; }

    // This part is tempetory
    private int InfantryPrice = 50;
    private int ArcherPrice = 100;
    private int CavalryPrice = 200;
    public int TotalPrice() { // This is make Negative Number
        int total = (InfantrySize * InfantryPrice) + (ArcherSize * ArcherPrice) + (CavalrySize * CavalryPrice);
        return total * -1;
    }
    public int ArmyDamage()
    {
        return (InfantrySize * 1) + (ArcherSize * 4) + (CavalrySize * 2);
    }
    public int ArmyDefense()
    {
        return (InfantrySize * 2) + (ArcherSize * 1) + (CavalrySize * 4);
    }
    public int ArmyHealth()
    {
        return (InfantrySize * 100) + (ArcherSize * 50) + (CavalrySize * 200);
    }
}

/*
public class Army
{
    public int TestID { get; set; }
    public int ArmySize { get; set; }
    public string TroopType { get; set; }
}*/

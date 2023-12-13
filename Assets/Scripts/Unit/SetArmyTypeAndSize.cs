using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetArmyTypeAndSize : MonoBehaviour
{
    // Infantry
    public TMP_InputField InfantrySizeField;
    public int InfantrySize = 0;

    // Archer
    public TMP_InputField ArcherSizeField;
    public int ArcherSize = 0;

    // Cavalry
    public TMP_InputField CavalrySizeField;
    public int CavalrySize = 0;

    public int TotalArmySize = 0;

    void Start()
    {
        InfantrySizeField.onValueChanged.AddListener(OnInfantrySizeValueChanged);
        ArcherSizeField.onValueChanged.AddListener(OnArcherSizeValueChanged);
        CavalrySizeField.onValueChanged.AddListener(OnCavalrySizeValueChanged);
        CalculateTotalArmySize();
    }

    private void OnInfantrySizeValueChanged(string ArmySize)
    {
        int.TryParse(ArmySize, out InfantrySize);
        CalculateTotalArmySize();
    }

    private void OnArcherSizeValueChanged(string ArmySize)
    {
        int.TryParse(ArmySize, out ArcherSize);
        CalculateTotalArmySize();
    }

    private void OnCavalrySizeValueChanged(string ArmySize)
    {
        int.TryParse(ArmySize, out CavalrySize);
        CalculateTotalArmySize();
    }

    private void CalculateTotalArmySize()
    {
        TotalArmySize = InfantrySize + ArcherSize + CavalrySize;
    }
}

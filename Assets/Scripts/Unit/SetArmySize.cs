using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class SetArmySize : MonoBehaviour
{
    public Button MinusButton;
    public Button PlusButton;
    public TMP_InputField InputField;

    public Button TroopType;

    public int ArmySize;

    void Start()
    {
        ArmySize = 0;

        InputField.onEndEdit.AddListener(UserInputArmySize);
        UpdateInputFieldContent();
        PlusButton.onClick.AddListener(PlusArmySize);
        MinusButton.onClick.AddListener(MinusArmySize);
    }

    void PlusArmySize()
    {
        ArmySize++;
        UpdateInputFieldContent();
    }

    void MinusArmySize()
    {
        if (ArmySize > 0)
        {
            ArmySize--;
            UpdateInputFieldContent();
        }
    }

    void UserInputArmySize(string input)
    {
        int.TryParse(input, out ArmySize);
        UpdateInputFieldContent();
    }

    void UpdateInputFieldContent()
    {
        InputField.text = ArmySize.ToString();
    }
}

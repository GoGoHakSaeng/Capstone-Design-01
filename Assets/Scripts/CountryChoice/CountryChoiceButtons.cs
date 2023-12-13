using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountryChoiceButtons : MonoBehaviour
{
    public TMP_Text Name;
    public int TimeforComplete;
    public Button[] childButtons;
    public Button FinalStartBtn;

    //public GameObject CountryChoicePanel;

    TreeData myTree;

    //public TMP_Text btnname;
    //public TMP_Text btndescription;

    private int Time;
    private Button CurrenBtn;
    private int currentBtnindex;

    private void Start()
    {
        //DisableChildButtons(); // Lock child buttons

        CurrenBtn = GetComponent<Button>();

        //CurrenBtn.onClick.AddListener(OnParentButtonClick); ÇÏ´ø °÷

        var loadedJson = Resources.Load<TextAsset>("CountryChoice");
        myTree = JsonUtility.FromJson<TreeData>(loadedJson.ToString());

    }

    private void CompleteChoice()
    {
        if (TimeforComplete == 1)
        {
            //
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using YemaekHan;

public class AIUnitSelect : MonoBehaviour
{
    UnitStat unitStat;

    public LayerMask unitLayerMask; // 유닛 레이어 마스크

    private Transform selectedUnit; // 선택된 유닛의 Transform

    private Army selectedArmy = null;
    public bool isUnitSelected = false;

    // AI Army Info Panel
    GameObject AIArmyInfoPanel;
    TMP_Text AIArmyNameText;
    TMP_Text AIArmySizeText;

    // Start is called before the first frame update
    void Start()
    {
        unitStat = gameObject.GetComponent<UnitStat>();
        getAIInfoPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClickUnit();
        }
    }

    public void setArmy(Army Army)
    {
        selectedArmy = Army;
    }

    void getAIInfoPanel()
    {
        AIArmyInfoPanel = GameObject.Find("Canvas").transform.Find("AIArmyInfoPanel").gameObject;
        AIArmyNameText = AIArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[0];
        AIArmySizeText = AIArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[1];
    }

    private void onClickUnit() // 유닛 클릭 이벤트
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Raycasting을 통해 유닛 선택

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, unitLayerMask))
        {
            if (hitInfo.transform == transform)
            {
                SelectUnit(hitInfo.transform);
            } 
        }
        else 
        {
            DeselectUnit();
        }
    }

    private void SelectUnit(Transform unitTransform)
    {
        selectedUnit = unitTransform;
        IsUnitSelected.instance.isSelected = true; // 외부 스크립트로 전달 위해 bool값 전달
        setAIArmyInfoPanel();

        if (selectedUnit != null)
        {
            Renderer selectedRenderer = selectedUnit.GetComponent<Renderer>();
            selectedRenderer.material.color = Color.gray;
        }

        AIArmyInfoPanel.SetActive(true);
        isUnitSelected = true;
    }

    private void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            GetComponent<UnitStat>().UnitColorChange();
            IsUnitSelected.instance.isSelected = false; // 외부 스크립트로 전달 위해 bool값 전달
        }
        selectedUnit = null;
        AIArmyInfoPanel.SetActive(false);
        isUnitSelected = false;
    }

    private void setAIArmyInfoPanel()
    {
        string AIArmyName = "";
        
        switch (unitStat.faction)
        {
            case Faction.Koguryeo:
                AIArmyName = "고구려";
                break;
            case Faction.Baekje:
                AIArmyName = "백제";
                break;
            case Faction.Gaya:
                AIArmyName = "가야";
                break;
            case Faction.Sila:
                AIArmyName = "신라";
                break;
        }

        string AIArmySize;
        int num = (selectedArmy.TotalSize);

        if (num / 10 > 0)
        {
            if (num % 10 >= 5)
            {
                num = (num + 5) / 10 * 10;
            }
            else
            {
                num = num / 10 * 10;
            }
        }
        AIArmySize = (num * 1000).ToString();

        if (AIArmyInfoPanel != null)
        {
            AIArmyNameText.SetText(AIArmyName + "군");
            AIArmySizeText.SetText(AIArmySize + "여 명");
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WorldMapStrategyKit;
using YemaekHan;

public class UnitSelect : MonoBehaviour
{
    public LayerMask unitLayerMask; // 유닛 레이어 마스크

    private Transform selectedUnit; // 선택된 유닛의 Transform

    private GameObject AttackRange;

    private Army selectedArmy = null;
    public bool isUnitSelected = false;

    // Army Info Panel for User
    GameObject ArmyInfoPanel;
    TMP_Text ArmyNameText;
    TMP_Text TotalSizeText;
    TMP_Text InfantrySizeText;
    TMP_Text ArcherSizeText;
    TMP_Text CavalrySizeText;
    TMP_Text ArmyMaintenanceText;
    TMP_Text Damage;

    Button DeleteButton;
    private EventSystem eventSystem;

    GoldSystem goldSystem;
    ManpowerSystem manpowerSystem;

    private void Start()
    {
        getArmyInfoPanel();
        

        eventSystem = EventSystem.current;
    }

    void getArmyInfoPanel()
    {
        ArmyInfoPanel = GameObject.Find("Canvas").transform.Find("ArmyInfoPanel").gameObject;
        ArmyNameText = ArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[0];
        TotalSizeText = ArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[1];
        InfantrySizeText = ArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[2];
        ArcherSizeText = ArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[3];
        CavalrySizeText = ArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[4];
        ArmyMaintenanceText = ArmyInfoPanel.GetComponentsInChildren<TMP_Text>()[5];

        Transform armyInfoTransform = GameObject.Find("Canvas").transform.Find("ArmyInfoPanel");
        if (armyInfoTransform != null)
        {
            DeleteButton = armyInfoTransform.Find("DeleteButton").GetComponent<Button>();
            DeleteButton.onClick.AddListener(onClickDeleteButton);
        }
    }

    void onClickDeleteButton()
    {
        if (selectedUnit != null)
        {
            goldSystem = FindObjectOfType<GoldSystem>();
            manpowerSystem = FindObjectOfType<ManpowerSystem>();

            goldSystem.ArmyMaintenance -= selectedArmy.TotalPrice();
            manpowerSystem.CalculateManpower((selectedArmy.TotalSize)*-1);

            Animator animator = selectedUnit.GetComponent<Animator>();

            //Destroy(selectedUnit.gameObject);
            selectedUnit.gameObject.SetActive(false);
            DeselectUnit();
            
        }
    }

    public void setArmy(Army Army)
    {
        selectedArmy = Army;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭 위치에 있는 UI 요소를 가져옵니다.
            GameObject clickedObject = eventSystem.currentSelectedGameObject;

            if (clickedObject == null || clickedObject != DeleteButton.gameObject)
            {
                // DeleteButton이 아닌 다른 UI 요소가 클릭된 경우에만 onClickUnit()을 호출합니다.
                onClickUnit();
            }
        }
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
            //Destroy(AttackRange);
        }
    }

    private void SelectUnit(Transform unitTransform)
    {
        selectedUnit = unitTransform;
        IsUnitSelected.instance.isSelected = true; // 외부 스크립트로 전달 위해 bool값 전달
        setArmyInfoPanel();

        // 선택된 유닛 하이라이트 표시
        if (selectedUnit != null)
        {
            Renderer selectedRenderer = selectedUnit.GetComponent<Renderer>();
            selectedRenderer.material.color = Color.white;
        }

        ArmyInfoPanel.SetActive(true);
        isUnitSelected = true;
        transform.Find("ShowAttackRange").gameObject.SetActive(true);
        //AttackRange = Instantiate(Resources.Load<GameObject>("Prefabs/ShowAttackRange"), selectedUnit.transform.position, Quaternion.identity);
    
    }

    private void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            GetComponent<UnitStat>().UnitColorChange();
            IsUnitSelected.instance.isSelected = false; // 외부 스크립트로 전달 위해 bool값 전달
        }
        selectedUnit = null;
        ArmyInfoPanel.SetActive(false);
        isUnitSelected = false;

        transform.Find("ShowAttackRange").gameObject.SetActive(false);
        //Destroy(AttackRange);
    }

    private void setArmyInfoPanel()
    {
        string ArmyName = selectedArmy.ID.ToString();
        string TotalSize = (selectedArmy.TotalSize * 1000).ToString();
        string InfantrySize = (selectedArmy.InfantrySize * 1000).ToString();
        string ArcherSize = (selectedArmy.ArcherSize * 1000).ToString();
        string CavalrySize = (selectedArmy.CavalrySize * 1000).ToString();
        string ArmyMaintenance = ((selectedArmy.TotalPrice() * -1).ToString());

        if (ArmyInfoPanel != null && TotalSizeText != null)
        {
            ArmyNameText.SetText(ArmyName + " 군");
            TotalSizeText.SetText(TotalSize + " 명");
            InfantrySizeText.SetText("보병 : " + InfantrySize + " 명");
            ArcherSizeText.SetText("궁병 : " + ArcherSize + " 명");
            CavalrySizeText.SetText("기병 : " + CavalrySize + " 명");
            ArmyMaintenanceText.SetText("유지비 : " + ArmyMaintenance);
        }
    }
}
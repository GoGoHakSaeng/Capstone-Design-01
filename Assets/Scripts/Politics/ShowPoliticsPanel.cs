using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WorldMapStrategyKit;
using YemaekHan;

public class ShowPoliticsPanel : MonoBehaviour
{
    public GameObject panel, buttonList;
    public Button upperButton;
    public TMP_Text countryNameText;
    public TMP_Text[] countryAttrText;

    private WMSK map;
    private GameObject[] ButtonObjects;
    private Button[] buttons;

    PublicSentimentSystem pstSystem;
    GoldSystem goldSystem;
    ManpowerSystem manpowerSystem;

    private void Start()
    {
        map = WMSK.instance; // 에셋 맵 로드

        pstSystem = FindAnyObjectByType<PublicSentimentSystem>();
        goldSystem = FindAnyObjectByType<GoldSystem>();
        manpowerSystem = FindAnyObjectByType<ManpowerSystem>();

        map.OnCountryClick += (int countryIndex, int regionIndex, int buttonIndex) => // 국가 클릭 시 이벤트 추가
        {
            if (IsUnitSelected.instance.isSelected == false)
            {
                if (buttonIndex == 1)
                {
                    TargetCountry.setTargetCountry(map.GetCountry(countryIndex));

                    PanelActivityChecker.instance.updateActivity(2);
                    if (TargetCountry.getTargetCountry == CountrySet.getUserCountry)
                    {
                        buttonList.SetActive(false);
                    }
                    else
                    {
                        buttonList.SetActive(true);
                    }
                    changePanelText();
                    panel.SetActive(true);
                }
            }
        };

        upperButton.onClick.AddListener(() => onClickButton()); // 버튼 클릭 이벤트 추가
        buttons = buttonList.GetComponentsInChildren<Button>();
        ButtonObjects = new GameObject[buttons.Length];

        for (int i = 0; i<buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() => addButtonFunction(buttons[index].GetComponentInChildren<TMP_Text>().text));
            ButtonObjects[i] = buttonList.transform.GetChild(i).gameObject;
        }
        buttons[0].interactable = false;
    }

    private void onClickButton()
    {
        if (TargetCountry.getTargetCountry == CountrySet.getUserCountry)
        {
            buttonList.SetActive(false);
        }
        else
        {
            buttonList.SetActive(true);
        }
        changePanelText();
        if (!panel.activeSelf)
        {
            panel.SetActive(false);
        } else
        {
            panel.SetActive(true);
        }
    }

    private void changePanelText()
    {
        Country country = TargetCountry.getTargetCountry;
        countryNameText.text = country.name;

        int currentPST = pstSystem.GetCurrentPublicSentiment();
        int currnetGold = goldSystem.GetCurrentGold();
        int currnetManpower = manpowerSystem.GetCurrentManpower();

        if (country == CountrySet.getUserCountry)
        {
            countryAttrText[0].text = currentPST.ToString() + " %";
            countryAttrText[1].text = currnetGold.ToString() + " 냥";
            countryAttrText[2].text = currnetManpower.ToString() + ",000 명";
        } else
        {
            countryAttrText[0].text = "알 수 없음";
            countryAttrText[1].text = "알 수 없음";
            countryAttrText[2].text = "알 수 없음";
        }
    }

    private void addButtonFunction(string text)
    {
        switch(text)
        {
            case "전쟁 선포":
                if (PoliticsButtonFunction.instance.declareWar())
                {
                    SetInteractableAll(buttons, false);
                    Debug.Log(CountrySet.getUserCountryName + "가 " + TargetCountry.getTargetCountryName + "에 선전포고함.");
                }
                else
                {
                    Debug.Log("선전포고 실패. 전쟁 명분화 필요");
                }
                break;
            case "동맹":
                PoliticsButtonFunction.instance.suggestAlliance();
                Debug.Log(CountrySet.getUserCountryName + "가 " + TargetCountry.getTargetCountryName + "에게 동맹을 제시함.");
                break;
            case "친선":
                Debug.Log(CountrySet.getUserCountryName + "가 " + TargetCountry.getTargetCountryName + "에게 친선활동을 함. " + TargetCountry.getTargetCountryName + "의 " + CountrySet.getUserCountryName + "에 대한 우호도가 10 증가");
                break;
            case "정당화":
                PoliticsButtonFunction.instance.justifyWar();
                buttons[0].interactable = true;
                Debug.Log(CountrySet.getUserCountryName + "가 " + TargetCountry.getTargetCountryName + "에 전쟁 명분을 만듦.");
                break;
            case "불가침":
                PoliticsButtonFunction.instance.concludeNonAggression();
                Debug.Log(CountrySet.getUserCountryName + "가 " + TargetCountry.getTargetCountryName + "와 불가침조약을 맺음");
                SetInteractable(buttons[0]);
                SetInteractable(buttons[3]);
                break;
        }
    }

    private void SetInteractable(Button button)
    {
        bool buttonState = button.interactable;
        if (buttonState)
        {
            buttonState = false;
        } else
        {
            buttonState = true;
        }
    }

    private void SetInteractableAll(Button[] AllButtons, bool state)
    {
        for (int i = 0; i < AllButtons.Length; i++)
        {
            AllButtons[i].interactable = state;
        }
    }
}

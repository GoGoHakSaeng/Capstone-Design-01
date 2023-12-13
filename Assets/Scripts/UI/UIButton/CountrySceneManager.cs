using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class CountrySceneManager : MonoBehaviour
{
    public List<CountryChoiceType> currentTypes = new List<CountryChoiceType>();
    public List<Button> CountryButtons = new List<Button>();
    public int CountryIDindex = -1;

    public CountryChange CC;
    public Button confirmButton;
    public TMP_Text CountryNameText;

    string countryName = ""; // 패널 국가이름 설정
    private bool buttonClicked = false;

    private void Start()
    {
        // 처음에 확인 버튼은 비활성화
        confirmButton.interactable = false;
        confirmButton.onClick.AddListener(OnConfirmButtonClick);

        for (int i=0; i<CountryButtons.Count; i++)
        {
            int index = i;
            CountryButtons[i].onClick.AddListener(() => OnBtnClick(index));
        }
    }

    public void OnBtnClick(int index)
    {
        if (CountryIDindex != index)
        {
            CountryIDindex = index;
            switch (currentTypes[index])
            {
                case CountryChoiceType.Goguryeo:
                    CC.countryID = 0;
                    countryName = "고구려";
                    break;
                case CountryChoiceType.Baekje:
                    CC.countryID = 1;
                    countryName = "백제";
                    break;
                case CountryChoiceType.Silla:
                    CC.countryID = 3;
                    countryName = "신라";
                    break;
                case CountryChoiceType.Gaya:
                    CC.countryID = 2;
                    countryName = "가야";
                    break;
            }

            CountryNameText.SetText(countryName);

            // 버튼이 눌린 후에 확인 버튼 활성화
            confirmButton.interactable = true;
        }
    }

    private static CountrySceneManager instance;

    public static CountrySceneManager Instance
    {
        get { return instance; }
    }

    private void OnConfirmButtonClick()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        // 확인 버튼 클릭 시 실행될 코드
        if (CC.countryID != 5)
        {
            CC.call();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoiceBtnType : MonoBehaviour
{
    public CountryChoiceType currentType;

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
    }

    public void OnBtnClick()
    {
        if (!buttonClicked)
        {
            switch (currentType)
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
            buttonClicked = true;

            // 버튼이 눌린 후에 확인 버튼 활성화
            confirmButton.interactable = true;
        }
    }

    public GameManager gameManager;

    private void OnConfirmButtonClick()
    {
        // 확인 버튼 클릭 시 실행될 코드
        if (CC.countryID != 5)
        {
            CC.call();
        }

        Debug.Log("CC.countryId : " + CC.countryID);
    }
}

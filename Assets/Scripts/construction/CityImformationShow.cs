using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;
using TMPro;

namespace YemaekHan
{
    public class CityImformationShow : MonoBehaviour
    {
        public GameObject targetPanel;
        public Button targetButton;
        public GameObject relatedPanel;
        public TMP_Text CityText;
        public Image[] conImageList;

        private WMSK map;
        private int selectedCity = -1;

        private void Start()
        {
            map = WMSK.instance;

            map.OnCityClick += (int cityIndex, int buttonIndex) =>
            {
                if (PanelActivityChecker.instance.isArmyPanelOn == false && IsUnitSelected.instance.isSelected == false)
                {
                    if (buttonIndex == 0)
                    {
                        CityText.SetText("City name");
                        selectedCity = cityIndex;
                        TargetCity.setTargetCity(map.GetCity(cityIndex));

                        bool isActive;
                        if (TargetCity.getTargetCity.countryIndex == CountrySet.getUserCountryIndex)
                        {
                            isActive = true;

                        }
                        else
                        {
                            isActive = false;
                        }

                        PanelActivityChecker.instance.updateActivity(1);
                        relatedPanel.SetActive(isActive);
                        targetButton.interactable = isActive;
                        OnClick();

                        map.ToggleCityHighlight(cityIndex, Color.cyan, true);
                    }
                }
            };
        }

        private void Update()
        {
            if (map.input.GetKeyDown(KeyCode.Mouse1))
            {
                if (selectedCity != -1)
                {
                    targetPanel.SetActive(false);
                    map.ToggleCityHighlight(selectedCity, Color.cyan, false);
                    selectedCity = -1;
                    CityText.SetText("City name");
                    TargetCity.clearTargetCity();
                    targetButton.interactable = false;
                    relatedPanel.SetActive(false);
                    
                }
            }

            if (TargetCity.getTargetCity != null)
            {
                if (TargetCity.getTargetCity.countryIndex == CountrySet.getUserCountryIndex)
                {
                    ChangeImage(TargetCity.getTargetCityJSON.ToDictionary(), conImageList);
                } else
                {
                    ChangeUnknown(conImageList);
                }
            }
        }

        private void OnClick()
        {
            targetPanel.SetActive(true);
            CityText.SetText(TargetCity.getTargetCity.name);
            ChangeImage(TargetCity.getTargetCityJSON.ToDictionary(), conImageList);
        }

        private void ChangeImage(Dictionary<string, string> dic, Image[] imgList)
        {
            int i = 0;

            foreach (KeyValuePair<string, string> kvp in dic)
            {
                switch (kvp.Value)
                {
                    case "farm":
                        imgList[i].sprite = Resources.Load<Sprite>("Images/barn");
                        break;
                    case "barrack":
                        imgList[i].sprite = Resources.Load<Sprite>("Images/camp");
                        break;
                    case "trading":
                        imgList[i].sprite = Resources.Load<Sprite>("Images/revenue");
                        break;
                }
                i++;
            }
            if (i <= 9)
            {
                for (int j = i; j < 10; j++)
                {
                    imgList[j].sprite = Resources.Load<Sprite>("Images/remove");
                }
            }
        }

        private void ChangeUnknown(Image[] imgList)
        {
            for (int i=0; i < imgList.Length; i++)
            {
                imgList[i].sprite = Resources.Load<Sprite>("Images/help");
            }
        }
    }
}
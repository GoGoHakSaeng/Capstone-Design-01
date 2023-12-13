using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WorldMapStrategyKit;
using YemaekHan;

public class SelectCity : MonoBehaviour
{
    //public Button CityNameButton;
    public TMP_Text CityNameText;
    public Button SummonButton;

    private WMSK map;

    public string CityName;
    public int selectedCity = -1;

    void Start()
    {
        map = WMSK.instance;
    }
    
    public void onClickSelectBtn(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            CityNameText.text = "도시를 선택해주세요.";
            map.OnCityClick += cityClick;
        }
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            selectedCity = -1;
            CityName = null;
            CityNameText.text = "-----";
        }
    }  

    void cityClick(int cityIndex, int buttonIndex)
    {
        selectedCity = cityIndex;
        TargetCity.setTargetCity(map.GetCity(cityIndex));

        
        if (TargetCity.getTargetCity.countryIndex == CountrySet.getUserCountryIndex)
        {
            CityName = map.GetCity(cityIndex).name;
            CityNameText.text = CityName;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YemaekHan;

public class DestroyConstruction : MonoBehaviour
{
    public Button[] iconList;

    private void Start()
    {
        for (int i=0; i<iconList.Length; i++)
        {
            int index = i;
            iconList[index].onClick.AddListener(() => onIconsClick(index));
        }
    }

    private void onIconsClick(int index)
    {
        if (TargetCity.getTargetCity.countryIndex == CountrySet.getUserCountryIndex)
        {
            string slot = "slot" + index;

            if (!TargetCity.getTargetCityJSON.GetField(slot))
            {
                return;
            }

            TargetCity.getTargetCityJSON.RemoveField(slot);

            int count = TargetCity.getTargetCityJSON.Count;
            int nowIndex = index + 1;

            while (count >= nowIndex)
            {
                string nextSlot = "slot" + nowIndex;
                string nowSlot = "slot" + (nowIndex - 1);
                TargetCity.getTargetCityJSON.RenameKey(nextSlot, nowSlot);
                nowIndex++;
            }
        }
    }
    // 일단 존재하면 다음단계, 존재하지 않으면 넘어가지 않음
    // 해당 위치의 버튼을 누를 시, 해당 번호의 건물을 파괴함
    // 파괴한 후, 다음 번호의 건물을 해당 번호로 넘김
}

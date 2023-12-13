using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeCountryImage : MonoBehaviour
{
    Image countryImage;
    public Sprite[] ImageList;
    public void Awake()
    {
        countryImage = GetComponent<Image>();
    }

    public void OnStartScene(int index)
    {
        
        switch (index)
        {
            case 0:
                countryImage.sprite = ImageList[0]; break;
            case 1:
                countryImage.sprite = ImageList[1]; break;
            case 2:
                countryImage.sprite = ImageList[2]; break;
            case 3:
                countryImage.sprite = ImageList[3]; break;
            default:
                countryImage.sprite = ImageList[0]; break;
        }
    }
}

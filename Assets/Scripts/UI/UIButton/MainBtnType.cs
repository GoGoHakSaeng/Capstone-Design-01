using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainBtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public mainButtonType currentType;
    public Transform ButtonScale;
    Vector3 defaultScale;
    // Start is called before the first frame update
    private void Start()
    {
        defaultScale = ButtonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case mainButtonType.New:
                SceneLoad.LoadSceneHandler("CountyChoiceScene", 0);
                break;
            case mainButtonType.Continue:
                SceneManager.LoadScene("CountyChoiceScene");
                break;
            case mainButtonType.Quit:
                Application.Quit();
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonScale.localScale = defaultScale;
    }
}

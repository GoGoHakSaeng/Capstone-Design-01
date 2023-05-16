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
                Debug.Log("새게임");
                SceneLoad.LoadSceneHandler("InGameScene", 0); // 0 새로하기
                break;
            case mainButtonType.Continue:
                Debug.Log("이어하기");
                SceneManager.LoadScene("InGameScene"); // 1 불러오기
                break;
            case mainButtonType.Quit:
                Debug.Log("나가기");
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

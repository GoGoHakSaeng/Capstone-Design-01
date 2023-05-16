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
                Debug.Log("������");
                SceneLoad.LoadSceneHandler("InGameScene", 0); // 0 �����ϱ�
                break;
            case mainButtonType.Continue:
                Debug.Log("�̾��ϱ�");
                SceneManager.LoadScene("InGameScene"); // 1 �ҷ�����
                break;
            case mainButtonType.Quit:
                Debug.Log("������");
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

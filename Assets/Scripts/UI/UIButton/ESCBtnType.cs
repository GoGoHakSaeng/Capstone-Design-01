using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCBtnType : MonoBehaviour
{
    public escButtonType currentType;
    // Start is called before the first frame update
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case escButtonType.Resume:
                Debug.Log("�̾��ϱ�");
                // GamgeManager���� �˾Ƽ� ó��
                break;
            case escButtonType.GoMain:
                Debug.Log("���θ޴���");
                SceneManager.LoadScene("StartScene");
                break;
            case escButtonType.Quit:
                Debug.Log("������");
                Application.Quit();
                break;
        }
    }
}

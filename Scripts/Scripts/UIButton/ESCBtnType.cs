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
                Debug.Log("이어하기");
                // GamgeManager에서 알아서 처리
                break;
            case escButtonType.GoMain:
                Debug.Log("메인메뉴로");
                SceneManager.LoadScene("StartScene");
                break;
            case escButtonType.Quit:
                Debug.Log("나가기");
                Application.Quit();
                break;
        }
    }
}

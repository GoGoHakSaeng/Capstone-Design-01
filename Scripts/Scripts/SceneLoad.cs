using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar; // 로딩 상황을 보여주는 슬라이더
    public Text loadtext; // 로딩 텍스트
    public static string loadScene; // 불러올 씬
    public static int loadType; // 새로불러올지 아닐지
    private void Start()
    {
        StartCoroutine(startLoading()); // 코루틴 : 반복적으로 코드 실행
    }

    public static void LoadSceneHandler(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator startLoading()
    {
        yield return null; // 코루틴 사용시 반드시 존재, 다음 프레임까지 대기
        AsyncOperation operation = SceneManager.LoadSceneAsync("InGameScene"); //씬 비동기 로드
        operation.allowSceneActivation = false; // InGameScene 비활성화

        while(!operation.isDone) // 씬 로드가 완료되지 않으면
        {
            yield return null;

            if (loadType == 0) // 0일때 새로
            {
                Debug.Log("새로하기");
            } else if (loadType == 1) // 1일때 불러오기
            {
                Debug.Log("불러오기");
            }

            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime); //슬라이더 값이 0.9까지 서서히 증가
            }

            if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime); // 슬라이더 값 1로 서서히 증가(완료)
            }
            else if (progressbar.value >= 1f)
            {
                loadtext.text = "Loading Success"; //로딩 완료시 텍스트 변경
            }

            if(progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true; // 씬 활성화
            }
        }
    }
}

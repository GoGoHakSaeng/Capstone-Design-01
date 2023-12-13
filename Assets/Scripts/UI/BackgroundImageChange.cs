using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageChange : MonoBehaviour
{
    private GameObject[] background;
    private List<Sprite> imagelist;
    //private float time = 0f;
    private int turn = 0;
    private int index;

    private void Start()
    {
        background = new GameObject[] // 백그라운드 오브젝트 지정
        {
            transform.GetChild(0).gameObject,
            transform.GetChild(1).gameObject
        };

        imagelist = new List<Sprite> // 백그라운드 이미지 호출
        {
            Resources.Load<Sprite>("Images/background/1"),
            Resources.Load<Sprite>("Images/background/2"),
            Resources.Load<Sprite>("Images/background/3"),
            Resources.Load<Sprite>("Images/background/4"),
            Resources.Load<Sprite>("Images/background/5"),
            Resources.Load<Sprite>("Images/background/6"),
            Resources.Load<Sprite>("Images/background/7"),
            Resources.Load<Sprite>("Images/background/8")
        };

        index = Random.Range(0, imagelist.Count); // 랜덤 숫자 지정
        background[0].GetComponent<Image>().sprite = imagelist[index]; // 랜덤한 이미지 설정
        background[1].GetComponent<Image>().sprite = imagelist[(index+1)%imagelist.Count];
        background[1].SetActive(false); // 두 번째 백그라운드 비활성화

        StartCoroutine(FadeCoroutine()); // 특정 시간마다 반복할 함수 실행
        index++;
    }


    IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(10.0f); // 10초 딜레이
        index = (index + 1) % imagelist.Count; // 이미지 인덱스 변화
        turn = (turn + 1) % 2; // 백그라운드 순서 변화
        StartCoroutine(FadeInCoroutine(background)); // 백그라운드 변경 함수 실행
        StartCoroutine(FadeCoroutine()); // 재귀
    }

    IEnumerator FadeInCoroutine(GameObject[] back) // 백그라운드 오브젝트를 받아 해당 순서에 맞게 백그라운드를 활성화시키는 함수
    {
        back[turn].SetActive(true); // 보일 백그라운드 활성화
        float fadeCount = 1; // 알파값 지정
        while (fadeCount > 0f) // 1초동안 지속
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            back[turn].GetComponent<Image>().color = new Color(255, 255, 255, 1.0f - fadeCount); // 해당 백그라운드를 서서히 보이게 함
            back[(turn + 1) % 2].GetComponent<Image>().color = new Color(255, 255, 255, fadeCount); // 해당 백그라운드를 서서히 안보이게 함
        }
        back[(turn + 1) % 2].SetActive(false); // 안보이게 된 백그라운드 비활성화
        back[(turn + 1) % 2].GetComponent<Image>().sprite = imagelist[index]; // 안보이게 된 백그라운드 이미지 변경
    }
}

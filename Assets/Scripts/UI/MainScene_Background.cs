using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene_Background : MonoBehaviour
{
    public List<Image> Backgrounds;


    float changeInterval = 5f;
    int currentIndex = 0;
    float timer = 0f;

    void Start()
    {
        currentIndex = 0; 
        Backgrounds[currentIndex].gameObject.SetActive(true);
        if (Backgrounds.Count > 1)
        {
            Backgrounds[(currentIndex + 1) % Backgrounds.Count].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 일정 시간마다 배경 이미지 변경
        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            ChangeBackgroundImage();
            timer = 0f;
        }
    }

    void ChangeBackgroundImage()
    {
        if (Backgrounds.Count > 0)
        {
            Backgrounds[currentIndex].gameObject.SetActive(false);

            currentIndex = (currentIndex + 1) % Backgrounds.Count;

            Backgrounds[currentIndex].gameObject.SetActive(true);
        }
    }
}

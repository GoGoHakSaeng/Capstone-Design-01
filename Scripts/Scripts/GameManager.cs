using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ESCMenu;
    public static bool isGameStop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC ∏ﬁ¥∫
        {
            if (ESCMenu.activeSelf == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
    public void Resume()
    {
        Debug.Log("∞Ëº”");
        ESCMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Debug.Log("∏ÿ√„");
        ESCMenu.SetActive(true);
        Time.timeScale = 0;
    }
}

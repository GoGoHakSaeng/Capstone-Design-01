using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class GameOver : MonoBehaviour
{
    public Button QuitButton;
    void Start()
    {
        WMSK.instance.paused = true;
        QuitButton.onClick.AddListener(() => onQuitButtonClick());
    }

    
    void onQuitButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}

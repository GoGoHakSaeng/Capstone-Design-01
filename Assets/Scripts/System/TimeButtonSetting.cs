using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class TimeButtonSetting : MonoBehaviour
{
    public Button StopButton;
    public TMP_Text StopButtonText;
    public List<Button> TimeSpeedButtons;
    public List<float> Speeds;

    bool isStop = false;
    int selectedButtonIndex = 0;

    private void Start()
    {
        StopButton.onClick.AddListener(() => TimeStop());

        onSpeedClick(selectedButtonIndex);
        for(int i=0; i < TimeSpeedButtons.Count; i++)
        {
            int index = i;
            TimeSpeedButtons[i].onClick.AddListener(() => onSpeedClick(index));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeStop();
        }
    }

    void onSpeedClick(int index)
    {
        if (selectedButtonIndex >= 0 && selectedButtonIndex < TimeSpeedButtons.Count)
        {
            ChangeButtonColor(TimeSpeedButtons[selectedButtonIndex], new Color(30 / 255f, 30 / 255f, 30 / 255f));
        }

        ChangeButtonColor(TimeSpeedButtons[index], Color.gray);

        // 선택한 버튼의 인덱스를 저장합니다.
        selectedButtonIndex = index;

        WMSK.instance.timeSpeed = Speeds[index];
    }

    void TimeStop()
    {
        if (isStop == false) // Stop
        {
            StopButtonText.SetText("계속");
            isStop = true;
        }
        else // Continue
        {
            StopButtonText.SetText("정지");
            isStop = false;
        }
        WMSK.instance.paused = isStop;
    }

    void ChangeButtonColor(Button button, Color color)
    {
        Image buttonColor = button.GetComponent<Image>();
        if (buttonColor != null)
        {
            buttonColor.color = color;
        }
    }
}

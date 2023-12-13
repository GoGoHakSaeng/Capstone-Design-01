using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlarmHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject descriptionPanel; // 설명 패널 또는 텍스트 UI를 연결할 변수
    private bool isMouseOver = false;

    private void Start()
    {
        // 설명 패널 또는 텍스트 UI를 비활성화 상태로 시작합니다.
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }

    // 마우스가 패널 위로 진입할 때 호출됩니다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        ShowDescription();
    }

    // 마우스가 패널을 빠져나갈 때 호출됩니다.
    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        HideDescription();
    }

    // 패널 위로 마우스가 진입하면 호출되는 함수
    private void ShowDescription()
    {
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(true);
            // 여기에서 설명 패널 또는 텍스트 내용을 설정하거나 업데이트합니다.
        }
    }

    // 패널을 빠져나가면 호출되는 함수
    private void HideDescription()
    {
        if (!isMouseOver && descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }
}

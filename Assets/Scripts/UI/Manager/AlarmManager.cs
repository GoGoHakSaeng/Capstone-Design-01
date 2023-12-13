using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    public GameObject AlarmsPanel;
    public GameObject constructionAlarmPrefab;
    public GameObject nationFocusAlarmPrefab;

    private List<GameObject> activeAlarms = new List<GameObject>();

    public bool isDuringConstruction { get; set; }
    public bool isDuringNationalFocus { get; set; }

    private float xOffset = 65f; // 알람들 사이의 세로 간격 조절

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 알람을 업데이트합니다.
        UpdateAlarms();
    }

    void CreateAlarm(GameObject alarmPrefab, bool isActive)
    {
        if (!isActive)
        {
            // 알람이 비활성화된 경우 해당 알람을 삭제하고 리스트에서 제거합니다.
            RemoveAlarm(alarmPrefab);
            return;
        }

        if (!IsAlarmActive(alarmPrefab))
        {
            // 알람을 생성하고 위치를 조절합니다.
            GameObject newAlarm = Instantiate(alarmPrefab, AlarmsPanel.transform);
            newAlarm.name = alarmPrefab.name;
            
            activeAlarms.Add(newAlarm);
            PositionAlarm();
        }
    }

    bool IsAlarmActive(GameObject alarmPrefab)
    {
        return activeAlarms.Exists(alarm => alarm.name == alarmPrefab.name);
    }

    float xPos = 0f;
    void PositionAlarm()
    {
        for (int i = 0; i < activeAlarms.Count; i++)
        {
            GameObject alarm = activeAlarms[i];
            if (alarm != null)
            {
                Vector3 position = new Vector3(xOffset * i, 0f, 0f);
                alarm.transform.localPosition = position;
            }
        }
    }

    void RemoveAlarm(GameObject alarmPrefab)
    {
        // 알람을 삭제하고 리스트에서 제거합니다.
        GameObject alarmToRemove = activeAlarms.Find(alarm => alarm.name == alarmPrefab.name);
        if (alarmToRemove != null)
        {
            activeAlarms.Remove(alarmToRemove);
            Destroy(alarmToRemove);
            xPos -= xOffset;
        }
    }

    void UpdateAlarms()
    {
        // 현재 활성화된 알람 목록을 확인하고 비활성화되었으면 제거합니다.
        for (int i = activeAlarms.Count - 1; i >= 0; i--)
        {
            GameObject alarm = activeAlarms[i];
            if (alarm == null)
            {
                activeAlarms.RemoveAt(i);
            }
        }

        // 현재 상태에 따라 새 알람을 생성합니다.
        CreateAlarm(constructionAlarmPrefab, !isDuringConstruction);
        CreateAlarm(nationFocusAlarmPrefab, !isDuringNationalFocus);
    }
}

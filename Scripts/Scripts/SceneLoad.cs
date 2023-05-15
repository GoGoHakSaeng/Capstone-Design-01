using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar; // �ε� ��Ȳ�� �����ִ� �����̴�
    public Text loadtext; // �ε� �ؽ�Ʈ
    public static string loadScene; // �ҷ��� ��
    public static int loadType; // ���κҷ����� �ƴ���
    private void Start()
    {
        StartCoroutine(startLoading()); // �ڷ�ƾ : �ݺ������� �ڵ� ����
    }

    public static void LoadSceneHandler(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator startLoading()
    {
        yield return null; // �ڷ�ƾ ���� �ݵ�� ����, ���� �����ӱ��� ���
        AsyncOperation operation = SceneManager.LoadSceneAsync("InGameScene"); //�� �񵿱� �ε�
        operation.allowSceneActivation = false; // InGameScene ��Ȱ��ȭ

        while(!operation.isDone) // �� �ε尡 �Ϸ���� ������
        {
            yield return null;

            if (loadType == 0) // 0�϶� ����
            {
                Debug.Log("�����ϱ�");
            } else if (loadType == 1) // 1�϶� �ҷ�����
            {
                Debug.Log("�ҷ�����");
            }

            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime); //�����̴� ���� 0.9���� ������ ����
            }

            if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime); // �����̴� �� 1�� ������ ����(�Ϸ�)
            }
            else if (progressbar.value >= 1f)
            {
                loadtext.text = "Loading Success"; //�ε� �Ϸ�� �ؽ�Ʈ ����
            }

            if(progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true; // �� Ȱ��ȭ
            }
        }
    }
}

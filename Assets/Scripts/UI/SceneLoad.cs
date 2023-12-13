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
        AsyncOperation operation = SceneManager.LoadSceneAsync("InGameScene"); //�� �񵿱� �ε�
        operation.allowSceneActivation = false; // InGameScene ��Ȱ��ȭ

        while(!operation.isDone) // �� �ε尡 �Ϸ���� ������
        {
            Debug.Log("오퍼레이션 프로그레스 " + operation.progress);
            progressbar.value = operation.progress;

            if (operation.progress >= 0.9f)
            {
                loadtext.text = "Loading Successed";
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }
}

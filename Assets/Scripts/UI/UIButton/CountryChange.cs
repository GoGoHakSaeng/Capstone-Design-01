using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountryChange : MonoBehaviour
{
    public int countryID;
    public GameObject countryIDObject;
    //public static CountryChange countrychange;

    // Start is called before the first frame update
    public void call()
    {
        SceneManager.LoadScene("InGameScene");
        DontDestroyOnLoad(countryIDObject);
    }
}

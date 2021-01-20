using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMind : MonoBehaviour
{

    private void Start()
    {
        GameObject.Find("MaxScore").GetComponent<TextMesh>().text = PlayerPrefs.GetInt("maxScore").ToString();
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MasterMind : MonoBehaviour
{
    public float start;
    public float end;
    public GameObject wood;
    public int score;
    public int combo;

    public GameObject scoreText;
    public GameObject comboText;
    public GameObject timerMask;

    public float time;

    public GameObject timer;


    // Update is called once per frame
    void Update()
    {
        start += Time.deltaTime;
        time -= Time.deltaTime;

        if (start > end)
        {
            int food = Random.Range(0, 38);

            Vector3 foodPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 3.5f;
            GameObject newFood = Instantiate(Resources.Load($"Prefabs/Foods/{food}"), foodPos, Quaternion.identity) as GameObject;
            newFood.AddComponent<FoodMind>();
            newFood.transform.SetParent(wood.transform);
            start = 0f;
        }

        //updating ui
        scoreText.GetComponent<TextMesh>().text = $"{score}";
        comboText.GetComponent<TextMesh>().text = $"x{combo}";
        if (score > PlayerPrefs.GetInt("maxScore")) PlayerPrefs.SetInt("maxScore", score);

        if (comboText.GetComponent<TextMesh>().text == "x1") comboText.GetComponent<MeshRenderer>().enabled = false;
        else comboText.GetComponent<MeshRenderer>().enabled = true;

        timerMask.GetComponent<RectTransform>().sizeDelta = new Vector2(timer.GetComponent<RectTransform>().sizeDelta.x * time / 10, timerMask.GetComponent<RectTransform>().sizeDelta.y);

        if (timerMask.GetComponent<RectTransform>().sizeDelta.x < 0f)
            Death();




    }

    public void Death()
    {
        SceneManager.LoadScene("Menu");
    }

}

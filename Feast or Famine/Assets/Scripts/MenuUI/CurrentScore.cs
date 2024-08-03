using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    private TMP_Text score;
    public GameObject[] food; 
    public bool isAtBase = false;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
        score.text = "Resources: " + PlayerPrefs.GetInt("Resources").ToString("");
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Resources: " + PlayerPrefs.GetInt("Resources").ToString("");
        if (isAtBase)
        {
            if (PlayerPrefs.GetInt("Resources") > 0)
                food[0].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 50)
                food[1].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 100)
                food[2].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 200)
                food[3].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 300)
                food[4].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 400)
                food[5].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 500)
                food[6].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 600)
                food[7].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 700)
                food[8].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 800)
                food[9].SetActive(true);
            if (PlayerPrefs.GetInt("Resources") > 900)
                food[10].SetActive(true);
        }
    }


}

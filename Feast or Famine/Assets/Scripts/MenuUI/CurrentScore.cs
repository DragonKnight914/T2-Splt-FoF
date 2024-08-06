using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    private TMP_Text score;
    public int highScore;
    public GameObject[] food; 
    public GameObject fadeOut;
    public bool isAtBase = false;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
        score.text = ":" + PlayerPrefs.GetInt("Resources").ToString("");
        highScore = PlayerPrefs.GetInt("Resources");
        if (highScore > PlayerPrefs.GetInt("TotalResources"))
        {
            PlayerPrefs.SetInt("TotalResources", highScore);
        }


    }

    // Update is called once per frame
    void Update()
    {
        score.text = ":" + PlayerPrefs.GetInt("Resources").ToString("");
        if (PlayerPrefs.GetInt("Resources") < 0)
        {
            StartCoroutine(GameOver());
            
        }
        if (isAtBase)
        {
            for(int i = 0;  i < 1000; i+= 100)
            {
                if(PlayerPrefs.GetInt("Resources") > i)
                {
                    for (int j = 0;  j < 11; j++)
                    {
                        if(j == (i / 100))
                            food[j].SetActive(true);
                        else
                            food[j].SetActive(false);
                    }
                }
            }
            /*if (PlayerPrefs.GetInt("Resources") > 0)
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
                food[10].SetActive(true);*/
        }
    }

    private IEnumerator GameOver()
    {
        fadeOut.SetActive(true);
        PlayerPrefs.SetInt("GameOver", 1);
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("M - Menu");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public GameObject[] rankings;
    public GameObject gameOverScreen;
    public GameObject MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("GameOver") == 1)
        {
            gameOverScreen.SetActive(true);
            MainMenu.SetActive(false);
        }
        if (PlayerPrefs.GetInt("TotalResources") < 100)
            rankings[5].SetActive(true);
        else if (PlayerPrefs.GetInt("TotalResources") <= 300 && 
        PlayerPrefs.GetInt("TotalResources") > 100)
            rankings[4].SetActive(true);
        else if (PlayerPrefs.GetInt("TotalResources") <= 500 && 
        PlayerPrefs.GetInt("TotalResources") > 300)
            rankings[3].SetActive(true);
        else if (PlayerPrefs.GetInt("TotalResources") <= 1000 && 
        PlayerPrefs.GetInt("TotalResources") > 500)
            rankings[2].SetActive(true);
        else if (PlayerPrefs.GetInt("TotalResources") <= 1500 && 
        PlayerPrefs.GetInt("TotalResources") > 1000)
            rankings[1].SetActive(true);
        else if (PlayerPrefs.GetInt("TotalResources") >= 3000)
            rankings[1].SetActive(true);

    }

    public void StopGameOver()
    {
        PlayerPrefs.SetInt("GameOver", 0);
    }
}

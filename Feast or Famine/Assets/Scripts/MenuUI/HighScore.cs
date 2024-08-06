using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    //public CurrentScore cs;
    private TMP_Text hs; 
    // Start is called before the first frame update
    void Start()
    {
        hs = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hs.text = ":" + PlayerPrefs.GetInt("TotalResources").ToString("");
    }
}

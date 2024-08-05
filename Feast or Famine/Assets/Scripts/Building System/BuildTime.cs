using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class BuildTime : MonoBehaviour
{
    public int time = 120;
    public bool canBuild = false;

    [SerializeField] TextMeshProUGUI timeToSpent;
    [SerializeField] GameObject buildText;
    [SerializeField] GameObject defenseText;
    [SerializeField] GameObject WallUI;
    [SerializeField] GameObject TowerUI;
    [SerializeField] Button buildMode;

    //Begin Defense Phase Button
    public bool canFastFoward = false;

    //Day - night cycle
    public PostProcessVolume m_Volume;


    // Start is called before the first frame update
    void Start()
    {
        time = PlayerPrefs.GetInt("PhaseTimer");
        UpdateTimeText();
        
        //buildMode.interactable = false;
        StartCoroutine(Countdown());
    }

    void Update()
    {
        if (canFastFoward)
            Time.timeScale = 20;
        else
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
            PlayerPrefs.SetInt("PhaseTimer", time);

            if (time <= 0)
            {
                if (canBuild == false)
                {
                    canBuild = true;
                    ChangeSceneState.instance.canGetOtherScenes = true;
                    buildMode.interactable = true;
                    buildText.SetActive(true);
                    defenseText.SetActive(false);
                    WallUI.SetActive(false);
                    TowerUI.SetActive(false);
                    time = 120;
                    PlayerPrefs.SetInt("PhaseTimer", time);
                }
                else if (canBuild == true)
                {
                    canFastFoward = false;
                    canBuild = false;
                    ChangeSceneState.instance.canGetOtherScenes = false;
                    buildMode.interactable = false;
                    buildText.SetActive(false);
                    defenseText.SetActive(true);
                    time = 60;
                    PlayerPrefs.SetInt("PhaseTimer", time);
                    GridBuildingSystem.instance.ExitBuildMode();
                }
            }

            UpdateTimeText();
        }
    }

    public void FastFoward()
    {
        canFastFoward = true;
    }

    void UpdateTimeText()
    {
        int minutes = time / 60;
        int seconds = time % 60;
        timeToSpent.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

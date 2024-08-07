using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCheck : MonoBehaviour
{
    public Movement P;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("HasDoneTutorial") == 1)
        {
            this.gameObject.SetActive(false);
            P.isPaused = false;
        }
        else
        {
            P.isPaused = true;
        }


    }

    public void SkipTutorial()
    {
        PlayerPrefs.SetInt("HasDoneTutorial", 1);
        P.isPaused = false;
    }

    public void EndTutorial()
    {
        PlayerPrefs.SetInt("HasDoneTutorial", 1);
        P.isPaused = false;
    }
}

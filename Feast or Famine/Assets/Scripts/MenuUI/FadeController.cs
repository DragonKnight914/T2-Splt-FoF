using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;
    public GameObject fade;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("CanEnterArea") == 0 && PlayerPrefs.GetInt("GameOver") == 0
        && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("M - BaseArea 2"))
        {
            
            fade.SetActive(false);
            //Debug.Log(fade.activeSelf);
        }
        else if (PlayerPrefs.GetInt("CanEnterArea") == 0 && PlayerPrefs.GetInt("GameOver") == 1
        && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("M - BaseArea 2"))
            StartCoroutine(GameEnd());
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(0.1f);
        fade.SetActive(false);

    }
    
}


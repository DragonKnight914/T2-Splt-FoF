using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cornucopia : MonoBehaviour
{
    private Player P;
    
    //UI
    public TextMeshProUGUI  scoreUI;

    //sfx
    [SerializeField] private AudioClip[] NoteClip = null;
    [SerializeField] private AudioSource Sounds;
    [SerializeField] private int points; 

    //Transition
    [SerializeField] private GameObject fadeOut; 
    public string _base;

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player").GetComponent<Player>();
        points *= PlayerPrefs.GetInt("RoundScaling") + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            P.score += points;
            scoreUI.text = ":" + P.score;
            PlayerPrefs.SetInt("Resources", P.score);
            //int soundPlayed = Random.Range(0, 5);
            Sounds.PlayOneShot(NoteClip[0], 0.5f);
            fadeOut.SetActive(true);
            StartCoroutine(ReturnToBase());
            Debug.Log("Coroutine Start");
            P.inDialog = true;
            //Destroy(this.gameObject);
            
        }

 
    }

    

    public IEnumerator ReturnToBase()
    {
        Debug.Log("Loading Base1");
        yield return new WaitForSeconds(3f);
        Debug.Log("Loading Base");
        SceneManager.LoadScene(_base);
    }
}

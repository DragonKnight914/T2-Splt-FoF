using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicNotes : MonoBehaviour
{
    private Player P;
    
    //UI
    public TextMeshProUGUI  scoreUI;

    //sfx
    [SerializeField] private AudioClip[] NoteClip = null;
    [SerializeField] private AudioSource Sounds;
    [SerializeField] private int points; 

    // Start is called before the first frame update
    void Start()
    {
        P = GameObject.Find("Player").GetComponent<Player>();
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
            Destroy(this.gameObject);
        }

 
    }
    
}

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
            P.score += 5;
            scoreUI.text = "Score: " + P.score;
            int soundPlayed = Random.Range(0, 5);
            Sounds.PlayOneShot(NoteClip[soundPlayed], 0.5f);
            Destroy(this.gameObject);
        }

 
    }
    
}

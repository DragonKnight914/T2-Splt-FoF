using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class TriggerEndCutscene : MonoBehaviour
{
    public VideoPlayer m_VideoPlayer;
    public GameObject vidHolder;
    public GameObject Canvas;
    public GameObject VidCanvas;
    public GameObject Map;
    public GameObject Player;
    public GameObject fadeOut;
    public AudioSource music;
    public GameObject otherTracks;
    public GameObject Thanks;
    public bool canPlayVid;

    // Start is called before the first frame update
    void Start()
    {
        //m_VideoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((m_VideoPlayer.frame) > 0 && (m_VideoPlayer.isPlaying == false))
        {
            //Video has finshed playing!
            Debug.Log("Finished");
            Thanks.SetActive(true);
            vidHolder.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            music.Stop();
            otherTracks.SetActive(false);
            fadeOut.SetActive(true);
            StartCoroutine(StartCutscene());
        }
    }

    private IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(4.7f);
        VidCanvas.SetActive(true);
        canPlayVid = true;
        Map.SetActive(false);
        Player.SetActive(false);
        Canvas.SetActive(false);
        
        

    }
}

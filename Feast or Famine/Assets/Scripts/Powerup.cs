
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Powerup : MonoBehaviour
{

    [SerializeField] private int powerupID = 0;
        //0 = Jump
        //1 = Dash
        //2 = Interact
    [SerializeField] private AudioClip PowerUpSoundClip = null;
    [SerializeField] private AudioClip MusicClip = null;
    [SerializeField] private AudioMixer music = null;
    [SerializeField] private AudioSource SoundEffects;
    [SerializeField] private AudioSource powerUpTrack;
    [SerializeField] private GameObject powerupImage;
    [SerializeField] private GameObject powerupDialog;

    //Audio
    public float defiValue;

    //URP Post Processing
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Player P = collision.GetComponent<Player>();
            if (P != null) //if script is found
            {
                if (powerupID == 0) //if script is found
                {
                    P.maxJumps = 2; //enables ability
                    //music.SetFloat("defi", Mathf.Lerp(-80f, 0, Time.deltaTime));
                    
                } 
                else if (powerupID == 1)
                {
                    P.lyreAbility = true;
                    //music.SetFloat("lyre", Mathf.Lerp(-80f, 0, Time.deltaTime));
                }
                else if (powerupID == 2)
                {
                    P.aulosAbility = true;
                    //music.SetFloat("aulos", Mathf.Lerp(-80f, 0, Time.deltaTime));
                }
                SoundEffects.PlayOneShot(PowerUpSoundClip, 0.1f);
                SoundEffects.PlayOneShot(MusicClip, 0.1f);
                powerupImage.SetActive(true);
                powerupDialog.SetActive(true);
                P.inDialog = true;
            }

            StartCoroutine(FadeInTrack());
            //if(powerUpTrack.volume >= 0.5f)
            //Destroy(this.gameObject); //powerup
        }
 
    }

    private IEnumerator FadeInTrack()
    {
        float timeToFade = 0.25f;
        float timeElapsed = 0f;
        while(timeElapsed < timeToFade)
        {
            powerUpTrack.volume = Mathf.Lerp(0, 1f, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
    /*public float GetDefiLevel()
    {
		bool result =  music.GetFloat("defi", out defiValue);
		if(result)
			return defiValue;
		else
			return 0f;
	}*/
}

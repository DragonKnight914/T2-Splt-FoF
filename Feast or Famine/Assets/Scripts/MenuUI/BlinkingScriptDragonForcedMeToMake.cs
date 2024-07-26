using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingScriptDragonForcedMeToMake : MonoBehaviour
{

    public GameObject blinkR;
    public GameObject blinkL;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(blinkEnum());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void blinking()
    {
        StartCoroutine(blinkEnum());
    }


    public IEnumerator pokeEye()
    {
        blinkL.SetActive(true);
        blinkR.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blinkL.SetActive(false);
        blinkR.SetActive(false);
    } 


    public IEnumerator blinkEnum()
    {
        float blinkInterval = Random.Range(0.5f, 3.5f);
        yield return new WaitForSeconds(blinkInterval);
        blinkL.SetActive(true);
        blinkR.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        blinkL.SetActive(false);
        blinkR.SetActive(false);
        Debug.Log("Blinked");
        StartCoroutine(blinkEnum());
    } 


}

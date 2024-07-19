using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorAbility : MonoBehaviour
{
    [SerializeField] private PowerPlatforms[] PPlatforms;
    [SerializeField] private GameObject barrier;
    [SerializeField] private float actTime = 2.916f;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Player P = collision.GetComponent<Player>();
            if (P != null) //if script is found
            {
                if (Input.GetKey(P.interactKey) && P.aulosAbility)
                {
                    barrier.SetActive(false);
                    StopAllCoroutines();
                    for (int j = 0; j < PPlatforms.Length; j++)
                    {
                        PPlatforms[j].isActive = false;
                        Debug.Log("Deactivated");                  
                    }
                    StartCoroutine(platformRhythmActivation());                    
                    //gameObject.SetActive(false);
                }
            }

            
        }
 
    }

    public IEnumerator platformRhythmActivation()
    {
        
        for (int j = 0; j < PPlatforms.Length; j++)
        {
            //
            PPlatforms[j].isActive = true;
            Debug.Log("activated");
            yield return new WaitForSeconds(actTime);
            StartCoroutine(platformRhythmDeactivation());
        }
        
        
    }
    public IEnumerator platformRhythmDeactivation()
    {
  
        for (int j = 0; j < PPlatforms.Length; j++)
        {
            yield return new WaitForSeconds(actTime);
            //StartCoroutine(platformRhythmActivation());
            PPlatforms[j].isActive = false;
            Debug.Log("deactivated");
            
        }
        //StartCoroutine(platformRhythmActivation());
        
    }
}

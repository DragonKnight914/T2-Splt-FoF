using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Player P;
    public Transform currentRespawnPoint;
    public GameObject BlackTrans;


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
            Debug.Log("Respawn");
            BlackTrans.SetActive(true);
            StartCoroutine(Respawner());
        }
 
    }

    private IEnumerator Respawner()
    {
        yield return new WaitForSeconds(0.5f);

        P.transform.position = currentRespawnPoint.position;
        
        yield return new WaitForSeconds(1.0f);

        BlackTrans.SetActive(false);
    }
}

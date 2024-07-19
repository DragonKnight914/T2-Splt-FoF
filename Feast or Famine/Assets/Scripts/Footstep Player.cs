using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioSource footstepsSound;
    private enum CURRENT_TERRAIN {GRASS, GROUND};
    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private void DetermineTerrain()
    {
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(transform.position, Vector2.down, 5.0f);

        foreach (RaycastHit2D rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Gound"))
            {
                currentTerrain = CURRENT_TERRAIN.GROUND;
                Debug.Log("ground terrain");
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = CURRENT_TERRAIN.GRASS;
                Debug.Log("Grass terrain");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

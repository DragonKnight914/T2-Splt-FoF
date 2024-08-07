using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private TDEnemy enemy;
    private int enemyMaxHp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemies")
        {
            //Destroy(collision.gameObject);
            enemy = collision.gameObject.GetComponent<TDEnemy>();
            StartCoroutine(damageInterval());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemies")
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator damageInterval()
    {
        yield return new WaitForSeconds(2.5f);
        enemy.Damage((1 * PlayerPrefs.GetInt("RoundScaling")));
        Debug.Log((int)(1 * PlayerPrefs.GetInt("RoundScaling")));
        Debug.Log("attack");
        StartCoroutine(damageInterval());

    }
}

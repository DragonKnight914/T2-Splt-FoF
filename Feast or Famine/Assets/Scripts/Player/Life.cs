using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public int life = 100;
    [SerializeField] public int maxLife = 100;
    [SerializeField] float speed = .5f;
    
    //hypothetical value
    int gainLife = 10;

    [SerializeField] Image greenBar;
    [SerializeField] Image redBar;
    //int amount futurely

    private void Update()
    {
        greenBar.fillAmount = (float)life  / (float)maxLife;
    }
    public void LostLife(int amount)
    {
        if(life <= 0)
        {
            //StopAllCoroutines();
            Die();
        }
        else if (life > 0)
        {
            life -= amount;
            //StartCoroutine(DecreaseLife());
        }
    }

    public void RecoverLife()
    {
        if(life > maxLife)
        {
            return;
        }

        life += gainLife;
        StartCoroutine (IncreaseLife());
    }

    /*void DecreaseLife()
    {
        float targetFillAmount = ((float)life) / ((float)maxLife);

        while(greenBar.fillAmount > targetFillAmount)
        {
            greenBar.fillAmount -= speed;
            //yield return new WaitForSeconds(.2f);

            while(redBar.fillAmount > targetFillAmount)
            {
                redBar.fillAmount -= speed * 5;
            }

            //yield return null;
        }

        greenBar.fillAmount = targetFillAmount;
        redBar.fillAmount = targetFillAmount;
    }*/

    IEnumerator IncreaseLife()
    {
        float targetFillAmount = ((float)life) / ((float)maxLife);

        while (redBar.fillAmount > targetFillAmount)
        {
            redBar.fillAmount += speed;
            yield return new WaitForSeconds(.2f);

            while (greenBar.fillAmount > targetFillAmount)
            {
                greenBar.fillAmount += speed * 5;
            }

            yield return null;
        }

        greenBar.fillAmount = targetFillAmount;
        redBar.fillAmount = targetFillAmount;
    }

    public void Die() 
    {

        Debug.Log("ded");
        Destroy(gameObject);
    }
}

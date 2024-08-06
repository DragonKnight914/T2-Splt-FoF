using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool canMove = true;

    Rigidbody2D rb2d;
    Vector2 movement;
    
    //Puase
    public bool isPaused = false;
    public GameObject PauseMenu;

    #region Unity Methods
    // Use Awake to get components, easy to dont get issue futurely
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Pause Button
        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
                Time.timeScale = 0;
                isPaused = true;
                canMove = false;
                PauseMenu.SetActive(true);
        }
        if (canMove)
        {
            Move();
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }
    #endregion

    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        /*if (movement.x != 0)
        {
            movement.y = 0;
        }
        else if (movement.y != 0)
        {
            movement.x = 0;
        }*/
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
        canMove = true;
    }
}

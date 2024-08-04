using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCEnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    private bool faceRight = false;
    private bool isMoving = false;
    private int forward;
    private Rigidbody2D rb;

    private Animator anim;

    //Lose Resources Mechanic
    private Player P;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundMask;
    public Vector2 boxSize;
    public float castDistance;

    //sfx
    [SerializeField] private AudioClip[] clip = null;
    private AudioSource Sounds;
    [SerializeField] private int pointLoss; 

    //UI
    //public TextMeshProUGUI  scoreUI;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim  = GetComponent<Animator>();
        Sounds = GetComponent<AudioSource>();
        StartCoroutine(Movement());
        StartCoroutine(Jumping());
        P = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            anim.SetBool("isIdle", false);
            rb.velocity = new Vector2(forward * moveSpeed, rb.velocity.y);
        }
        else
            anim.SetBool("isIdle", true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Colliding");
        if (collision.tag == "Player")
        {
            Debug.Log("Colliding");
            if (P.canBeDamaged)
            {
                P.score -= pointLoss;
                P.Damage();
            
                //scoreUI.text = "Resources: " + P.score;
                PlayerPrefs.SetInt("Resources", P.score);
            }
            //int soundPlayed = Random.Range(0, 5);
            //Sounds.PlayOneShot(clip[soundPlayed], 0.5f);
            //Destroy(this.gameObject);
        }

 
    }

    //Controls random movement
    private IEnumerator Movement()
    {
        float randomTime = Random.Range(1.0f, 3.0f);
        int moveDirection = Random.Range(0, 2);
        yield return new WaitForSeconds(randomTime);
        //Debug.Log(moveDirection);
        //Debug.Log(direction);
        //Randoms the direction the AI moves
        isMoving = true;
        if (moveDirection == 0)
        {
            forward = -1;
            if (faceRight)
            {
                Direction();
            }
            
        }
        if (moveDirection == 1)
        {
            forward = 1;
            if (!faceRight)
            {
                Direction();
            }
        }
        //Debug.Log("Moving");
        float walkTime = Random.Range(1.0f, 3.0f);
        yield return new WaitForSeconds(walkTime);
        isMoving = false;
        StartCoroutine(Movement());
    }

    //Controls random Jumping
    private IEnumerator Jumping()
    {
        float randomTime = Random.Range(1.0f, 5.0f);
        yield return new WaitForSeconds(randomTime);
        //Randoms the Jump timing the AI moves
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        
        StartCoroutine(Jumping());
    }

    private void Direction()
    {
        faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
    }
}

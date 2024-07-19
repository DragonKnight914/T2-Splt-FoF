using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Default Parameters")]
    
    private Player p;
    private Animator anim;
    public float speed = 5.0f;
    public float airSpeed = 1.0f;
    public float maxFallSpeed;
    private float fireRate = 0.25f; // slowdown
    private float canFire = 0.03f; //elapsed time
    private float canJump = -4f;
    public Rigidbody2D rb;
    public float jumpAmount = 9.0f;
    public float fallMultiplier = 4.5f;
    private BoxCollider2D boxCollider2d;
    public float frictionAmount = 3.0f;
    public Vector2 boxSize;
    public float castDistance;
    private float horizontalInput;
    [Header("Inputs")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.RightShift;
    public KeyCode interactKey = KeyCode.F;


    [Header("Power Ups")]
    public bool canDoubleJump;
    public int jumpCount;
    public int maxJumps;
    public bool lyreAbility;
    public float dashVelocity = 14f;
    public float dashTime = 0.5f;
    public float dashCooldown = 0.5f;
    private Vector2 dashDirection;
    public bool isDashing = false;
    private bool canDash = true;
    private int dashCount = 1;
    public bool aulosAbility;
    public bool inDialog;

    //ShurikenFire Direction
    //public Transform shurikenPoint;

    [Header("Player Direction")]
    public bool faceRight = true;


    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundMask;

    //Lives
    [SerializeField] public int lives = 3;


    //Powerups
    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public Animator lyreAnim;
    public Animator defiAnim;

    //Objects
    [SerializeField] private GameObject ShurikenPrefab = null;
    [SerializeField] private GameObject TripleShotPrefab = null;

    //private UIManager UI = null; //can hold link to Canvas
    //private SpawnManager SM = null;
    //private GameManager GM = null;

    //Sounds
    [SerializeField] private AudioClip DefiClip = null;
    [SerializeField] private AudioClip AulosClip = null;
    [SerializeField] private AudioClip LyreClip = null;
    [SerializeField] private AudioClip DeathClip = null;
    [SerializeField] private AudioClip DashClip = null;
    [SerializeField] private AudioClip JumpClip = null;
    [SerializeField] private AudioSource PlayerSounds;

    //score
    public float score;

    [Header("VCamReferences")]
    [SerializeField] private GameObject CameraFollowGO;
    private CameraFollowObjects CameraFollowObject;
    private float fallSpeedYDampChangeThresh;

    //Puase
    public bool isPaused = false;
    public GameObject PauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();

        CameraFollowObject = CameraFollowGO.gameObject.GetComponent<CameraFollowObjects>();

        fallSpeedYDampChangeThresh = CameraManager.instance.fallSpeedYDampChangeThresh;
        //transform.position = new Vector3(-6,-4,0);
        /*Anim = GetComponent<Animator>();
        
        
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        //link to UIManager code
        lives = 3;
        UI.UpdateLives(lives); //Starts wgame with 3 lives

        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (SM != null)
        {
            SM.StartSpawnRoutines();
        }

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();*/
        //PlayerSounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //Pause Button
        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
                Time.timeScale = 0;
                isPaused = true;
                PauseMenu.SetActive(true);
        }

        if (!inDialog  && isPaused == false)
        {

            horizontalInput = Input.GetAxisRaw("Horizontal");
            //Move();

            //Bounds();

            //Shoot();

            //Resets Dash
            if (isGrounded())
                canDash = true;
            if (aulosAbility && Input.GetKeyDown("f"))
            {
                StartCoroutine(PlayAulos());
            }

            //Resets Jump 
            if (isGrounded() && rb.velocity.y <= 0f)
            {
                jumpCount = 0;
                canDoubleJump = false;
                
            }

            if (!isGrounded() && canDoubleJump == false)
            {
                jumpCount++;
                canDoubleJump = true;
                
            }


            //Checks if can jump
            if (Input.GetKeyDown(jumpKey))
            {
                if (isGrounded() && jumpCount == 0)
                {
                    canDoubleJump = true;
                    Jump();
                    jumpCount++;

                }

                else if (!isGrounded() && jumpCount < maxJumps)
                {
                    Jump();
                    jumpCount++;
                }
            }
            if (!Input.GetKey(jumpKey) && !isGrounded())
            {
                //rb.AddForce(Vector2.up * jumpAmount * fallAmount, ForceMode2D.Force);
                JumpFall();
            }

            if (Input.GetKeyDown(dashKey) && canDash && lyreAbility && dashCount >= 1)
            {
                //rb.AddForce(Vector2.up * jumpAmount * fallAmount, ForceMode2D.Force);
                isDashing = true;
                canDash = false;
                dashCount--;
                if (faceRight)
                    dashDirection = Vector3.right;
                else
                    dashDirection = Vector3.left;
                StartCoroutine(Dashing());
            }

            //if we are falling past the speed threshold
            if (rb.velocity.y < fallSpeedYDampChangeThresh && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.lerpedFromPlayerFalling)
            {
                CameraManager.instance.LerpYDamping(true);
            }

            //if we're standing still or moving up
            if (rb.velocity.y == 0f && !CameraManager.instance.isLerpingYDamping && CameraManager.instance.lerpedFromPlayerFalling)
            {
                CameraManager.instance.lerpedFromPlayerFalling = false;

                CameraManager.instance.LerpYDamping(false);
            }
        }

        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

    }

    void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        if (!inDialog)
        {
            //clamp players Y fall speed 
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed * 5));

            Move();

            SpeedController();

            isGrounded();


            if (isDashing)
            {
                rb.velocity = dashDirection.normalized * dashVelocity;
                return;
            }
        }
        
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    void Move()
    {
        

        if (horizontalInput > 0 && !faceRight)
        {
            Direction();

            //turn the camera follow object
            CameraFollowObject.CallTurn();
            //rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
        }
        else if (horizontalInput < 0 && faceRight)
        {
            Direction();

            //turn the camera follow object
            CameraFollowObject.CallTurn();
            //rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
        }
        
        //changes movement direction based on direction faced
        //if (isGrounded())
        //{
            //rb.AddForce(Vector3.right * speed * horizontalInput, ForceMode2D.Force);
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

            //boxCollider2d.sharedMaterial.friction = frictionAmount;
        //}
            
            //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        /*else if (!isGrounded())
        {
            //rb.velocity = new Vector2(horizontalInput * airSpeed, rb.velocity.y);
            rb.AddForce(Vector3.right * airSpeed * horizontalInput, ForceMode2D.Force);
            //boxCollider2d.sharedMaterial.friction = 0.0f;
        }  */  
            
            //rb.AddForce(Vector3.left * speed * horizontalInput, ForceMode2D.Force);
            //transform.Translate(Vector3.left * speed * horizontalInput * Time.deltaTime);
                
    }

    private void Jump()
    {
        //if (isGrounded())
        //{
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);

        
        //}
        /*else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            jumpCount++;
        }*/
        PlayerSounds.PlayOneShot(JumpClip, 0.25f);
        if (jumpCount == 1)
        {
            defiAnim.SetTrigger("DefiTrigger");
            PlayerSounds.PlayOneShot(DefiClip, 0.05f);
        }
            
    }

    private void JumpFall()
    {
        rb.AddForce(Vector2.up *  -jumpAmount/fallMultiplier, ForceMode2D.Force);
    }

    private IEnumerator Dashing()
    {
        PlayerSounds.PlayOneShot(DashClip, 0.3f);
        PlayerSounds.PlayOneShot(LyreClip, 0.15f);
        lyreAnim.SetTrigger("Cooldown");
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        
        StartCoroutine(StopDashing());
        
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashCount++;
        
    }

    private void SpeedController()
    {
        // limiting speed on slope
        /*if (OnSlope() && !leaveSlope)
        {
            if (rb.velocity.magnitude > speed)
                rb.velocity = rb.velocity.normalized * speed;
        }*/

        //limits speed on the ground or air
        Vector2 maxSpeed = new Vector2(rb.velocity.x, 0);

        // limits velocity if necessary
        if (maxSpeed.magnitude > speed)
        {
            Vector2 limitedSpeed = maxSpeed.normalized * speed;
            rb.velocity = new Vector2(limitedSpeed.x, rb.velocity.y);
        }
        

        // reset y velocity
        //if (rb.velocity.y < 0f && isGrounded)
           //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    public bool isGrounded()
    {
          
        if (Physics2D.BoxCast(transform.position - transform.up * castDistance, boxSize, 0f, Vector2.down, 0f, groundMask))
        {
            //Debug.Log("Grounded");
            return true;
        }
        else
        {
            //Debug.Log("Not Grounded");
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }


    

    private void Direction()
    {
        faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public IEnumerator PlayAulos()
    {
        yield return new WaitForSeconds(0.4f);            
        PlayerSounds.PlayOneShot(AulosClip, 0.3f);
    }

    public void ExitDialog()
    {
        inDialog = false;
    }

    /*public void TripleShotPowerUp()
    {
        canTripleShot = true;
        
        StartCoroutine(TripleShotPowerDown());
           //run method separate from main program
    }

    public IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        //wait while program runs for 5 seconds            
        canSpeedBoost = false; //turn off triple shot
        speed = 5.0f;
    }

    public void SpeedBoostPowerUp()
    {
        canSpeedBoost = true;
        speed = 10.0f;
        
        StartCoroutine(SpeedBoostPowerDown());
           //run method separate from main program
    }*/

    /*public void LifeUp()
    {
        if (lives < 3)
        {
            lives++;
            UI.UpdateLives(lives);
        }    
    }

    public void Damage()
    {
        //Anim.SetTrigger("Hurt");
        lives--; //takes 1 life away
        //UI.UpdateLives(lives); //actual parameter
        Debug.Log("" + lives);

        if (lives < 1)
        {
            //End Game
            GM.gameOver = true;

            UI.ShowTitleScreen();

            //animation
            //Instantiate(explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(DeathClip, Camera.main.transform.position);

            //remove
            Destroy(this.gameObject);

            
        }

        
    }*/
}
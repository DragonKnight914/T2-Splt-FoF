using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSR
{

    public class PlayerAnimation : MonoBehaviour
    {

        private Animator Anim = null;
        private string currentState;
        private Player P = null;

        private float xAxis;
        private float yAxis;
        private Rigidbody2D rb2d;
        private bool isJumpPressed;
        private int groundMask;
        private bool isAulosPressed;
        private bool isAulosPlaying;
        private bool canLand;

        //Animation States
        const string PLAYER_IDLE = "IdleAnim";
        const string PLAYER_WALK = "WalkingAnim";
        const string PLAYER_DASH = "DashAnim";
        const string PLAYER_JUMP = "JumpAnim";
        const string PLAYER_AULOS = "AulosAnim";
        const string PLAYER_LAND = "LandingAnim";
        const string PLAYER_FALL = "FallingAnim";

        //[SerializeField] private FSR_Player fSR_Player;

        //Animation Durration
        private float aulosTime;
        private float landingTime;

        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
            P = GetComponent<Player>();
            //groundMask = 1 << LayerMask.NameToLayer("Ground");
            //conncet to player's animator component
        }

        void ChangeAnimationState(string newState)
        {
            //stop the same animation from interrupting itself
            if (currentState == newState)
            {
                return;
            }

            //play the animation
            Anim.Play(newState);

            //reassign the current state
            currentState = newState;

        }
        // Update is called once per frame
        void Update()
        {
            if (!P.inDialog)
            {
                //Checking directional inputs
                xAxis = Input.GetAxisRaw("Horizontal");

                //Checking Jump input
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isJumpPressed = true;
                }

                //Checking Aulos
                if (P.aulosAbility && Input.GetKeyDown("f"))
                {
                        isAulosPressed = true;
                }
            }
            else
                xAxis = 0;
        }

        private void FixedUpdate()
        {
            if (P.inDialog)
                ChangeAnimationState(PLAYER_IDLE);
            else
            {
                if (P.isGrounded() && !P.isDashing && !isAulosPlaying && rb2d.velocity.y == 0f && !canLand)
                {
                    
                    if (xAxis != 0)
                    {
                        ChangeAnimationState(PLAYER_WALK);
                        //fSR_Player.step();
                    }
                    else
                    {
                        ChangeAnimationState(PLAYER_IDLE);
                    }
                }

                if (P.isDashing)
                {
                    ChangeAnimationState(PLAYER_DASH);
                }

                //-------------------------------------
                //Check player trying to jump
                if (isJumpPressed && !P.isDashing && !isAulosPlaying && rb2d.velocity.y >= 0f) 
                {
                    isJumpPressed = false;
                    ChangeAnimationState(PLAYER_JUMP);
                }

                /*if (!P.isGrounded() && !P.isDashing)
                {
                    ChangeAnimationState(PLAYER_JUMP);
                }*/
                //Check player trying to attack
                if ((isAulosPressed && P.isGrounded()) && !P.isDashing)
                {
                    isAulosPressed = false;
                    if (!isAulosPlaying)
                    {

                        isAulosPlaying = true;
                        ChangeAnimationState(PLAYER_AULOS);
                        aulosTime = Anim.GetCurrentAnimatorStateInfo(0).length;
                        Invoke("AulosComplete", aulosTime); 

                    }
                }

                if (rb2d.velocity.y < 0f && !P.isDashing)
                {
                    ChangeAnimationState(PLAYER_FALL);
                    canLand = true;   
                }

                if (canLand && rb2d.velocity.y == 0f && !P.isDashing)
                {
                    bool isLanding = false;
                    if (!isLanding)
                    {
                        isLanding = true;
                        ChangeAnimationState(PLAYER_LAND);
                        landingTime = Anim.GetCurrentAnimatorStateInfo(0).length;
                        Invoke("LandingComplete", landingTime); 
                    }
                }
            }
        }

        void AulosComplete()
        {
            isAulosPlaying = false;
        }

        void LandingComplete()
        {
            canLand = false; 
        }
    }
}
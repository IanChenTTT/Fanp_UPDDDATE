using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV3 : MonoBehaviour
{
    [SerializeField]
    float fJumpVelocity = 5;
    private float HorizontalInput;
    private float VerticalInput;
    Rigidbody2D playerRB;

    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    float fGroundedRemember = 0;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;

    [SerializeField]
    float fHorizontalAcceleration = 1;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float fCutJumpHeight = 0.5f;
   private float fHorizontalVelocity;

   [SerializeField] private float MaxSpeed;
   // Assumption frition material woudn't larger then 1;
   [SerializeField]
   [Range(0,1)]
    private float uFrictionMultiply;
   [SerializeField] private float FlipForce;

    // Player Grapple Controll
    [SerializeField] private GameObject GrappleGun;
    // New PlayerAnimation

    [SerializeField] private NewPlayerAnim playerAnim;

    [SerializeField] private SpringJoint2D hook_joint2d;

   // CHECK FUNC MEMBER VARIABLE
   [SerializeField] private bool isPlayerFlipForce = true;
   [SerializeField] private float castDist = 0;
   [SerializeField] private Vector2 castVecSize;
   [SerializeField] private float castRadSize;
   [SerializeField] private LayerMask groundLayer;
   private bool isFaceRight = true;
    void Start ()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<NewPlayerAnim>();
	}
	
	void Update ()
    {

        //Handle PLayer Flipping
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        if ( HorizontalInput < 0 && isFaceRight)Flip();
        else if ( HorizontalInput > 0 && !isFaceRight)Flip();

        //Hook Animation
        if(hook_joint2d != null)
        {
            if(hook_joint2d.enabled == true)
            {
                playerAnim.ChangeAnimeState(PlayerAnimeState.SWING);
            }
            else{
                //iF player was hook before
                if(playerAnim.IsHook()) playerAnim.ChangeAnimeState(PlayerAnimeState.JUMP);
            }
        }
        fGroundedRemember -= Time.deltaTime;
        if (IsGround())
        {
            //If statement handle frcition f = u * force;
            if(isFaceRight)
                playerRB.AddForce(Vector2.left * uFrictionMultiply / 5, ForceMode2D.Impulse);
            else
                playerRB.AddForce(Vector2.right * uFrictionMultiply / 5, ForceMode2D.Impulse);
            
            //This line adjust multitap timer
            fGroundedRemember = fGroundedRememberTime;

            //Ground Animation
            if(playerRB.velocity.x != 0){
                playerAnim.ChangeAnimeState(PlayerAnimeState.RUN);
            }
            if(VerticalInput == 0 && HorizontalInput == 0){
                playerAnim.ChangeAnimeState(PlayerAnimeState.IDLE);
            }

            if(GrappleGun != null){
                GrappleGun.SetActive(false);
            }
        }
        // JUMP ANIMATE DETECT
        if((VerticalInput != 0 || playerRB.velocity.y > 0 || playerRB.velocity.y < 0) && !IsGround() && !playerAnim.IsHook())
        {
            playerAnim.ChangeAnimeState(PlayerAnimeState.JUMP);

            if(GrappleGun != null){
                GrappleGun.SetActive(true);
            }
        }

        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (playerRB.velocity.y > 0)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * fCutJumpHeight);
            }
        }

        if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            playerRB.velocity = new Vector2(playerRB.velocity.x, fJumpVelocity);
        }

        fHorizontalVelocity = playerRB.velocity.x;
        if(fHorizontalVelocity <= MaxSpeed && fHorizontalVelocity >= -MaxSpeed)
        {
            fHorizontalVelocity += HorizontalInput * fHorizontalAcceleration ;

            if (Mathf.Abs(HorizontalInput) < 0.01f)
                fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime*10);
            else if (Mathf.Sign(HorizontalInput) != Mathf.Sign(fHorizontalVelocity))
                fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime*10 );
            else
                fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime *10 );
        }
    }
   void FixedUpdate(){

      playerRB.velocity = new Vector2(fHorizontalVelocity , playerRB.velocity.y);
   }
// Check Function
   private void Flip()
   {
      //this line handle quick air turn aroun
      if(isPlayerFlipForce)
      {
        FlipForce *= -1;
        playerRB.AddForce(Vector2.right * FlipForce,ForceMode2D.Impulse); 
      }
      isFaceRight = !isFaceRight;
      transform.Rotate(0,180,0);
   }
  private bool IsGround()
   {
      /**
       * CAST ON TARGET ITSELF
       * Use bound size This was wierd
       * Update: using transform size 
       * MAKE SURE SPRITE WAS TRIM,
       * USING TRANSFORM TO DO
       * 0.1 mean transform direction away from origin
       * box vector2 * 0.1
      */
      //RaycastHit2D boxRayDown = Physics2D.BoxCast(playerCol.bounds.center, playerCol.bounds.size, 0f, -Vector2.up, 0.1f, groundLayer);
      //RaycastHit2D boxRayDown = Physics2D.BoxCast(transform.position, castVecSize, 0f, -transform.up, castDist, groundLayer);
      RaycastHit2D capsuleDown = Physics2D.CircleCast(transform.position, castRadSize, -transform.up, castDist, groundLayer);
      if (capsuleDown.collider != null)
         return true;
      return false;
   }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      // Gizmos.DrawWireCube(transform.position - transform.up * castDist, castVecSize);
      Gizmos.DrawWireSphere(transform.position - transform.up * castDist, castRadSize);
   }
}


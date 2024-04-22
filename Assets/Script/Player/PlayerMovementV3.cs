using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV3 : MonoBehaviour
{
    [SerializeField]
    LayerMask lmWalls;
    [SerializeField]
    float fJumpVelocity = 5;

    Rigidbody2D rigid;

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

   // CHECK FUNC MEMBER VARIABLE
   [SerializeField] private float castDist = 0;
   [SerializeField] private Vector2 castVecSize;
   [SerializeField] private float castRadSize;
   [SerializeField] private LayerMask groundLayer;
   private bool isFaceRight = true;

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        fGroundedRemember -= Time.deltaTime;
        if (IsGround())
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * fCutJumpHeight);
            }
        }

        if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, fJumpVelocity);
        }

        float fHorizontalVelocity = rigid.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);

        rigid.velocity = new Vector2(fHorizontalVelocity, rigid.velocity.y);

      //Handle PLayer Flipping
      if (rigid.velocity.x < 0 && isFaceRight)Flip();
      else if (rigid.velocity.x > 0 && !isFaceRight)Flip();
    }
   // Check Function
   private void Flip()
   {
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

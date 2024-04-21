using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   private Rigidbody2D playerRB;
   private Collider2D playerCol;

   //Input Action
   private PlayerControll playerControll;
   [SerializeField] private float jumpForce;
   [SerializeField] private float addForce = 0;
   [SerializeField] private float playerMovSpeed = 0;
   [SerializeField] private float PlayerMaxVel = 7.5f;
   [SerializeField] private Vector2 playerInputVec;
   [SerializeField] private Vector2 playerInputVecSmooth;
   [SerializeField] private Vector2 smoothVel;
   [SerializeField] private float smoothTime = .2f;
   [SerializeField] private float castDist = 0;
   [SerializeField] private Vector2 castVecSize ;
   [SerializeField] private float castRadSize ;
   [SerializeField] private LayerMask groundLayer;
   private bool isGround;
   private const float  maxAddForce = 2;

   private bool isFaceRight;
   void Awake(){
      playerRB = GetComponent<Rigidbody2D>();
      playerCol = GetComponent<Collider2D>();

      // Input Action setup
      playerControll = new PlayerControll();
      playerControll.PlayerActionMap.Enable();
      playerControll.PlayerActionMap.Jump.performed += OnPressJump;
   }
   private void Update(){
      playerInputVec = playerControll.PlayerActionMap.MoveMent.ReadValue<Vector2>();
      playerInputVecSmooth = Vector2.SmoothDamp(playerInputVecSmooth, playerInputVec, ref smoothVel, smoothTime);
   }
  private void FixedUpdate(){
      if(playerRB.velocity.x <= PlayerMaxVel && playerRB.velocity.x >= -PlayerMaxVel)
         playerRB.velocity += playerMovSpeed * Time.deltaTime * playerInputVecSmooth;
      if(Math.Abs(playerInputVec.x) < 0 && isFaceRight){
         Flip();
      }
      if(Math.Abs(playerInputVec.x) > 0 && !isFaceRight){
         Flip();
      }
      // Debug.Log(Time.deltaTime);
      // Debug.Log(new Vector2(playerInputVec.x,playerInputVec.y));
      // Debug.Log(playerMovSpeed);
      // playerRB.AddForce(new Vector2(playerInputVec.x,playerInputVec.y) * playerMovSpeed * Time.deltaTime);
      //playerRB.MovePosition((Vector2)this.transform.position+ (playerInputVec * playerMovSpeed * Time.deltaTime));
      
   }
   //Custom action map function
   public void OnPressJump(InputAction.CallbackContext callbackContext){
      Debug.Log(callbackContext.ReadValue<float>());
      if(IsGround(true))
      {
         Debug.Log("ADd force: "+jumpForce + addForce);
         playerRB.AddForce(jumpForce  *  Vector2.up ,ForceMode2D.Impulse);
         addForce = 0f;
      }
   }
   // Check Function
   private void Flip(){
       if (transform.localEulerAngles.y != 180 && !isFaceRight)
        transform.Rotate(0f, 180f, 0f);
    else if(transform.localEulerAngles.y != 0 && isFaceRight)
            transform.Rotate(0f, -180f, 0f);
            isFaceRight = !isFaceRight;
   }
   public void OnHoldJump(InputAction.CallbackContext callbackContext){
      Debug.Log("Jump");
      if(callbackContext.performed && isGround)
      {
         if(addForce <= maxAddForce)addForce += 0.2f;
      }
   }
 
    private bool IsGround(bool isDev)
    {
        /**
         * CAST ON TARGET ITSELF
         * Use bound size This was wierd
         * Update using transform size 
         * MAKE SURE SPRITE WAS TRIM,
         * USING TRANSFORM TO DO
         * 0.1 mean transform direction away from origin
         * box vector2 * 0.1
        */
        //RaycastHit2D boxRayDown = Physics2D.BoxCast(playerCol.bounds.center, playerCol.bounds.size, 0f, -Vector2.up, 0.1f, groundLayer);
        //RaycastHit2D boxRayDown = Physics2D.BoxCast(transform.position, castVecSize, 0f, -transform.up, castDist, groundLayer);
        RaycastHit2D capsuleDown = Physics2D.CircleCast(transform.position, castRadSize, -transform.up, castDist, groundLayer);
        if (isDev) DebugDrawDownBox();
        if (capsuleDown.collider != null)
        {
            isGround = true;
            return true;
        }
         isGround = false;
        return false;

    }
    private void OnDrawGizmos(){
      Gizmos.color = Color.red;
      // Gizmos.DrawWireCube(transform.position - transform.up * castDist, castVecSize);
      Gizmos.DrawWireSphere(transform.position - transform.up * castDist, castRadSize);
    }
    public void DebugDrawDownBox()
    {

        Debug.DrawRay(playerCol.bounds.center + new Vector3(playerCol.bounds.extents.x, 0), Vector2.down * (playerCol.bounds.extents.y + 0.1f), Color.yellow);

        Debug.DrawRay(playerCol.bounds.center - new Vector3(playerCol.bounds.extents.x, 0), Vector2.down * (playerCol.bounds.extents.y + 0.1f), Color.yellow);

        Debug.DrawRay(playerCol.bounds.center - new Vector3(playerCol.bounds.extents.x, playerCol.bounds.extents.y + 0.1f), Vector2.right * (playerCol.bounds.extents.x * 2f), Color.yellow);
    }
}

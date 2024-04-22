using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
   [SerializeField] private float playerMovSpeed = 0;
   [SerializeField] private Vector2 playerInputVec;
   [SerializeField] private Vector2 playerInputVecSmooth;
   [SerializeField] private float playerFallMultiplier = 2.5f;
   [SerializeField] private float playerLowMultiplier = 2f;
   [SerializeField] private Vector2 smoothVel;
   [SerializeField] private float smoothTime = .2f;
   [SerializeField] private float Fallspeed = 0f;
   [SerializeField] private float maxFallspeed = 0f;
   private Vector2 cacheFall;
   private float cacheLow;
   private float GravityScale;
   
   // CHECK FUNC MEMBER VARIABLE
   [SerializeField] private float castDist = 0;
   [SerializeField] private Vector2 castVecSize;
   [SerializeField] private float castRadSize;
   [SerializeField] private LayerMask groundLayer;
   private bool isFaceRight = true;
   void Awake()
   {
      playerRB = GetComponent<Rigidbody2D>();
      playerCol = GetComponent<Collider2D>();

      // Input Action setup
      playerControll = new PlayerControll();
      playerControll.PlayerActionMap.Enable();
      playerControll.PlayerActionMap.Jump.performed += OnPressJump;
   }
   void Start(){
      playerFallMultiplier = 2.4f;
      cacheFall = (playerFallMultiplier -1) * Physics2D.gravity.y * Vector2.up;
      Debug.Log(cacheFall);      
   }
   private void Update()
   {
      
      // New Input Action wont get lerp value, instead it gave clamp val
      // use slerp or damp to creat 0 ~ 1 gradually increase
      playerInputVec = playerControll.PlayerActionMap.MoveMent.ReadValue<Vector2>();
      playerInputVecSmooth = Vector2.SmoothDamp(playerInputVecSmooth, playerInputVec, ref smoothVel, smoothTime);
   }
   private void FixedUpdate()
   {
      // playerRB.AddForce(Vector2.down * Physics2D.gravity,ForceMode2D.Force);
      playerRB.velocity = new Vector2(playerInputVecSmooth.x * playerMovSpeed, playerRB.velocity.y);
      Debug.Log(GravityScale);
      Debug.Log(playerRB.gravityScale);
      //Handle Player Floating 
      //Credit from https://www.youtube.com/watch?v=7KiK0Aqtmzc
      if(playerRB.velocity.y > 0){
         playerRB.velocity += cacheFall * Time.deltaTime;
         GravityScale = 1;
         playerRB.gravityScale = GravityScale;
      }
      if(playerRB.velocity.y < 0){
         //playerRB.AddForce(Vector2.down * Fallspeed, ForceMode2D.Force);
         GravityScale = playerRB.gravityScale;   
         playerRB.gravityScale = Fallspeed * GravityScale;
         // playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Max(playerRB.velocity.y, -maxFallspeed));
      }           
      //Handle PLayer Flipping
      if (playerRB.velocity.x < 0 && isFaceRight)Flip();
      else if (playerRB.velocity.x > 0 && !isFaceRight)Flip();
   }
   //Custom action map function
   public void OnPressJump(InputAction.CallbackContext context)
   {
      if (context.performed && IsGround())
      {
         playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
         // playerRB.AddForce(Vector2.up * jumpForce);
      }

      if (context.canceled && playerRB.velocity.y > 0f)
      {
         playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.9f);
      }
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

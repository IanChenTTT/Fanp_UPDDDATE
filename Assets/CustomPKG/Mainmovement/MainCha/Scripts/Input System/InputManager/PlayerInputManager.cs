using UnityEngine;
using UnityEngine.InputSystem;
using static UltimateCC.PlayerMain;

namespace UltimateCC
{
    public class PlayerInputManager : MonoBehaviour
    {
        // Declaration of necessary references
        private PlayerMain player;
        private PlayerData playerData;
        public InputActions playerControls;

        // Variables to store input
        [SerializeField, NonEditable] private float input_Walk;
        [SerializeField, NonEditable] private bool input_Jump;
        //[SerializeField, NonEditable] private bool input_Dash;
        [SerializeField, NonEditable] private bool input_Crouch;
        [SerializeField, NonEditable] private bool input_WallGrab;
        [SerializeField, NonEditable] private float input_WallClimb;

        // Properties to access the input variables
        public float Input_Walk => input_Walk;
        public bool Input_Jump => input_Jump;
        //public bool Input_Dash => input_Dash;
        public bool Input_Crouch => input_Crouch;
        public bool Input_WallGrab => input_WallGrab;
        public float Input_WallClimb => input_WallClimb;

        // Constructor to set player and playerData references
        public PlayerInputManager(PlayerMain player, PlayerData playerData)
        {
            this.player = player;
            this.playerData = playerData;
        }


        private void Awake()
        {
            playerControls = new InputActions(); // Initialize InputActions object to use input map of new input system
            player = GetComponent<PlayerMain>(); // Reference for Ultimate2DPlayer component where all of content come up together
            playerData = player.PlayerData; // Reference for Ultimate2DPlayer.PlayerData component where all variables stored
        }

        private void OnEnable()
        {
            // This code block assigns events to actions in order to receive input from the player.
            playerControls.Player.Enable();
            playerControls.Player.Jump.started += OnJumpStarted;
            playerControls.Player.Jump.canceled += OnJumpCanceled;
            playerControls.Player.Walk.performed += OnWalk;
            playerControls.Player.Walk.canceled += OnWalk;
            //playerControls.Player.Dash.performed += OnDash;
            //playerControls.Player.Dash.canceled += OnDash;
            playerControls.Player.Crouch.performed += OnCrouch;
            playerControls.Player.Crouch.canceled += OnCrouch;
            playerControls.Player.WallGrab.performed += OnWallGrab;
            playerControls.Player.WallGrab.canceled += OnWallGrab;
            playerControls.Player.WallClimb.performed += OnWallClimb;
            playerControls.Player.WallClimb.canceled += OnWallClimb;
        }

        private void FixedUpdate()
        {
            UpdateTimers();
            
        }

        private void UpdateTimers()
        {
            //coyote time and jump buffer timers implementation
            if (playerData.Physics.IsGrounded && player.CurrentState != AnimName.Jump && (!playerData.Physics.IsOnNotWalkableSlope || playerData.Physics.IsMultipleContactWithNonWalkableSlope))
            {
                playerData.Jump.CoyoteTimeTimer = playerData.Jump.CoyoteTimeMaxTime;
            }
            else if (player.CurrentState == AnimName.Jump)
            {
                playerData.Jump.CoyoteTimeTimer = 0f;
            }

            if (playerData.Physics.IsNextToWall && player.CurrentState != AnimName.WallJump)
            {
                playerData.Walls.WallJump.CoyoteTimeTimer = playerData.Walls.WallJump.CoyoteTimeMaxTime;
            }
            else if (player.CurrentState == AnimName.WallJump)
            {
                playerData.Walls.WallJump.CoyoteTimeTimer = 0f;
            }

            playerData.Jump.JumpBufferTimer = playerData.Jump.JumpBufferTimer > 0f ? playerData.Jump.JumpBufferTimer - Time.deltaTime : 0f;
            playerData.Jump.CoyoteTimeTimer = playerData.Jump.CoyoteTimeTimer > 0f ? playerData.Jump.CoyoteTimeTimer - Time.deltaTime : 0f;
            //playerData.Dash.DashCooldownTimer = playerData.Dash.DashCooldownTimer > 0f ? playerData.Dash.DashCooldownTimer - Time.deltaTime : 0f;
            playerData.Walls.WallJump.JumpBufferTimer = playerData.Walls.WallJump.JumpBufferTimer > 0f ? playerData.Walls.WallJump.JumpBufferTimer - Time.deltaTime : 0f;
            playerData.Walls.WallJump.CoyoteTimeTimer = playerData.Walls.WallJump.CoyoteTimeTimer > 0f ? playerData.Walls.WallJump.CoyoteTimeTimer - Time.deltaTime : 0f;
        }

        // all input based events
        #region Input Event Functions
        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            input_Jump = context.ReadValueAsButton();
            playerData.Jump.JumpBufferTimer = playerData.Jump.JumpBufferMaxTime;
            playerData.Walls.WallJump.JumpBufferTimer = playerData.Walls.WallJump.JumpBufferMaxTime;
            playerData.Jump.NewJump = player.CurrentState != AnimName.WallGrab
                                    && player.CurrentState != AnimName.WallSlide
                                    && player.CurrentState != AnimName.WallClimb
                                    && playerData.Walls.WallJump.CoyoteTimeTimer == 0;
        }

        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            input_Jump = context.ReadValueAsButton();
            if (player.CurrentState == AnimName.Jump)
            {
                playerData.Physics.CutJump = true;
            }
        }

        private void OnWalk(InputAction.CallbackContext context)
        {
            input_Walk = context.ReadValue<float>();
        }

        /*private void OnDash(InputAction.CallbackContext context)
        {
            input_Dash = context.ReadValueAsButton();
        }*/

        private void OnCrouch(InputAction.CallbackContext context)
        {
            input_Crouch = context.ReadValueAsButton();
        }
        private void OnWallGrab(InputAction.CallbackContext context)
        {
            input_WallGrab = context.ReadValueAsButton();
        }
        private void OnWallClimb(InputAction.CallbackContext context)
        {
            input_WallClimb = context.ReadValue<float>();
        }
        #endregion
    }
}

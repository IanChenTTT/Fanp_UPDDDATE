using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewPlayerAnim : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField]
    private NewPlayerAnimState playerState;
    private void Awake(){
        playerAnimator = this.GetComponent<Animator>();

        //Make Player stay at idle state evertime it load;
        playerState.currentState = PlayerAnimeState.IDLE;
    }
    private void Update(){
        switch(playerState.currentState){
            case  PlayerAnimeState.IDLE:
                playerAnimator.Play("idl");
            break;
            case  PlayerAnimeState.RUN:
                playerAnimator.Play("run_fanp");
            break;
            case  PlayerAnimeState.JUMP:
                playerAnimator.Play("jump_fanp");
            break;
            case  PlayerAnimeState.SWING:
                playerAnimator.Play("swing");
            break;
            default:
            break;
        }
        Debug.Log(playerState.currentState);
    }
    public void ChangeAnimeState(PlayerAnimeState state){
        // Prevent Load same Anim
        if(playerState.currentState == state) return;

        playerAnimator.Play("idl");

        playerState.currentState = state;
    }

}

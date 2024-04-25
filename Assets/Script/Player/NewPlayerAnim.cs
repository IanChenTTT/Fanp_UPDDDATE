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

    private void ChangeAnimeState(PlayerAnimeState state){
        // Prevent Load same Anim
        if(playerState.currentState == state) return;

        playerAnimator.Play("idl");

        playerState.currentState = state;
    }

}

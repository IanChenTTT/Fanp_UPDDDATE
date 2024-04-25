using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public enum PlayerAnimeState{
        IDLE,
        RUN,
        JUMP,
        SWING
};
[CreateAssetMenu(fileName = "PlayerAnimeState", menuName = "ScriptableObjects/PlayerAnimeState", order = 1)]
public class NewPlayerAnimState : ScriptableObject
{
   
    public PlayerAnimeState currentState;

}

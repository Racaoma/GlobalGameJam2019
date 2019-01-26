using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public PlayerStateController Player { get; set; }
    public void SetState(PlayerState state)
    {
        if(CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = state;
        if(CurrentState != null)
        {
            CurrentState.Player = Player;
            CurrentState.OnEnter();
        }
    }

    /*
    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }
    */
}

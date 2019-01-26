using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public PlayerStateController Player { get; set; }
    public abstract void OnEnter();

    public abstract void OnExit();

}

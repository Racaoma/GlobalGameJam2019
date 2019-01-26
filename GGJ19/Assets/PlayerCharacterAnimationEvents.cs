using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCharacterAnimationEvents : MonoBehaviour
{
    public Action OnExecuteAttack;
    public void ExecuteAttack()
    {
        if(OnExecuteAttack != null)
        {
            OnExecuteAttack();
        }
    }
}

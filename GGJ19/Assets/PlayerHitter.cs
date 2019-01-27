using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitter : MonoBehaviour
{
    Enemy _enemy;
    public void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_enemy != null)
        {
            if (_enemy.currentState != enemyState.Active)
            {
                return;
            }
        }

        var player = other.GetComponent<PlayerStateController>();
        if(player != null)
        {
            player.TakeHit(transform.position);
        }

        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerStateController>();
        if(player != null)
        {
            player.TakeHit(transform.position);
        }
    }
}

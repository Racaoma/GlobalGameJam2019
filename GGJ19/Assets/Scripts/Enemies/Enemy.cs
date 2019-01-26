using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    protected int maxHP;
    protected int currentHP;

    //Methods
    public void takeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
        if(currentHP <= 0)
        {
            killEnemy();
        }
    }

    public void killEnemy()
    {
        this.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum enemyType
{
    Pillow,
    Puff,
    Mattress
}

public class Enemy : MonoBehaviour
{
   public UnityEvent OnEnemyDie;
    //Variables
    protected int maxHP;
    protected int currentHP;

    void Awake() {
        OnEnemyDie.AddListener(LevelFlow.Instance.EnemyDeath);
    }
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
        OnEnemyDie.Invoke();
        this.enabled = false;
    }
}

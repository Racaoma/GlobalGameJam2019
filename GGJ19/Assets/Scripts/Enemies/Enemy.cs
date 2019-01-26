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

public enum enemyState
{
    Active,
    Stunned,
    KnockedDown
}

public class Enemy : MonoBehaviour
{
   public UnityEvent OnEnemyDie;
    //Variables
    protected int maxHP;
    protected int currentHP;
    protected enemyState currentState;
    protected Animator animatorRef;
    protected float stunTimer;
    public UnityEvent OnEnemyDie;

    //Particles
    public GameObject hitFX;
    public GameObject deathFX;
    private void Start()
    {
        OnEnemyDie.AddListener(LevelFlow.Instance.EnemyDeath);
    }

    public void takeDamage(int damageTaken)
    {
        currentHP -= damageTaken;

        if (currentHP <= 0)
        {
            killEnemy();
           
        }
        else
        {
            SpawnFX(hitFX);
        }
    }

    public void killEnemy()
    {      
        OnEnemyDie.Invoke();
        SpawnFX(deathFX);
        this.enabled = false;
    }

    
    private void SpawnFX(GameObject effect)
    {
        if (effect == null)
            return;

        var fx = Instantiate(effect, transform.position, transform.rotation);
        Destroy(fx, 1);
    }
}

﻿using System.Collections;
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
    //Variables
    protected int maxHP;
    protected int currentHP;
    protected enemyState currentState;
    protected Animator animatorRef;
    protected float stunTimer;

    //Layers
    protected int playerLayer;
    protected int groundLayer;
    protected LayerMask playerLayerMask;
    protected LayerMask groundLayerMask;

    //Events
    public UnityEvent OnEnemyDie;

    //Particles
    public GameObject hitFX;
    public GameObject deathFX;

    //Methods
    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
        playerLayerMask = LayerMask.GetMask("Player");
        groundLayerMask = LayerMask.GetMask("Ground");
        animatorRef = this.transform.GetComponentInChildren<Animator>();
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
            stunTimer = 1f;
            currentState = enemyState.Stunned;
            SpawnFX(hitFX);
        }
    }

    public void killEnemy()
    {      
        OnEnemyDie.Invoke();
        SpawnFX(deathFX);
        animatorRef.SetTrigger("knockDown");
        currentState = enemyState.KnockedDown;
        this.enabled = false;
    }
    
    private void SpawnFX(GameObject effect)
    {
        if (effect == null) return;
        var fx = Instantiate(effect, transform.position, transform.rotation);
        Destroy(fx, 1);
    }
}

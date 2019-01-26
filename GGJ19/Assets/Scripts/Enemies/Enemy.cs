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

public abstract class Enemy : MonoBehaviour
{
    //Variables
    protected int maxHP;
    protected int currentHP;
    protected enemyState currentState;
    protected float stunTimer;

    //Layers
    protected int playerLayer;
    protected int groundLayer;
    protected LayerMask playerLayerMask;
    protected LayerMask groundLayerMask;

    //Events
    public UnityEvent OnEnemyDie;

    //Particle Effects
    public GameObject feathersFX;
    public GameObject transformFX;

    //References
    protected Animator animatorRef;
    protected SpriteRenderer spriteRendererRef;
    [SerializeField]
    protected Sprite mundaneFormSprite;

    //Methods
    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
        playerLayerMask = LayerMask.GetMask("Player");
        groundLayerMask = LayerMask.GetMask("Ground");
        animatorRef = this.transform.GetComponentInChildren<Animator>();
        spriteRendererRef = this.transform.GetComponentInChildren<SpriteRenderer>();
        OnEnemyDie.AddListener(LevelFlow.Instance.EnemyDeath);
    } 

    public void takeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
        if(damageTaken == 2) SpawnFX(feathersFX);

        if (currentHP <= 0)
        {
            killEnemy();
        }
        else
        {
            stunTimer = 1f;
            currentState = enemyState.Stunned;
        }
    }

    public void killEnemy()
    {      
        OnEnemyDie.Invoke();
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

    //Abstract Methods
    public void becomeMundane()
    {
        SpawnFX(transformFX);
        animatorRef.enabled = false;
        spriteRendererRef.sortingOrder = -1;
        spriteRendererRef.sprite = mundaneFormSprite;
    }
}

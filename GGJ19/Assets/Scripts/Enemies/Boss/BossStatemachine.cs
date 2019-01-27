using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatemachine : MonoBehaviour
{
    public Collider2D headCollider;
    public Collider2D leftFistCollider;
    public Collider2D rightFistCollider;

    public Animator animator;

    public Enemy_Boss enemyBoss;

    public ParticleSystem DeathFX;
    


    public IdleBoss idle;
    public AttackingBoss attacking;
    public DefendingBoss defending;
    public HittingFloorBoss hittingFloor;
    public ExplodingBoss explode;
    public DefeatedBoss defeated;

    private BossState currentState;


    private void Start()
    {
        idle = new IdleBoss(this);
        attacking = new AttackingBoss(this);
        defending = new DefendingBoss(this);
        hittingFloor = new HittingFloorBoss(this);
        explode = new ExplodingBoss(this);
        defeated = new DefeatedBoss(this);


        SetState(idle);

        enemyBoss.OnEnemyDie.AddListener(KillBoss);
    }

    private void Update()
    {
        currentState.Tick();
    }

    private void FixedUpdate()
    {
        currentState.FixedTick();
    }

    
    public void IdleState(int delay = 0)
    {
        SetState(idle, delay);
    }

    public void RandomState(int delay = 0)
    {
        int n = Random.Range(0, 4);

        if (n == 0)
        {
            SetState(attacking, delay);
        }
        else if (n == 1)
        {
            SetState(defending, delay);
        }
        else if (n == 2)
        {
            SetState(hittingFloor, delay);
        }
        else if (n == 3)
        {
            SetState(explode, delay);
        }
    }

    public void KillBoss()
    {
        SetState(idle);

        enemyBoss.gameObject.SetActive(false);
        DeathFX.Play();
    }


    public void SetState(BossState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void SetState(BossState state, float delay)
    {
        StartCoroutine(WaitToChange(state, delay));
    }

    IEnumerator WaitToChange(BossState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetState(state);
    }
}

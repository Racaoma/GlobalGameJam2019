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

    public AudioClip sound_attacking;
    public AudioClip sound_defending;
    public AudioClip sound_hittingFloor;
    public AudioClip sound_explode;
    public AudioClip sound_defeated;

    
    public IdleBoss idle;
    public AttackingBoss attacking;
    public DefendingBoss defending;
    public HittingFloorBoss hittingFloor;
    public ExplodingBoss explode;
    public DefeatedBoss defeated;

    private BossState _currentState;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();

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
        _currentState.Tick();
    }

    private void FixedUpdate()
    {
        _currentState.FixedTick();
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
        StopAllCoroutines();

        SetState(defeated);

        enemyBoss.gameObject.SetActive(false);
        DeathFX.Play();
    }


    public void SetAudio(AudioClip audio, bool loop = false)
    {
        //return;

        _source.Play();
        _source.clip = audio;
        _source.loop = loop;
        _source.Play();
    }

    public void SetState(BossState state)
    {
        if (_currentState != null)
        {
            _currentState.OnStateExit();
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.OnStateEnter();
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

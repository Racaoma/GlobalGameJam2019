﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerAttackController : Singleton<PlayerAttackController>
{
    [SerializeField]
    private HitArea _attackCollider;
    [SerializeField]
    private int _startingBullets = 10;

    public int StartingBullets{
        get { return _startingBullets; }
        }
    [SerializeField]
    private GunShotSpawner _gunShotSpawner;
    private PlayerAnimatorController _animationController;
    private Coroutine _attackCoroutine;
    private AttackConfig _currentAttack;
    [SerializeField]
    private float _giveBulletsInterval = 5;
    private float _giveBulletsTimeout = 3;
    private Vector2 _shotDirection;


    //Attack Control Variables
    public bool canAttack = true;
    public bool canShoot = true;
    public bool giveBulletsEnabled = true;

    public int Bullets { get; private set; }

    public int getMaxBullets()
    {
        return _startingBullets;
    }
    
    public void AddBullet()
    {
        Bullets = Mathf.Min(Bullets+1, _startingBullets);
    }

    private AttackConfig _swordAttackConfig = new AttackConfig()
    {
        AnimatorWeapon = PlayerAnimatorController.Weapon.Sword,
        IntervalInSeconds = 0.5f,
    };
    private AttackConfig _gunAttackConfig = new AttackConfig()
    {
        AnimatorWeapon = PlayerAnimatorController.Weapon.Gun,
        IntervalInSeconds = 0.5f
    };

    protected override void Awake()
    {
        base.Awake();
        _animationController = GetComponent<PlayerAnimatorController>();
        
        Bullets = _startingBullets;
        _shotDirection = new Vector2(1.0f, .1f).normalized;
    }
    private void Start()
    {
        _animationController.OnExecuteAttack += OnExecuteAttack;
    }
    private void OnDestroy()
    {
        _animationController.OnExecuteAttack -= OnExecuteAttack;
    }
    private void Update()
    {
        if(!enabled)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartSwordAttack();
        }
        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            StartGunAttack();
        }

        _giveBulletsTimeout -= Time.deltaTime;
        if(_giveBulletsTimeout <= 0 && giveBulletsEnabled)
        {
            _giveBulletsTimeout = _giveBulletsInterval;
            AddBullet();
        }
    }

    private IEnumerator GiveAmmoCR()
    {
        for(;;)
        {
            yield return new WaitForSeconds(3);
            AddBullet();
        }
    }

    private void OnDisable()
    {
        CancelAttacks();
    }

    private void StartSwordAttack()
    {
        GameEvents.PlayerAction.SwordAttack.SafeInvoke();
        StartAttack(_swordAttackConfig);
    }
    private void StartGunAttack()
    {
        StartAttack(_gunAttackConfig);
    }

    public void CancelAttacks()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }

        _animationController.StopAttackAnimation();
    }

    private void StartAttack(AttackConfig attackConfig)
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        _attackCoroutine = StartCoroutine(AttackCR(attackConfig));
    }

    public IEnumerator AttackCR(AttackConfig attack)
    {
        _animationController.StartAttackAnimation();
        _animationController.ChangeWeapon(attack.AnimatorWeapon);
        
        _currentAttack = attack;
        yield return new WaitForSeconds(attack.IntervalInSeconds);
        _animationController.StopAttackAnimation();
    }

    public void OnExecuteAttack()
    {
        if (_currentAttack == _swordAttackConfig)
        {
            OnExecuteSwordAttack();
        }
        if (_currentAttack == _gunAttackConfig)
        {
            OnExecuteGunAttack();
        }
        CancelAttacks();
    }

    public void OnExecuteSwordAttack()
    {
        StartCoroutine(ExecuteSwordAttackCR());
    }

    private IEnumerator ExecuteSwordAttackCR()
    {
        _attackCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        _attackCollider.gameObject.SetActive(false);
    }

    public void OnExecuteGunAttack()
    {
        if(Bullets > 0)
        {
            GameEvents.PlayerAction.NerfShoot.SafeInvoke();
            _shotDirection.x = Mathf.Abs(_shotDirection.x) * Mathf.Sign(transform.transform.localScale.x);
            _gunShotSpawner.Shot(_shotDirection);
            Bullets--;
            HUDController.Instance.CheckBulletsAmount();
        }
        else
        {
            Debug.Log("Out of ammo");
        }
    }

    [System.Serializable]
    public class AttackConfig
    {
        public PlayerAnimatorController.Weapon AnimatorWeapon { get; set; }
        public float IntervalInSeconds { get; set; }
        public Action OnExecuteAttack { get; set; }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.GetComponent<NerfGunBullet>();
        if(bullet != null)
        {
            if(bullet.CanBeCatched)
            {
                Destroy(bullet.gameObject);
                if (Bullets < StartingBullets) {
                    Bullets++;
                }
                HUDController.Instance.CheckBulletsAmount();
            }
        }
    }
}

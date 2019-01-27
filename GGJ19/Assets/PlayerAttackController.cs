using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerAttackController : MonoBehaviour
{
    [SerializeField]
    private HitArea _attackCollider;
    [SerializeField]
    private int _startingBullets = 2;
    [SerializeField]
    private GunShotSpawner _gunShotSpawner;
    private PlayerAnimatorController _animationController;
    private Coroutine _attackCoroutine;
    private AttackConfig _currentAttack;

    public int Bullets { get; private set; }
    
    public void AddBullet()
    {
        Bullets++;
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

    private void Awake()
    {
        _animationController = GetComponent<PlayerAnimatorController>();
        _animationController.OnExecuteAttack += OnExecuteAttack;
        Bullets = _startingBullets;
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
        if (Input.GetButtonDown("Fire1"))
        {
            StartSwordAttack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            StartGunAttack();
        }
    }

    private void OnDisable()
    {
        CancelAttacks();
    }

    private void StartSwordAttack()
    {
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
        if(_attackCoroutine != null)
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
        Debug.Log("Execute sword attack");
    }

    private IEnumerator ExecuteSwordAttackCR()
    {
        _attackCollider.gameObject.active = true;
        yield return new WaitForSeconds(1);
        _attackCollider.gameObject.active = false;
    }

    public void OnExecuteGunAttack()
    {
        if(Bullets > 0)
        {
            _gunShotSpawner.Shot(transform.right * Mathf.Sign(transform.transform.localScale.x));
            Bullets--;
            Debug.Log("Execute gun attack");
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
                Bullets++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerAttackController : MonoBehaviour
{
    private PlayerAnimatorController _animationController;
    private Coroutine _attackCoroutine;

    private AttackConfig _swordAttackConfig = new AttackConfig()
    {
        AnimatorWeapon = PlayerAnimatorController.Weapon.Sword,
        IntervalInSeconds = 0.1f
    };
    private AttackConfig _gunAttackConfig = new AttackConfig()
    {
        AnimatorWeapon = PlayerAnimatorController.Weapon.Gun,
        IntervalInSeconds = 0.1f
    };

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

    private void Awake()
    {
        _animationController = GetComponent<PlayerAnimatorController>();
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
        yield return new WaitForSeconds(attack.IntervalInSeconds);
        _animationController.StopAttackAnimation();
    }

    [System.Serializable]
    public class AttackConfig
    {
        public PlayerAnimatorController.Weapon AnimatorWeapon { get; set; }
        public float IntervalInSeconds { get; set; }
    }
}

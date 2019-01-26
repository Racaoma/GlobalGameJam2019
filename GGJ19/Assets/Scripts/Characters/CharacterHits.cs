using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHits : MonoBehaviour
{
    public int hitPoints = 1;

    public GameObject hitFX;
    public GameObject deathFX;


    public string hitTriggerName;
    public string deathTriggerName;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void TakeHit()
    {
        Debug.Log("TakeHit");

        hitPoints--;

        if (hitPoints <= 0)
        {
            Die();
        } 
        else
        {
            ShowDamage();
        }
    }


    public void ShowDamage()
    {
        Debug.Log("ShowDamage");

        if (_animator != null)
        {
            _animator.SetTrigger(hitTriggerName);
        }

        Instantiate(hitFX, transform.position, transform.rotation);
    }

    public void Die()
    {
        Debug.Log("Die");

        if (_animator != null)
        {
            _animator.SetTrigger(deathTriggerName);
        }
        
        Instantiate(deathFX, transform.position, transform.rotation);

        Destroy(gameObject);
    }


}

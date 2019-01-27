using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    public int damage = 1;
    [SerializeField]
    private Collider2D _ignoreCollider;
    [SerializeField]
    private bool _oneFrameOnly;
    protected Collider2D _collider;
    private List<Collider2D> visitedColliders = new List<Collider2D>();
    public bool hitEnemies = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        if(_collider != null && _ignoreCollider != null)
        {
            Physics2D.IgnoreCollision(_collider, _ignoreCollider);
        }
    }

    public void LateUpdate()
    {
        if(enabled && gameObject.activeInHierarchy)
        {
            CheckCollisions();
        }
    }

    public void OnEnable()
    {
        visitedColliders.Clear();
        CheckCollisions();

        if (_oneFrameOnly)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void OnHitTarget(GameObject target)
    {
        
    }

    public void CheckCollisions()
    {
        Collider2D[] results = new Collider2D[10];
        int count = _collider.OverlapCollider(new ContactFilter2D(), results);
        for (int i = 0; i < count; i++)
        {
            if (visitedColliders.Contains(results[i]))
            {
                return;
            }


            visitedColliders.Add(results[i]);
            var result = results[i];
            if(hitEnemies)
            {
                var enemy = result.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                    OnHitTarget(result.gameObject);
                }
            }
            else
            {
                var player = result.GetComponent<PlayerStateController>();
                if (player != null)
                {
                    //player take damage;
                }
            }

            if(result.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
            {
                OnHitTarget(result.gameObject);
            }
        }
    }
}

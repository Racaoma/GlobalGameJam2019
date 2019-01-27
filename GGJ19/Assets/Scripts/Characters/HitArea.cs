using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    public int damage = 2;

    [SerializeField]
    private Collider2D _ignoreCollider;
    [SerializeField]
    private bool _oneFrameOnly;

    [SerializeField]
    private LayerMask _collisionLayerMask;

    protected Collider2D _collider;
    private List<Collider2D> visitedColliders = new List<Collider2D>();
    public bool hitEnemies = false;
    protected Rigidbody2D _rigidBody;
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        if(_collider != null && _ignoreCollider != null)
        {
            Physics2D.IgnoreCollision(_collider, _ignoreCollider);
        }

        _rigidBody = GetComponent<Rigidbody2D>();
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
                if (enemy != null && enemy.currentState != enemyState.KnockedDown)
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

            if (_collisionLayerMask.value == (_collisionLayerMask.value | (1 << result.gameObject.layer)))
            {
                OnHitTarget(result.gameObject);
            }
        }
    }
}

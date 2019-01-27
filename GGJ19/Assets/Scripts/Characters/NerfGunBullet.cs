using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfGunBullet : HitArea
{
    public bool CanBeCatched { get; set; }
    private GameObject pivot;

    public override void OnHitTarget(GameObject target)
    {
        if(CanBeCatched)
        {
            return;
        }

        base.OnHitTarget(target);

        _rigidBody.velocity = -GetComponent<Rigidbody2D>().velocity.normalized * 2;
        _rigidBody.AddTorque((Random.Range(0.0f, 1.0f) > 0.5f ? -1 : 1) * Random.Range(600, 1200));
        _rigidBody.isKinematic = false;
        _collider.isTrigger = false;
        damage = 0;
        hitEnemies = false;
        CanBeCatched = true;
    }

    public void Update()
    {
        if(CanBeCatched)
        {
            return;
        }

        var vel = _rigidBody.velocity;
        vel.y -= 9 * Time.deltaTime;
        _rigidBody.velocity = vel;
        if (_rigidBody.velocity.sqrMagnitude > 0.1f)
        {
            transform.right = _rigidBody.velocity.normalized;
        }
    }
}

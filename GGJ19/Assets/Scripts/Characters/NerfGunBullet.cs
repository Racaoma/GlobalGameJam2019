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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        var lossyScale = transform.lossyScale;
        transform.SetParent(target.transform);
        pivot = new GameObject("tempPivot");
        pivot.transform.position = transform.position;
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

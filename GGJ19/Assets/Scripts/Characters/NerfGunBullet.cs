using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfGunBullet : HitArea
{
    public bool CanBeCatched { get; set; }
    public override void OnHitTarget(GameObject target)
    {
        base.OnHitTarget(target);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        transform.parent = target.transform;
        damage = 0;
        hitEnemies = false;
        CanBeCatched = true;
    }
}

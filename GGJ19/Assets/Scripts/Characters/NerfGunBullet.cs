using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfGunBullet : HitArea
{
    public bool CanBeCatched { get; set; }
    private GameObject pivot;

    private void Start()
    {
        base.damage = 1;
    }

    public override void OnHitTarget(GameObject target)
    {
        if(CanBeCatched)
        {
            return;
        }

        base.OnHitTarget(target);
        var enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            GetComponent<Rigidbody2D>().velocity = -GetComponent<Rigidbody2D>().velocity.normalized * 2;
            GetComponent<Rigidbody2D>().AddTorque((Random.Range(0.0f, 1.0f) > 0.5f ? -1 : 1) * Random.Range(600, 1200));
            GetComponent<Rigidbody2D>().isKinematic = false;
            var lossyScale = transform.lossyScale;
            transform.SetParent(target.transform);
            pivot = new GameObject("tempPivot");
            pivot.transform.position = transform.position;
            damage = 0;
            hitEnemies = false;
            CanBeCatched = true;
        }
        else
        {
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

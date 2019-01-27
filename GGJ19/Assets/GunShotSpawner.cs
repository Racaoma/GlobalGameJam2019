using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private float speed = 10;
    
    [SerializeField]
    private GunShotVFX gunShotVFX;

    public void Shot(Vector2 direction)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //bullet.transform.position = transform.position;
        var rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        bullet.transform.right = direction;
        gunShotVFX.Shoot(direction);
    }
}

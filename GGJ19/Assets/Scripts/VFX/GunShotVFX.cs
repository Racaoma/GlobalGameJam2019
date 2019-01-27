using UnityEngine;

public class GunShotVFX : MonoBehaviour
{
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    public void Shoot(Vector2 direction)
    {
        ps.transform.forward = direction;
        ps.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlink : MonoBehaviour
{
    public bool useShared = false;
    public SpriteRenderer rend;
    public float maxBrigthnes;
    private Coroutine blinkingCoroutine;
    private Material material;

    void Awake()
    {
        if (useShared)
        {
            material = rend.sharedMaterial;
        }
        else
        {
            material = rend.material;
        }
    }

    public void StartBlink(float interval, float duration)
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
        }

        blinkingCoroutine = StartCoroutine(Blink(interval, duration));
    }

    private IEnumerator Blink(float interval, float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            float brigthness = Mathf.PingPong(timer, interval) / interval * maxBrigthnes;
            material.SetFloat("_WhiteOverlay", brigthness);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void OnDisable()
    {
        material.SetFloat("_WhiteOverlay", 0f);
    }
}

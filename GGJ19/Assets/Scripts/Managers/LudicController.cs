using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudicController : Singleton<LudicController>
{
    [SerializeField]
    private float ludicMeter;
    private float currentLudicMeter;
    public float maxLudicMeter;
    public float changeSpeed;
    private SpriteRenderer spriteRendererRef;
    public float ludicMeterPercent
    {
        get
        {
            return (float)ludicMeter/(float)maxLudicMeter;
        }
    }

    private void Start()
    {
        spriteRendererRef = this.GetComponent<SpriteRenderer>();
        currentLudicMeter = maxLudicMeter;
        ludicMeter = maxLudicMeter;
    }

    public float getLudicMeter()
    {
        return ludicMeter;
    }

    public void setMaxLudic()
    {
        ludicMeter = maxLudicMeter;
    }

    public void setMinLudic(){
        ludicMeter = 0f;
    }

    public float getRatio(float current, float max)
    {
        return current / max;
    }

    void Update()
    {
        currentLudicMeter = Mathf.Lerp(currentLudicMeter, ludicMeter, changeSpeed * Time.deltaTime);
        spriteRendererRef.color = new Color(1f, 1f, 1f, getRatio(currentLudicMeter, maxLudicMeter));
    }

    public void IncreaseLudicMeter()
    {
        ludicMeter = Mathf.Min(ludicMeter + 1f, maxLudicMeter);
        HUDController.Instance.ChangeLudicMeter();
    }

    public void DecreaseLudicMeter()
    {
        ludicMeter = Mathf.Max(ludicMeter - 1f, 0);
        HUDController.Instance.ChangeLudicMeter();
    }
}

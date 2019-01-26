using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LudicController : Singleton<LudicController>
{
    public static Action<float> UpdateLudicMeter;
    public int ludicMeter;
    private float ludicMeter_continous;
    public int maxLudicMeter;
    public float changeSpeed = 2f;
    void Update()
    {
        if (Mathf.Abs(ludicMeter_continous - (float)ludicMeter) > 0.05f)
        {
            ludicMeter_continous = Mathf.Lerp(ludicMeter_continous, ludicMeter, changeSpeed * Time.deltaTime);
            UpdateLudicMeter(ludicMeter_continous/(float)maxLudicMeter);
        }
    }
    public void IncreaseLudicMeter()
    {
        ludicMeter = Mathf.Max(ludicMeter + 1, maxLudicMeter);
    }

    public void DecreaseLudicMeter()
    {
        ludicMeter = Mathf.Min(ludicMeter - 1, 0);
    }
}

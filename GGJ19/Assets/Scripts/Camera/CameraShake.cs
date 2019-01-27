using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public enum ShakeDuration { small, medium, large };
    public enum ShakeIntensity { weak, medium, strong };
    public ShakeDuration shakeDuration_option = ShakeDuration.small;
    public ShakeIntensity shakeIntensity_option = ShakeIntensity.strong;
    // Desired duration of the shake effect
    public float shakeDuration_short = 0.5f;
    public float shakeDuration_medium = 1f;
    public float shakeDuration_long = 2f;
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude_weak = 0f;
    public float shakeMagnitude_medium = 0f;
    public float shakeMagnitude_strong = 0f;
    private float shakeMagnitude = 0f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 0f;

    // The initial position of the GameObject
    Vector3 initialPosition;


    void OnEnable()
    {
        initialPosition = transform.localPosition;

        // register events
        GameEvents.PlayerAction.TookDamage += PlayerTookDamage;
    }

    void OnDisable()
    {
        GameEvents.PlayerAction.TookDamage -= PlayerTookDamage;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(ShakeDuration d, ShakeIntensity i)
    {
        shakeDuration_option = d;
        shakeIntensity_option = i;
        ActivateShake();
    }

    public void ActivateShake()
    {
        switch (shakeDuration_option)
        {
            case ShakeDuration.small:
                shakeDuration = shakeDuration_short;
                break;
            case ShakeDuration.medium:
                shakeDuration = shakeDuration_medium;
                break;
            case ShakeDuration.large:
                shakeDuration = shakeDuration_long;
                break;
            default:
                break;
        }

        switch (shakeIntensity_option)
        {
            case ShakeIntensity.weak:
                shakeMagnitude = shakeMagnitude_weak;
                break;
            case ShakeIntensity.medium:
                shakeMagnitude = shakeMagnitude_medium;
                break;
            case ShakeIntensity.strong:
                shakeMagnitude = shakeMagnitude_strong;
                break;
            default:
                break;
        }
    }

    private void PlayerTookDamage()
    {
        TriggerShake(ShakeDuration.small, ShakeIntensity.weak);
    }

}

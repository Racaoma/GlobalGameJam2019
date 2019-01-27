using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmersionController : MonoBehaviour
{
    //Variables
    private SpriteRenderer spriteRendererRef;

    // Start is called before the first frame update
    void Start()
    {
        spriteRendererRef = this.GetComponent<SpriteRenderer>();
        LudicController.UpdateLudicMeter += updateAlpha;
    }

    private void OnDestroy()
    {
        LudicController.UpdateLudicMeter -= updateAlpha;
    }

    public void updateAlpha(float value)
    {
        spriteRendererRef.color = new Color(255f, 255f, 255f, value * 255);
    }
}

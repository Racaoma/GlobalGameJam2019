using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zorderer : MonoBehaviour
{
    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * 1000.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

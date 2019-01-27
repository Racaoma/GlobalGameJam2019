using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public float speed;
    private float timer;
    public float lifeTime = 3f;

    void Update()
    {
        transform.position += transform.forward.normalized * speed * Time.deltaTime;
        timer += Time.deltaTime;

        if(timer>=lifeTime){
            gameObject.SetActive(false);
        }
    }

    public void Reset(){
        timer = 0f;
    }


}

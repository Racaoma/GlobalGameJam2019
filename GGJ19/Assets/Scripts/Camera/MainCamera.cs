using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;

    public float speed = 10f;

    public Transform minBoundX;
    public Transform maxBoundX;
    public Transform minBoundY;
    public Transform maxBoundY;
    public float offsetX;
    public float offsetY;

    private Vector3 newPosition = new Vector3();
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();

        offsetX = (_camera.aspect * _camera.orthographicSize);
        offsetY = _camera.orthographicSize;
    }
    
    void Update()
    {
        /*if (target.position.x < minBoundX.position.x + offsetX ||
            target.position.x > maxBoundX.position.x - offsetX ||
            target.position.y < minBoundY.position.y + offsetY ||
            target.position.y > maxBoundY.position.y - offsetY)
        {
            return;
        }*/

        newPosition = transform.position;
        
        newPosition.x = Mathf.Clamp(target.position.x, minBoundX.position.x + offsetX, maxBoundX.position.x - offsetX);
        newPosition.y = Mathf.Clamp(target.position.y, minBoundY.position.y + offsetY, maxBoundY.position.y - offsetY);

        transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    private float speed = 5;
    public Vector3 targetPosition;
    
    public float expirationTime;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((targetPosition - transform.position).normalized * (Time.deltaTime * speed));
        if (this.transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
}

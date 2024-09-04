using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float xSpeed = 5f;
    private float xVector = 0f;
    public bool inCave;

    private Rigidbody2D rb;

    public float ySpeed = 5f;
    private float yVector = 0f;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle input
        float horizontalInput = Input.GetAxis("Horizontal");
        xVector = xSpeed * horizontalInput;
        float verticalInput = Input.GetAxis("Vertical");
        yVector = ySpeed * verticalInput;
        rb.AddForce(new Vector2(xVector, yVector));

    }
}
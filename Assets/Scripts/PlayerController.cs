using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float xSpeed = 5f;
    private float xVector = 0f;
    public bool inCave=false;
    public AudioSource metalpipe;
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
        xVector = xSpeed * horizontalInput*Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical");
        yVector = ySpeed * verticalInput*Time.deltaTime;
        transform.Translate(xVector, yVector,0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("collision");
        if (other.tag == "Coin")
        {
            Debug.Log("oww");
            metalpipe.Play();
        }
    }
}
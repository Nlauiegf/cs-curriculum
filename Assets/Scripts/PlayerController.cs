using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float xSpeed = 50f;
    private float xVector = 0f;
    public bool inCave=false;
    public AudioSource metalpipe;
    private Rigidbody2D rb;
    public GameManager gm;

    public float ySpeed = 50f;
    private float yVector = 0f;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Handle input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        xVector = xSpeed * horizontalInput*Time.deltaTime;
        yVector = ySpeed * verticalInput*Time.deltaTime;
        transform.Translate(xVector, yVector,0);
        // normalize distance so that diagonal isnt too fast
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("collision");
        if (other.tag == "Coin")
        {
            Debug.Log("Lets Go I Found A Coin!");
            gm.coins += 1;
            metalpipe.Play();
            if (gm.coins == 1)
            {
                Debug.Log("I now have 1 coin");
            }
            else
            {
                Debug.Log("I now have " + gm.coins + " coins");
            }
            Destroy(other.gameObject);
        }
    }
}
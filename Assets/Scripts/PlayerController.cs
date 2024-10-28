using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float xSpeed = 5f;
    private float xVector = 0f;
    public bool inCave = false;
    public AudioSource metalpipe;
    private Rigidbody2D rb;
    public DUDEGUY dg;
    public GameManager gm;
    public float ySpeed = 5f;
    private float yVector = 0f;
    //public Vector3 mtpvp;
    //public string direction;
    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        dg = FindObjectOfType<DUDEGUY>();
    }

    void Update()
    {
        /*mtpvp = ((.transform.position) - transform.position);
        if (Mathf.Abs(mtpvp.x)>Mathf.Abs(mtpvp.y))
        {
            if (mtpvp.x > 0)
            {
                direction = "Right_";
            }
            else
            {
                direction = "Left_";
            }
        }

        if (Mathf.Abs(mtpvp.y)>Mathf.Abs(mtpvp.x))
        {
            if (mtpvp.y > 0)
            {
                direction = "Up_";
            }
            else 
            {
                direction = "Down_";
            }
           
        }*/
        // Handle input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        xVector = xSpeed * horizontalInput*Time.deltaTime;
        yVector = ySpeed * verticalInput*Time.deltaTime;
        transform.Translate(xVector, yVector,0);
        // normalize distance so that diagonal isnt too fast
        if (Input.GetMouseButton(0))
        {
            dg.changeEnemyHealth(-3);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision");
        if (other.tag == "Coin")
        {
            gm.changeCoins( 1);
            Destroy(other.gameObject);
        }
        
        if (other.tag == "Spikes")
        {
            gm.changeHealth( -1);
        }

        if (other.tag == "Tourette Projectile")
        {
            gm.changeHealth(-2);
            Destroy(other.gameObject);
        }
        metalpipe.Play();
    }
}
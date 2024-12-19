using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    // Movement variables
    public Vector2 GravDirec = Vector2.down;
    public bool canSwap = true;
    public bool AxeEquipped = false;
    public bool closeToDoor = false;
    public float gravity = 1f;
    public TopDown_AnimatorController TDown;
    public int pdamage = 3;
    public float xSpeed = 5f;
    private float xVector = 0f;
    public bool inCave = false;
    public AudioSource metalpipe;
    private Rigidbody2D rb;
    public DUDEGUY dg;
    public SpriteRenderer sr;
    public PlayerController cp;
    public GameManager gm;
    public float ySpeed = 5f;
    private float yVector = 0f;
    public float cooldown = 0;
    public GameObject door;
    public int Gravity = 1;
    public float Jumps = 0;
    public bool Overwold = false;
    public bool canJump = true;
    public float JumpForce = 5f;
    //public Vector3 mtpvp;
    //public string direction;
    void Start()
    {
        // Get the Rigidbody2D component
        TDown = FindObjectOfType<TopDown_AnimatorController>();
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        dg = FindObjectOfType<DUDEGUY>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BeSmol();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            BeBig();
        }
        if (!Overwold)
        {
            ySpeed=0;
            canJump = true;
        }
        if (Overwold)
        {
            canJump = false;
            ySpeed = 5f;
        }
        // Handle input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        xVector = xSpeed * horizontalInput*Time.deltaTime;
        yVector = ySpeed * verticalInput*Time.deltaTime;
        transform.Translate(xVector, yVector,0);
        cooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q) && canSwap && !Overwold)
        {
            SwapGravity();
            canSwap = false;
        }
        if (Input.GetMouseButton(0) && cooldown <= 1 && dg.enemyAlive)
        {
            if(Vector3.Distance(dg.transform.position, transform.position) < 3)
            {
                dg.ChangeEnemyHealth(pdamage);
                Debug.Log("hit");
                cooldown = 2;
            }
        }
        
        if (closeToDoor && Input.GetMouseButton(0) && AxeEquipped)
        {
            Debug.Log("door hit");
            Debug.Log(door);
            Destroy(door);
        }
        
        if (Physics2D.Raycast(transform.position, GravDirec, 1f) && canJump)
        { 
            canSwap = true;
            if (Input.GetKey(KeyCode.Space) && cooldown <= 0)
            {
                Jumps = 0;
                Jump();
                Debug.Log("jump1");
                cooldown = .5f;
            }
        }
        if (Jumps < 2 && Jumps > 0 && cooldown <= 0 && canJump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("jump2");
                Jump();
            }
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
            gm.changeHealth( -2);
            Destroy(other.gameObject);
        }
        if (other.tag == "Axe")
        {
            TDown.SwitchToAxe();
            gm.changeCoins( 5);
            AxeEquipped = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Door")
        {
            closeToDoor = true;
            door = other.gameObject;
            Debug.Log("close to door");
        }
        if (other.tag == "Door")
        {
            if (Overwold)
            {
                Overwold = false;
            }
            if (!Overwold)
            {
                Overwold = true;
            }
        }
        if (other.tag == "Dying now")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        metalpipe.Play();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Door")
        {
            closeToDoor = false;
            Debug.Log("not close to door");
        }
    }
    private void Jump()
    {
        if (Jumps < 2)
        {
            rb.AddForce(Vector2.up * gravity * JumpForce, ForceMode2D.Impulse);
            Jumps++;
        }
    }
    private void SwapGravity()
    {
        gravity *= -1;
        sr.flipY = !sr.flipY;
        rb.gravityScale = gravity;
        if (gravity == 1)
        {
            GravDirec = Vector2.down;
        }
        else
        {
            GravDirec = Vector2.up;
        }
    }
    private void BeSmol()
    {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }
    private void BeBig()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
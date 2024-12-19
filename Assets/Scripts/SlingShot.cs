using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController pc;
    public bool drawLine = false;
    public LineRenderer line;
    public Vector3 target;
    public Vector3 playerTarget;
    public Rigidbody2D rb;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        rb = pc.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drawLine) 
        {
            line.SetPosition(0, playerTarget);
            line.SetPosition(1, target);
            pc.transform.Translate((target - pc.transform.position).normalized * (Time.deltaTime * 10));

        }
        if (Input.GetKeyDown("f"))
        {
            drawLine = true;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerTarget= pc.transform.position;
            rb.gravityScale = 0;
        }
        if (Input.GetKeyDown("g"))
        {
            drawLine = false;
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));
            rb.gravityScale = 1;
        }
    }
}

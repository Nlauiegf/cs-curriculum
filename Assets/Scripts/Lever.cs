using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool StartPlatformMoving = false;
    public PlayerController pc;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && Vector3.Distance(transform.position, pc.transform.position) < 3)
        {
            Debug.Log("LeverGronked");
            StartPlatformMoving = true;
        }
    }
}
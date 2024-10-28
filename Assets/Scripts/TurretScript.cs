using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private float cooldown;
    private float firerate = 1;
    public GameObject bullet;
    private GameObject player;
    
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <0 && player)
        {
            Shoot();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
    
    void Shoot()
    {
        
        Debug.Log("Yippee");
        GameObject bulletInstance = Instantiate(bullet, transform.position, quaternion.identity);
        bulletInstance.GetComponent<BulletScript>().targetPosition = player.transform.position;
        cooldown = firerate;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class DUDEGUY : MonoBehaviour
{
    public float speed = 4;
    private int targetWaypoint = 0;
    public List<GameObject> waypoints;
    public GameObject player;
    public Animator mobileEnemy;
    public Vector3 mtpv;
    public string direction;
    public float cooldown;
    Vector3 targetPosition;
    private GameManager gm;
    public int OrcHealth = 5;
    public GameObject Axe;
    public bool alive = true;
    public DUDEGUY dg;
    public bool enemyAlive = true;
    public PlayerController pc;
    public enum states
    {
        Attack,
        Patrol,
        Chase,
        Death
    }

    public states state;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pc = FindObjectOfType<PlayerController>();
    }

    //state = states.Attack;
    void Update()
    {
        if (Mathf.Abs(mtpv.x)>Mathf.Abs(mtpv.y))
        {
            if (mtpv.x > 0)
            {
                direction = "Right";
            }
            else
            {
                direction = "Left";
            }
        }

        if (Mathf.Abs(mtpv.y)>Mathf.Abs(mtpv.x))
        {
            if (mtpv.y > 0)
            {
                direction = "Up";
            }
            else 
            {
                direction = "Down";
            }
           
        }
        if (state == states.Chase && alive)
        {
            Chase();
        }

        if (state == states.Attack && alive)
        {
            Attack();
        }

        if (state == states.Patrol && alive)
        {
            Patrol();
        }

        if (state == states.Death)
        {
            Death();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    void Chase()
    {
        if (player)
        {
            targetPosition = player.transform.position;
        }
     

        mtpv = (targetPosition - transform.position).normalized;
        mobileEnemy.Play("Walk" + direction);
        transform.Translate(mtpv * (speed * Time.deltaTime));
        if (Vector3.Distance(waypoints[targetWaypoint].transform.position, transform.position) > 13)
        {
            player = null;
        }
        if (player == null)
        {
            state = states.Patrol;
        }
        if (Vector3.Distance(targetPosition, transform.position) < 1 && player)
        {
            state = states.Attack;
        }
    }

    void Patrol()
    {
        targetPosition = waypoints[targetWaypoint].transform.position;
        mtpv = (targetPosition - transform.position).normalized;
        mobileEnemy.Play("Walk" + direction);
        if (Vector3.Distance(waypoints[targetWaypoint].transform.position, transform.position) < .015)
        {
            targetWaypoint++;
            targetWaypoint %= waypoints.Count;
        }
        transform.Translate(mtpv * (speed * Time.deltaTime));
        if (player)
        {
            state = states.Chase;
        }
    }

    void Attack()
    {
        cooldown -= Time.deltaTime;
        targetPosition = player.transform.position;
        if (Vector3.Distance(targetPosition, transform.position) > 2 && player)
        {
            state = states.Chase;
        }

        if (Vector3.Distance(targetPosition, transform.position) < 2 && player && cooldown < 1)
        {
            mobileEnemy.Play("Attack" + direction);
            cooldown = 2;
            gm.changeHealth(-3);
        }
    }

    public void ChangeEnemyHealth(int damage)
    {
        //add health above maybe?
        OrcHealth -= damage;
        if (OrcHealth < 1)
        {
            state = states.Death;
            enemyAlive = false;
            Instantiate(Axe, mobileEnemy.transform.position, Quaternion.identity);
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}

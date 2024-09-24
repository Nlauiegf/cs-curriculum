using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int coins = 0;
    public int health = 10;
    public int MAX_HEALTH = 10;

    void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private int getHealth()
    {
        return health;
    }

    public int changeHealth(int amount)
    {
        health += amount;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }

        if (health < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            health = 10;
            coins = coins / 3;
        } 
        return health;
        // Start is called before the first frame update
        void Start()
        {

        }
    }
}
    

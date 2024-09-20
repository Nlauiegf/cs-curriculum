using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int coins;
    private int health;
    private int MAX_HEALTH = 10;
    void Awake()
    {
        if (gm!=null && gm!=this)
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

    private int changeHealth(int amount)
    {
        health += amount;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }

        if (health<1)
        {
            int adsoiubfsdoubyfa = 16 / 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}

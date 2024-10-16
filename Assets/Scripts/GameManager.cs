using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI Coins;
    public TextMeshProUGUI Health;
    public static GameManager gm;
    public int coins = 0;
    public int health = 5;
    public int MAX_HEALTH = 5;

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

    public int changeCoins(int amount)
    {
        coins += amount;
        Coins.text = "Coins: " + gm.coins;
        return coins;
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
        Health.text = "Health: " + gm.health;
        return health;
    }
}
    

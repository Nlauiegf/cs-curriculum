using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnscreenGui : MonoBehaviour
{
    public GameManager gm;

    public TextMeshProUGUI coins;

    public string coinString;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = coinString;
        coinString = "" + gm.coins;
    }
}

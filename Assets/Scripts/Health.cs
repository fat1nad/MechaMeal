using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    static public Health instance;

    public int decrement;

    int health;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        health = 100;
    }

    public void ReduceHealth()
    {
        health -= decrement;
        // show this visually

        if (health <= 0)
        {
            Manager.instance.GameOver();
        }
    }

}

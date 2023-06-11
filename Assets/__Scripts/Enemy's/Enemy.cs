using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float MaxHealth;

    void Start()
    {
        health = MaxHealth;
    }
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<PlayerMovement>().currentRoom.CheckKillCount();
        }
    }
}

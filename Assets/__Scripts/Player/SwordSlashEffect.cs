using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashEffect : MonoBehaviour
{
    private WeaponController controller;
    private Rigidbody2D rb;
    private Animator anim;
    
    void Start()
    {
        controller = FindObjectOfType<WeaponController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.velocity = transform.right * controller.slashMoveSpeed;
        anim.speed = 1;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

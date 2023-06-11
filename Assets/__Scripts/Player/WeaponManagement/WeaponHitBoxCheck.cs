using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBoxCheck : MonoBehaviour
{
    public bool AlreadyAttacking;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && FindObjectOfType<WeaponController>().attacking && !AlreadyAttacking)
        {
            AlreadyAttacking = true;
            collision.GetComponent<Enemy>().health -= FindObjectOfType<WeaponController>().currentHoldingItem.GetComponent<HotBarholder>().Item.Damage;
            Debug.Log(collision);
            Debug.Log(FindObjectOfType<WeaponController>().currentHoldingItem.GetComponent<HotBarholder>().Item.Damage);
        }
    }

    private void Update()
    {
        if (!FindObjectOfType<WeaponController>().attacking)
            AlreadyAttacking = false;
    }
}

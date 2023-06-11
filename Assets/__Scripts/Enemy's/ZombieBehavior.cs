using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    private PlayerMovement player;
    private Transform spriteChild;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        spriteChild = GetComponentInChildren<SpriteRenderer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        spriteChild.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}

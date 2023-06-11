using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    private PlayerMovement player;
    private Rigidbody2D rb;

    public float distance;
    public bool inRange;
    public float walkSpeed;

    private Vector3 rotation;



    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        bool isUpsideDown = (angle > 90 || angle < -90);

        transform.localScale = new Vector3(1, isUpsideDown ? -1 : 1, 1);

        Vector3 circleDirection = new Vector3(-direction.y, direction.x, 0f).normalized * 2;

        if (IsInRange())
        {
            Vector3 newPosition = player.transform.position + circleDirection * walkSpeed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, walkSpeed * Time.deltaTime);
        }
        if (!IsInRange())
        {
            rb.velocity = Vector2.zero;
        }
    }

    private bool IsInRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            inRange = true;
            walkSpeed = Vector3.Distance(transform.position, player.transform.position) * Random.Range(0.5f, 2.5f);
            return true;
        }
        else {
            inRange = false;
            return false;
        }
    }
}

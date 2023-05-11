using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement")] 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 movementVector;
    [SerializeField] private float speed;
    public bool canWalk = false;

    [Header("Animations")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private GameObject spriteRenderer;
    [SerializeField] private bool collided;
    [SerializeField] private float raycastDistance = 1f;
    [SerializeField] private LayerMask wallLayer;

    [Header("Looking / Weapon")]
    [SerializeField] private int lookingDir;
    [SerializeField] private int lookdir;
    private WeaponController weaponController;

    [Header("NPC / Interaction")]
    public bool talking;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponController = GetComponent<WeaponController>();
        canWalk = true;
    }

    private void Update()
    {
        //MovementInput
        if (canWalk)
            Movement();
    }
    private void FixedUpdate()
    {
        //MovementOutput
        if (canWalk)
        {
            rb.velocity = movementVector.normalized * speed * Time.fixedDeltaTime;
            WalkAnimationManager();
        }
    }

    private void Movement()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

    }
    private void WalkAnimationManager()
    {
 
        lookdir = lookingDir;
        if (movementVector.x == 0 && movementVector.y == 0)
        {
            playerAnim.SetBool("StandingStill", true);
            return;
        }
        else
            playerAnim.SetBool("StandingStill", false);

        //Checking if Moving
        if (movementVector.y > 0)
        {
            lookingDir = 2;
            playerAnim.SetBool("MovingBack", true);
        }
        else
        {
            lookingDir = 3;
            playerAnim.SetBool("MovingBack", false);
        }
        if (movementVector.x < 0)
        {
            spriteRenderer.transform.localScale = new Vector2(-1, 1);
            lookingDir = 0;
        }
        else if (movementVector.x > 0)
        {
            lookingDir = 1;
            spriteRenderer.transform.localScale = new Vector2(1, 1);
        }
        if (lookingDir != lookdir)
        {
            Debug.Log("hi");
            weaponController.ChangeWeaponPos(lookingDir);
        }
        //RayCollisionCheck();
    }
    //Stop Animation when collided
    private void RayCollisionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movementVector, raycastDistance, wallLayer);
        if (hit.collider != null)
        {
            playerAnim.SetBool("StandingStill", true);
            return;
        }
        //else
        //    playerAnim.SetBool("StandingStill", true);
    }

    public void StopWalking()
    {
        canWalk = false;
        movementVector = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

}

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

    [Header("Animations")]
    [SerializeField] private Animator playerAnim;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //MovementInput
        Movement();
    }

    private void FixedUpdate()
    {
        //MovementOutput
        rb.velocity = movementVector * speed * Time.fixedDeltaTime;
        WalkAnimationManager();
    }

    private void Movement()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
    }
    private void WalkAnimationManager()
    {
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
            playerAnim.SetBool("MovingBack", true);
        }
        else
        {
            playerAnim.SetBool("MovingBack", false);
        }
    }

}

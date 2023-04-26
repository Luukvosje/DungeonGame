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
    [SerializeField] private GameObject spriteRenderer;

    [Header("Looking")]
    public static int lookingDir;


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
        rb.velocity = movementVector.normalized * speed * Time.fixedDeltaTime;
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
    }

}

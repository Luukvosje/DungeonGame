using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{

    private DialogueTrigger dialogueTrigger;
    private Animator anim;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !FindObjectOfType<PlayerMovement>().talking)
        {
            dialogueTrigger.TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Open", true);
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("Open", false);
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }


}
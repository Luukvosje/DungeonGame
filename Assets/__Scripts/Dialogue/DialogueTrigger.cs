using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueSystem>().StartDialogueSystem(dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TriggerDialogue();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public Animator anim;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI DialogueText;

    public Queue<string> sentences = new Queue<string>();

    private bool typing = false;
    private float waitTime = 0.08f;

    public void StartDialogueSystem(Dialogue dialogue)
    {

        anim.SetBool("IsOpen", true);
        Name.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        NextDialogueSentence();
    }

    public void NextDialogueSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

       string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        waitTime = 0.08f;
        typing = true;
        foreach (char item in sentence.ToCharArray())
        {
            DialogueText.text += item;
            yield return new WaitForSeconds(waitTime);
        }
        typing = false;
    }

    private void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
        FindObjectOfType<PlayerMovement>().talking = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!typing)
                NextDialogueSentence();
            else
            {
                waitTime = 0.00001f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public DialogueManager dm;
    public Dialogue dia;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dia);
    }

    public void Interact()
    {
        TriggerDialogue();
    }
}

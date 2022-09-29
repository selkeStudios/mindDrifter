using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour, IInteractable
{
    public GameController gc;
    public GameObject speechBox;
    public Text name_;
    public Text speech;

    public string[] names;
    public string[] sentences;
    
    public int sentence = 0;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void StartDialogue(Dialogue dia)
    {
        if (!gc.talking)
        {
            gc.talking = true;
            names = dia.names;
            sentences = dia.sentences;
            speechBox.SetActive(true);
            DisplayNextSentence();
        }
    }

    void DisplayNextSentence()
    {
        if (sentence < sentences.Length)
        {
            name_.text = names[sentence];
            speech.text = sentences[sentence];
            sentence++;
        }
        else
        {
            gc.talking = false;
            speechBox.SetActive(false);
            sentence = 0;
        }
    }

    public void Interact()
    {
        DisplayNextSentence();
    }
}

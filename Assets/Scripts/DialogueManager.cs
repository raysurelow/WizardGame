using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public Text dialogueText;
    public Queue<string> sentences;
    public Canvas canvas;

    // Use this for initialization
    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        canvas.enabled = false;
        Time.timeScale = 1f;
    }


}

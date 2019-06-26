using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
    public Canvas canvas;
    public Button continueButton;
    private bool dialogueTriggered;

    void Start()
    {
        dialogueTriggered = false;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            

            if (!dialogueTriggered)
            {
                TriggerDialogue();
                dialogueTriggered = true;
                canvas.enabled = true;
                EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
                es.SetSelectedGameObject(continueButton.gameObject);
                Time.timeScale = 0f;
            }
        }
    }
}

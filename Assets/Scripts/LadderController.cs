using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour {

    private WizardController player;
    private EdgeCollider2D topCollider;
    public bool someoneClimbing;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<WizardController>();
        topCollider= GetComponent<EdgeCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (someoneClimbing)
        {
            topCollider.enabled = false;
        }
        else
        {
            topCollider.enabled = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.onLadder = false;
        }
    }
}

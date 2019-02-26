using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LadderController : MonoBehaviour {

    private WizardController wizard;
    private EdgeCollider2D topCollider;
    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player

    // Use this for initialization
    void Start () {
        topCollider= GetComponentInChildren<EdgeCollider2D>();
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }
	
	// Update is called once per frame
	void Update () {
        if (wizard)
        {
            foreach(Collider2D collider in wizard.gameObject.GetComponents<Collider2D>())
            {
                Physics2D.IgnoreCollision(collider, topCollider, player.GetAxisRaw("Climb Ladder") == -1);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wizard = collision.gameObject.GetComponent<WizardController>();
            if (wizard != null)
            {
                wizard.onLadder = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && (collision is CapsuleCollider2D))
        {
            if (wizard != null)
            {
                wizard.onLadder = false;
                wizard = null;
            }
        }
    }
}

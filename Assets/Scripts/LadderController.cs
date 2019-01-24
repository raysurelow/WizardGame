using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour {

    private WizardController player;
    private EdgeCollider2D topCollider;

	// Use this for initialization
	void Start () {
        topCollider= GetComponentInChildren<EdgeCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (player)
        {
            foreach(Collider2D collider in player.gameObject.GetComponents<Collider2D>())
            {
                Physics2D.IgnoreCollision(collider, topCollider, Input.GetAxisRaw("Vertical") == -1);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<WizardController>();
            if (player != null)
            {
                player.onLadder = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && (collision is CapsuleCollider2D))
        {
            if (player != null)
            {
                player.onLadder = false;
                player = null;
            }
        }
    }
}

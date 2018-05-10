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
        if (player && player.climbInitialized)
        {
            //topCollider.enabled = false;
        }
        else
        {
            //topCollider.enabled = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        print(collision.gameObject.GetComponent<Collider>().bounds.min.y < topCollider.bounds.min.y);
        if (collision.gameObject.GetComponent<Collider>().bounds.min.y < topCollider.bounds.min.y)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponentInChildren<Collider>());
        }
        */
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
        if (collision.gameObject.tag == "Player")
        {
            if (player != null)
            {
                player.onLadder = false;
            }
        }
    }
}

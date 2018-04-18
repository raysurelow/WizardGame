using UnityEngine;
using System.Collections;

public class StickSwitchController : AbstractSwitch {

    private Animator animator;
    private bool playerInProximity;

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("IsSwitchedOn", IsSwitchedOn);
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (playerInProximity && Input.GetButtonDown("Fire1"))
        {
            IsSwitchedOn = !IsSwitchedOn;
            animator.SetBool("IsSwitchedOn", IsSwitchedOn);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInProximity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInProximity = false;
        }
    }
}

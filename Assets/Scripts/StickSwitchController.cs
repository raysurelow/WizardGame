using UnityEngine;
using System.Collections;

public class StickSwitchController : AbstractSwitch {

    public bool isTimedSwitch;
    public float timedSwitchDuration;
    private Animator animator;
    private bool playerInProximity;
    private float elapsedTime;

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("IsSwitchedOn", IsSwitchedOn);
	}
	
	// Update is called once per frame
	protected override void Update () {
        if(isTimedSwitch && IsSwitchedOn)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timedSwitchDuration)
            {
                IsSwitchedOn = false;
                elapsedTime = 0;
            }
        }
        if (playerInProximity && Input.GetButtonDown("Fire1"))
        {
            IsSwitchedOn = !IsSwitchedOn;
        }
        animator.SetBool("IsSwitchedOn", IsSwitchedOn);
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

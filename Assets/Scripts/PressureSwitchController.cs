using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitchController : AbstractSwitch {

    private Animator animator;
    private int triggerCount;

	// Use this for initialization
	protected override void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("IsSwitchedOn", IsSwitchedOn);
    }
	
	// Update is called once per frame
	protected override void Update () {
		if (triggerCount > 0)
        {
            IsSwitchedOn = true;
        } else
        {
            IsSwitchedOn = false;
        }

        animator.SetBool("IsSwitchedOn", IsSwitchedOn);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerCount--;
    }
}

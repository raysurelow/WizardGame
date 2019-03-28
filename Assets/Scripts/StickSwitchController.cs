using UnityEngine;
using System.Collections;
using Rewired;

public class StickSwitchController : AbstractSwitch {

    public bool isTimedSwitch;
    public float timedSwitchDuration;
    private Animator animator;
    private bool playerInProximity;
    private float elapsedTime;

    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("IsSwitchedOn", IsSwitchedOn);
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
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
        if (playerInProximity && player.GetButtonDown("FlipSwitch"))
        {
            IsSwitchedOn = !IsSwitchedOn;
            elapsedTime = 0;
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

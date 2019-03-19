using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CameraController : MonoBehaviour {

    public GameObject wizard;
    private Vector3 targetPosition;
    public float smoothing;
    public float cameraHorizontalGive;
    public float cameraVerticalGive;
    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player
    public float cameraMaxLookDistance;
    private Vector3 targetLookPosition;
    private float currentX;
    private float currentY;
    private float maxCurrentX;
    private float maxCurrentY;
    private float minCurrentX;
    private float minCurrentY;
    private bool looking;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(wizard.transform.position.x, wizard.transform.position.y, -10);
        targetPosition = new Vector3(wizard.transform.position.x, wizard.transform.position.y, -10);
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }
	
	// Update is called once per frame
	void Update () {
        /*if (!player.activeSelf)
        {
            findActivePlayer();
        }*/
        if (wizard != null)
        {
            //handle following player as he moves
            FollowPlayer();

            //handle looking around while stationary only
            /*if(player.GetAxisRaw("Move Horizontal") == 0)
            {
                LookAround();
            }*/
            
        }
    }

    private void FollowPlayer()
    {
        //check if player is far enough from the camera to make it move
        float playerDistanceFromCameraX = wizard.transform.position.x - transform.position.x;
        float playerDistanceFromCameraY = wizard.transform.position.y - transform.position.y;
        float targetPositionX;
        float targetPositionY;

        //account for horizontal adjustment
        if (playerDistanceFromCameraX > cameraHorizontalGive)
        {
            targetPositionX = wizard.transform.position.x - cameraHorizontalGive;
        }
        else if (playerDistanceFromCameraX < -cameraHorizontalGive)
        {
            targetPositionX = wizard.transform.position.x + cameraHorizontalGive;
        }
        else
        {
            targetPositionX = transform.position.x;
        }

        //account for vertical adjustment
        if (playerDistanceFromCameraY > cameraVerticalGive)
        {
            targetPositionY = wizard.transform.position.y - cameraVerticalGive;
        }
        else if (playerDistanceFromCameraY < -cameraVerticalGive)
        {
            targetPositionY = wizard.transform.position.y + cameraVerticalGive;
        }
        else
        {
            targetPositionY = transform.position.y;
        }

        //update target position with adjusted horizontal/vertical positions and Lerp towards target
        targetPosition = new Vector3(targetPositionX, targetPositionY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }

    /*
    private void LookAround()
    {
        if(player.GetAxisRaw("Move Horizontal") > 0)
        {
            targetX cameraMaxLookDistance;
        }

        transform.position = Vector3
    }*/

    /*public void findActivePlayer()
    {
        //Make generic player finding method for this and SpellButtonScript
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            WizardController wizardScript = player.GetComponent<WizardController>();
            if (wizardScript.isActivePlayer && player.activeSelf)
            {
                this.player = player;
            }
        }
    }
    */


}

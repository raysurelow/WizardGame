using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 targetPosition;
    public float smoothing;
    public float cameraHorizontalGive;
    public float cameraVerticalGive;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(0, player.transform.position.y, -10);
        targetPosition = new Vector3(0, player.transform.position.y, -10);
	}
	
	// Update is called once per frame
	void Update () {
        /*if (!player.activeSelf)
        {
            findActivePlayer();
        }*/
        if (player != null)
        {
            //check if player is far enough from the camera to make it move
            float playerDistanceFromCameraX = player.transform.position.x - transform.position.x;
            float playerDistanceFromCameraY = player.transform.position.y - transform.position.y;
            float targetPositionX;
            float targetPositionY;

            //account for horizontal adjustment
            if (playerDistanceFromCameraX > cameraHorizontalGive)
            {
                targetPositionX = player.transform.position.x - cameraHorizontalGive;
            }else if(playerDistanceFromCameraX < -cameraHorizontalGive)
            {
                targetPositionX = player.transform.position.x + cameraHorizontalGive;
            }else
            {
                targetPositionX = transform.position.x;
            }

            //account for vertical adjustment
            if (playerDistanceFromCameraY > cameraVerticalGive)
            {
                targetPositionY = player.transform.position.y - cameraVerticalGive;
            }else if (playerDistanceFromCameraY < -cameraVerticalGive)
            {
                targetPositionY = player.transform.position.y - cameraVerticalGive;
            }else
            {
                targetPositionY = transform.position.y;
            }

            //update target position with adjusted horizontal/vertical positions and Lerp towards target
            targetPosition = new Vector3(targetPositionX, targetPositionY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }

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

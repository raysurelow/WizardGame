using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 targetPosition;
    public float smoothing;
    public float cameraHorizontalGive;

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
            float playerDistanceFromCamera = player.transform.position.x - transform.position.x;
            if (playerDistanceFromCamera > cameraHorizontalGive)
            {
                targetPosition = new Vector3(player.transform.position.x - cameraHorizontalGive, transform.position.y, transform.position.z);
            }
            if (playerDistanceFromCamera < -cameraHorizontalGive)
            {
                targetPosition = new Vector3(player.transform.position.x + cameraHorizontalGive, transform.position.y, transform.position.z);
            }

            
            if (playerDistanceFromCamera != 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
            }
            
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

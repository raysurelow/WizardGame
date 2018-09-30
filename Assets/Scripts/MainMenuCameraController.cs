using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour {

    public float cameraSpeed;
    public float maxHeight;
    public float minHeight;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < maxHeight)
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y + cameraSpeed, transform.position.z);
        }else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > minHeight)
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y - cameraSpeed, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 15 * Time.deltaTime);

    }
}

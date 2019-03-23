using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public Sprite checkpointDisabled;
    public Sprite checkpointEnabled;
    public int checkpointNumber;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = checkpointDisabled;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CheckpointReached();
            //GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void CheckpointReached()
    {
        spriteRenderer.sprite = checkpointEnabled;
        if (CrossSceneInformation.CheckpointReached < checkpointNumber)
        {
            CrossSceneInformation.LoadPosition = transform.position;
            CrossSceneInformation.CheckpointReached = checkpointNumber;
        }
    }
}

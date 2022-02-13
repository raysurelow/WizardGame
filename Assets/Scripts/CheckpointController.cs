using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour {

    public Sprite checkpointDisabled;
    public Sprite checkpointEnabled;
    public int checkpointNumber;
    private string scene;
    private LevelManagerController levelManager;
    private SpriteRenderer spriteRenderer;

	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = checkpointDisabled;
        scene = SceneManager.GetActiveScene().name;
        levelManager = FindObjectOfType<LevelManagerController>();
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
        if (CrossSceneInformation.CheckpointData.ContainsKey(scene))
        {
            if(CrossSceneInformation.CheckpointData[scene].CheckpointReached < checkpointNumber)
            {
                CheckpointMapping sceneCheckpoint = CrossSceneInformation.CheckpointData[scene];
                sceneCheckpoint.CheckpointReached = checkpointNumber;
                sceneCheckpoint.CheckpointLocation = transform.position;
                sceneCheckpoint.ProgressText = checkpointNumber + "/" + sceneCheckpoint.TotalCheckpointsInScene + " Checkpoints Reached";
                levelManager.UpdateProgressText(sceneCheckpoint.ProgressText);
                CrossSceneInformation.CheckpointData[scene] = sceneCheckpoint;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour {

    private LevelManagerController levelManager;
    private GameManagerController gameManager;
    public string levelToLoad;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManagerController>();
        gameManager = FindObjectOfType<GameManagerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            string scene = SceneManager.GetActiveScene().name;
            CrossSceneInformation.CheckpointData[scene].CheckpointReached = 0;
            CrossSceneInformation.CheckpointData[scene].CheckpointLocation = null;
            CrossSceneInformation.CheckpointData[scene].ProgressText = "Complete";
            CrossSceneInformation.Level5EnemyCheckpointHit = false;
            CrossSceneInformation.DialogueTriggered = 0;
            if(CrossSceneInformation.CompletedLevels == null)
            {
                CrossSceneInformation.CompletedLevels = new List<string>();
            }
            CrossSceneInformation.CompletedLevels.Add(SceneManager.GetActiveScene().name);
          //  Debug.Log("calling save game");
          //  gameManager.SaveGame();
            levelManager.LoadLevel(levelToLoad);
        }
        
    }
}

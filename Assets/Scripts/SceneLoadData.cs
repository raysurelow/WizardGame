﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadData : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string scene = SceneManager.GetActiveScene().name;
        if(scene == "Level 3")
        {
            if (CrossSceneInformation.CheckpointReached > 0)
            {
                SetStartingPosition("Box_1", new Vector3(-15.81f, -4.2f));
            }
        }

        if(scene == "Level 4")
        {
            if (CrossSceneInformation.CheckpointReached > 0)
            {
                SetStartingPosition("Box_1", new Vector3(-2.12f, -1.2f));
                GameObject iceWall = GameObject.Find("Ice Wall");
                if(iceWall != null)
                {
                    iceWall.SetActive(false);
                }
            }
        }


        if(scene == "Level 5")
        {
            if(CrossSceneInformation.CheckpointReached > 0)
            {
                if (CrossSceneInformation.Level5EnemyCheckpointHit)
                {
                    SetStartingPosition("Enemy_1", new Vector3(6f, -11f));
                }
                SetStartingPosition("Box_1", new Vector3(-6f, -4f));
            }
        }
        
        
        if(scene == "Level 9")
        {
            if(CrossSceneInformation.CheckpointReached > 0)
            {
                SetStartingPosition("Box_2", new Vector3(0f, -6.2f));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetStartingPosition(string name, Vector3 position)
    {
        GameObject gameObject = GameObject.Find(name);
        if(gameObject != null)
        {
            gameObject.transform.position = position;
        }
        else
        {
            print("unable to find " + name);
        }
    }
}

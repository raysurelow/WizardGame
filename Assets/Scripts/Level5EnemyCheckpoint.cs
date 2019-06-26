using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5EnemyCheckpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Enemy_1")
        {
            CrossSceneInformation.Level5EnemyCheckpointHit = true;
        }
    }
}

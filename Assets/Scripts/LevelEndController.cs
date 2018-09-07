using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour {

    private LevelManagerController levelManager;
    public string levelToLoad;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManagerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            levelManager.LoadLevel(levelToLoad);
        }
        
    }
}

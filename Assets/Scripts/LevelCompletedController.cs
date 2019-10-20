using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedController : MonoBehaviour {

    public string LevelName;
    public Image LevelSelectImage;
    // Use this for initialization
    void Start () {
		if((CrossSceneInformation.CompletedLevels != null) && (!CrossSceneInformation.CompletedLevels.Contains(LevelName)))
        {
            LevelSelectImage.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

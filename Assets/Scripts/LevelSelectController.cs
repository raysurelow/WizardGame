using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour {

    public Image[] levels;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //AssetPreview.GetMiniThumbnail(SceneManager.GetSceneByName(levels[0]));
	}

    public void LoadLevel()
    {
        /* switch (EventSystem.current.currentSelectedGameObject.name)
         {
             case "Level1_Button":
                 SceneManager.LoadScene("Level1");
                 break;
             case "Level2_Button":
                 SceneManager.LoadScene("Level2");
                 break;
             case "Level3_Button":
                 SceneManager.LoadScene("Level3");
                 break;
         } */

        SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
    }
}

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour {

    private GameManagerController gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        gameManager.LoadGame();
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void LoadLevel()
    {
        SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    public GameObject thePauseScreen;
    public bool gamePaused;

	// Use this for initialization
	void Start () {
        gamePaused = false;
        thePauseScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                PauseGame();
            }
        }
	}

    public void PauseGame()
    {
        Time.timeScale = 0f;
        thePauseScreen.SetActive(true);
        gamePaused = true;
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        thePauseScreen.SetActive(false);
        gamePaused = false;
    }

    public void LevelSelect()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

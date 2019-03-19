using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Rewired;

public class PauseMenuController : MonoBehaviour {

    public GameObject thePauseScreen;
    public bool gamePaused;
    private LevelManagerController levelManager;
    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player
    public GameObject resumeButton;
    public GameObject exitButton;
    public Canvas controllerMap;

    // Use this for initialization
    void Start () {
        gamePaused = false;
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        levelManager = FindObjectOfType<LevelManagerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetButtonDown("Pause") && !player.GetButtonDown("Open Spell Chooser")) 
        {
            if (!gamePaused && (Time.timeScale != 0))
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
        es.SetSelectedGameObject(resumeButton);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        thePauseScreen.SetActive(false);
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        gamePaused = false;
    }

    public void LevelSelect()
    {
        ResumeGame();
        levelManager.LoadLevel("MainMenu");
    }

    public void RestartLevelFromCheckpoint()
    {
        Time.timeScale = 1f;
        //Restart level
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenControlMap()
    {
        controllerMap.enabled = true;
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(exitButton);
    }

    public void CloseControlMap()
    {
        controllerMap.enabled = false;
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.SetSelectedGameObject(resumeButton);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Rewired;
using UnityEngine.EventSystems;

public class LevelManagerController : MonoBehaviour {
    public Text activeSpellText;
    private GameObject activeSpellButton;
    public GameObject fireButton;
    public GameObject iceButton;
    public GameObject cloneButton;
    public GameObject gustButton;
    public GameObject spellChooser;
    public static Vector3 PlayerLoadPosition { get; set; }
    public static bool CheckpointReached { get; set; }
    

    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player

    // Use this for initialization
    void Start () {
        spellChooser.SetActive(false);
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }
	
	// Update is called once per frame
	void Update () {
       /** if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        } **/

        if (player.GetButtonDown("Restart Scene"))
        {
            //Restart level
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (Time.timeScale != 0)
        {
            if (player.GetButtonDown("Open Spell Chooser"))
            {
                spellChooser.SetActive(true);
                EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
                es.SetSelectedGameObject(null);
                es.SetSelectedGameObject(activeSpellButton);
                Time.timeScale = .01F;
            }

            if (player.GetButtonUp("Open Spell Chooser"))
            {
                EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
                es.SetSelectedGameObject(null);
                spellChooser.SetActive(false);
                Time.timeScale = 1F;
            }
        }
    }

    public void UpdateActiveSpellText(Spell spell)
    {
        switch (spell.spellName)
        {
            case "Fire":
                activeSpellText.text = "Fire";
                activeSpellText.color = Color.red;
                activeSpellButton = fireButton;
                break;
            case "Ice":
                activeSpellText.text = "Ice";
                activeSpellText.color = Color.blue;
                activeSpellButton = iceButton;
                break;
            case "Gust":
                activeSpellText.text = "Gust";
                activeSpellText.color = Color.white;
                activeSpellButton = gustButton;
                break;
            case "Clone":
                activeSpellText.text = "Clone";
                activeSpellText.color = Color.green;
                activeSpellButton = cloneButton;
                break;
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelToLoad));
    }
}

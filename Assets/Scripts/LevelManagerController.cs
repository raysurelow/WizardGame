using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Rewired;

public class LevelManagerController : MonoBehaviour {
    public Text activeSpellText;
    public Canvas spellChooser;
    public static Vector3 PlayerLoadPosition { get; set; }
    public static bool CheckpointReached { get; set; }

    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player

    // Use this for initialization
    void Start () {
        spellChooser.enabled = false;
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

        if (player.GetButtonDown("Open Spell Chooser"))
        {
            spellChooser.enabled = true;
            Time.timeScale = .01F;
        }

        if (player.GetButtonUp("Open Spell Chooser"))
        {
            spellChooser.enabled = false;
            Time.timeScale = 1F;
        }
    }

    public void UpdateActiveSpellText(Spell spell)
    {
        switch (spell.spellName)
        {
            case "Fire":
                activeSpellText.text = "Fire";
                activeSpellText.color = Color.red;
                break;
            case "Ice":
                activeSpellText.text = "Ice";
                activeSpellText.color = Color.blue;
                break;
            case "Gust":
                activeSpellText.text = "Gust";
                activeSpellText.color = Color.white;
                break;
            case "Clone":
                activeSpellText.text = "Clone";
                activeSpellText.color = Color.green;
                break;
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelToLoad));
    }
}

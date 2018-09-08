using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerController : MonoBehaviour {
    public Text activeSpellText;
    public Canvas spellChooser;

    // Use this for initialization
    void Start () {
        spellChooser.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Restart level
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (Input.GetMouseButtonDown(1))
        {
            spellChooser.enabled = true;
            Time.timeScale = .05F;
        }

        if (Input.GetMouseButtonUp(1))
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerController : MonoBehaviour {
    public Text activeSpellText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //Restart level
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void updateActiveSpellText(Spell spell)
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

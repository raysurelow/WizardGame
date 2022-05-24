using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlePageManager : MonoBehaviour
{

 //   public InputField mainInputField;
    private string userId;
    public Text errorText;
    public Button newGameButton;
 //   public Button loadGameButton;
    private GameManagerController gameManager;

    public void Start()
    {
 //       newGameButton.interactable = false;
 //       loadGameButton.interactable = false;
 //       mainInputField.onValueChanged.AddListener(delegate { SetUserId(mainInputField.text); });
        gameManager = FindObjectOfType<GameManagerController>();
    }

 /*   private void SetUserId(string inputId)
    {
        userId = inputId;
        if (string.IsNullOrEmpty(userId))
        {
            newGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
        else
        {
            newGameButton.interactable = true;
            loadGameButton.interactable = true;
            SetSavePath(userId);
        }

    } 

    public void LoadGame()
    {
        if (!gameManager.SaveFileExists())
        {
            errorText.text = "No save file for this player exists, please enter the player name used when selecting new game.";
            return;
        }
        else
        {
            gameManager.LoadGame();
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void SetSavePath(string userId)
    {
        CrossSceneInformation.SavePath = Application.persistentDataPath + "/" + userId + "WizardingGameSave.save";
    } */

    public void NewGame()
    {
        SceneManager.LoadScene("MainMenu");
        /* if (gameManager.SaveFileExists())
         {
             errorText.text = "Save file with this name already exists, please click load game or change names.";
             return;
         }
         else
         {
             print("saving to: " + CrossSceneInformation.SavePath);
             gameManager.SaveGame();
             gameManager.LoadGame();
             SceneManager.LoadScene("MainMenu");
         }*/
    }
}

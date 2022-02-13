using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    public void Start()
    {

    }

    public void SaveGame()
    {
        if (!string.IsNullOrEmpty(CrossSceneInformation.SavePath)) { 
            Debug.Log("Saving to: " + CrossSceneInformation.SavePath);
            SaveFile save = CreateSaveFileObject();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(CrossSceneInformation.SavePath);
            bf.Serialize(file, save);
            file.Close();
            Debug.Log("Game Saved");
        }
    }

    public void LoadGame()
    {
        Debug.Log("Loading file: " + CrossSceneInformation.SavePath);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(CrossSceneInformation.SavePath, FileMode.Open);
        SaveFile save = (SaveFile)bf.Deserialize(file);
        file.Close();
        CrossSceneInformation.Level5EnemyCheckpointHit = save.Level5EnemyCheckpointHit;
        CrossSceneInformation.DialogueTriggered = save.DialogueTriggered;
        CrossSceneInformation.CompletedLevels = save.CompletedLevels;
        CrossSceneInformation.CheckpointData = save.CheckpointData;
    }

    private SaveFile CreateSaveFileObject()
    {
        SaveFile save = new SaveFile
        {
            Level5EnemyCheckpointHit = CrossSceneInformation.Level5EnemyCheckpointHit,
            DialogueTriggered = CrossSceneInformation.DialogueTriggered,
            CompletedLevels = CrossSceneInformation.CompletedLevels,
            CheckpointData = CrossSceneInformation.CheckpointData
        };

        return save;
    }

    private void SetSavePath(string userId)
    {
       CrossSceneInformation.SavePath = Application.persistentDataPath + "/" + userId + "WizardingGameSave.save";
    }

    public bool SaveFileExists()
    {
        return File.Exists(CrossSceneInformation.SavePath);
    }
}

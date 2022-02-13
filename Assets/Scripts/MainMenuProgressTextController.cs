using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuProgressTextController : MonoBehaviour
{

    public string LevelName;
    public Text ProgressText;
    // Use this for initialization
    void Start()
    {
        if (CrossSceneInformation.CheckpointData.ContainsKey(LevelName))
        {
            string text = CrossSceneInformation.CheckpointData[LevelName].ProgressText;
            ProgressText.text = text;
        }
            
    }

    // Update is called once per frame
    void Update()
    {

    }
}

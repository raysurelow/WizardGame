using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile {
    public bool Level5EnemyCheckpointHit;
    public int DialogueTriggered;
    public List<string> CompletedLevels;
    public Dictionary<string, CheckpointMapping> CheckpointData;
}

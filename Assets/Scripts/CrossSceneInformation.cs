using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneInformation{
    public static bool Level5EnemyCheckpointHit;
    public static int DialogueTriggered;
    public static List<string> CompletedLevels;
    public static string SavePath;

    public static Dictionary<string,CheckpointMapping> CheckpointData = new Dictionary<string, CheckpointMapping>()
    {
        { "Level_1", new CheckpointMapping(3,0,null) },
        { "Level_2", new CheckpointMapping(7,0,null) },
        { "Level_3", new CheckpointMapping(3,0,null) },
        { "Level_4", new CheckpointMapping(3,0,null) },
        { "Level_5", new CheckpointMapping(4,0,null) },
        { "Level_6", new CheckpointMapping(6,0,null) },
        { "Level_7", new CheckpointMapping(4,0,null) },
        { "Level_8", new CheckpointMapping(2,0,null) },
        { "Level_9", new CheckpointMapping(5,0,null) },
        { "Level_10", new CheckpointMapping(3,0,null) }
    };
}

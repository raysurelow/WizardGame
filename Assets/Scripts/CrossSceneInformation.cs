using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneInformation{
    public static Vector3 LoadPosition { get; set; }
    public static int CheckpointReached { get; set; }
    public static bool Level5EnemyCheckpointHit;
    public static int dialogueTriggered;
}

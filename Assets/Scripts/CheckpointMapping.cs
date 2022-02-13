using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class CheckpointMapping
{
    public int TotalCheckpointsInScene { get; set; }
    public int CheckpointReached { get; set; }
    public Vector3S? CheckpointLocation { get; set; }
    public string ProgressText { get; set; }
    public CheckpointMapping(int totalCheckpointsInScene, int checkpointReached, Vector3S? checkpointLocation)
    {
        TotalCheckpointsInScene = totalCheckpointsInScene;
        CheckpointReached = checkpointReached;
        CheckpointLocation = checkpointLocation;
        ProgressText = "0/" + totalCheckpointsInScene + " Checkpoints Reached";
    }
}

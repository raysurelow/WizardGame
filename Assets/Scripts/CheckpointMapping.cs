using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class CheckpointMapping
{
    public int CheckpointReached { get; set; }
    public Vector3S? CheckpointLocation { get; set; }
    public CheckpointMapping(int checkpointReached, Vector3S? checkpointLocation)
    {
        CheckpointReached = checkpointReached;
        CheckpointLocation = checkpointLocation;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CheckpointMapping
{
    public int CheckpointReached { get; set; }
    public Vector3? CheckpointLocation { get; set; }
    public CheckpointMapping(int checkpointReached, Vector3? checkpointLocation)
    {
        CheckpointReached = checkpointReached;
        CheckpointLocation = checkpointLocation;
    }
}

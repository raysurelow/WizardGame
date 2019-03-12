using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneInformation{
    public static Vector3 LoadPosition { get; set; }
    public static List<int> CheckpointsReached = new List<int>();
}

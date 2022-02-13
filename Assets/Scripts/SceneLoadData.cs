using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadData : MonoBehaviour {

 //   private Dictionary<int, Vector3> Level3Box1;
 //   private Dictionary<int, Vector3> Level4Box1;
    private Dictionary<int, Vector3> Level9Box1;
    private Dictionary<bool, Vector3> Level9Enemy1;
    //   private Dictionary<int, Vector3> Level9Box2;
    private string scene;

    // Use this for initialization
    void Start () {
        /*Level4Box1 = new Dictionary<int, Vector3>()
        {
            { 0, new Vector3(0.23f, -1.16f) },
            { 1, new Vector3(0.23f, -1.16f) },
            { 2, new Vector3(0.23f, -1.16f) },
            { 3, new Vector3(0.23f, -1.16f) },
            { 4, new Vector3(-2.12f, -1.2f) }
        };*/

        Level9Box1 = new Dictionary<int, Vector3>()
        {
            { 0, new Vector3(-1.76f,-0.23f) },
            { 1, new Vector3(-1.76f,-0.23f) },
            { 2, new Vector3(-1.76f,-0.23f) },
            { 3, new Vector3(-6f, -4.2f) },
            { 4, new Vector3(3.5f, -14.28f) }
        };

        Level9Enemy1 = new Dictionary<bool, Vector3>()
        {
            { false, new Vector3(2.44f, -3.87f) },
            { true, new Vector3(6f, -11f) },
        };



        scene = SceneManager.GetActiveScene().name;
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        //enable all checkpoints up to the latest checkpoint reached on load
        if (CrossSceneInformation.CheckpointData.ContainsKey(scene))
        {
            foreach (GameObject checkpoint in checkpoints)
            {
                if (checkpoint.GetComponent<CheckpointController>().checkpointNumber <= CrossSceneInformation.CheckpointData[scene].CheckpointReached)
                {
                    checkpoint.GetComponent<CheckpointController>().CheckpointReached();
                }
            }

            var wizard = GameObject.FindGameObjectWithTag("Player");
            if (CrossSceneInformation.CheckpointData[scene].CheckpointReached > 0 && wizard != null)
            {
                if (CrossSceneInformation.CheckpointData[scene].CheckpointLocation != null)
                {
                    wizard.transform.position = (Vector3)CrossSceneInformation.CheckpointData[scene].CheckpointLocation;
                }
            }
        }
        /*if(scene == "Level_4")
        {
            SetStartingPosition(scene, "Box_1");
            if (CrossSceneInformation.CheckpointReached == 4)
            {
                GameObject iceWall = GameObject.Find("Ice Wall");
                if(iceWall != null)
                {
                    iceWall.SetActive(false);
                }
            }
        }


        if(scene == "Level_5")
        {
            SetStartingPosition(scene, "Enemy_1");
            SetStartingPosition(scene, "Box_1");
        }*/


        if (scene == "Level_9")
        {
            SetStartingPosition(scene, "Box_1");
            SetStartingPosition(scene, "Enemy_1");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetStartingPosition(string scene, string name)
    {
        GameObject gameObject = GameObject.Find(name);
        if(gameObject != null)
        {
            Vector3? location = FindCurrentReloadPosition(scene, name);
            if(location  == null)
            {
                print("unable To Find Location!!");
            }
            else
            {
                gameObject.transform.position = (Vector3) location;
            }
        }
        else
        {
            print("unable to find " + name);
        }
    }

    public Vector3? FindCurrentReloadPosition(string scene, string objectName)
    {
        string reloadDictionary = scene + objectName;
        switch (reloadDictionary){
            case ("Level_9Enemy_1"):
                return GetLocationFromDictionary(Level9Enemy1);
            case ("Level_9Box_1"):
                return GetLocationFromDictionary(Level9Box1);
            default:
                return null;
        }
    }

    private Vector3 GetLocationFromDictionary(Dictionary<int, Vector3> dictionary)
    {
        if (CrossSceneInformation.CheckpointData.ContainsKey(scene))
        {
            if (dictionary.ContainsKey(CrossSceneInformation.CheckpointData[scene].CheckpointReached))
            {
                return dictionary[CrossSceneInformation.CheckpointData[scene].CheckpointReached];
            }
        }
        return dictionary[dictionary.Keys.Min()];
    }

    private Vector3 GetLocationFromDictionary(Dictionary<bool, Vector3> dictionary)
    {
        return dictionary[CrossSceneInformation.Level5EnemyCheckpointHit];
    }
}

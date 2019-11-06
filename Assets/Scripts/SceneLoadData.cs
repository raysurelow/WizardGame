using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadData : MonoBehaviour {

    private Dictionary<int, Vector3> Level3Box1;
    private Dictionary<int, Vector3> Level4Box1;
    private Dictionary<int, Vector3> Level5Box1;
    private Dictionary<bool, Vector3> Level5Enemy1;
    private Dictionary<int, Vector3> Level9Box2;

    // Use this for initialization
    void Start () {
        Level4Box1 = new Dictionary<int, Vector3>()
        {
            { 0, new Vector3(0.23f, -1.16f) },
            { 1, new Vector3(0.23f, -1.16f) },
            { 2, new Vector3(0.23f, -1.16f) },
            { 3, new Vector3(0.23f, -1.16f) },
            { 4, new Vector3(-2.12f, -1.2f) }
        };

        Level5Box1 = new Dictionary<int, Vector3>()
        {
            { 0, new Vector3(-1.76f,-0.23f) },
            { 1, new Vector3(-1.76f,-0.23f) },
            { 2, new Vector3(-1.76f,-0.23f) },
            { 3, new Vector3(-6f, -4f) },
            { 4, new Vector3(7.68f, -14.28f) }
        };

        Level5Enemy1 = new Dictionary<bool, Vector3>()
        {
            { false, new Vector3(2.44f, -3.87f) },
            { true, new Vector3(6f, -11f) },
        };

        Level9Box2 = new Dictionary<int, Vector3>()
        {
            { 0, new Vector3(-10.51f, -6.21f) },
            { 1, new Vector3(0f, -6.2f) }
        };

        string scene = SceneManager.GetActiveScene().name;
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        //enable all checkpoints up to the latest checkpoint reached on load
        foreach (GameObject checkpoint in checkpoints)
        {
            if (checkpoint.GetComponent<CheckpointController>().checkpointNumber <= CrossSceneInformation.CheckpointReached)
            {
                checkpoint.GetComponent<CheckpointController>().CheckpointReached();
            }
        }

        if(scene == "Level_4")
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
        }
        
        
        if(scene == "Level_9")
        {
            SetStartingPosition(scene, "Box_2");
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
            case ("Level_3Box_1"):
                return GetLocationFromDictionary(Level3Box1);
            case ("Level_4Box_1"):
                return GetLocationFromDictionary(Level4Box1);
            case ("Level_5Box_1"):
                return GetLocationFromDictionary(Level5Box1);
            case ("Level_5Enemy_1"):
                return GetLocationFromDictionary(Level5Enemy1);
            case ("Level_9Box_2"):
                return GetLocationFromDictionary(Level9Box2);
            default:
                return null;
        }
    }

    private Vector3 GetLocationFromDictionary(Dictionary<int, Vector3> dictionary)
    {
        if (dictionary.ContainsKey(CrossSceneInformation.CheckpointReached))
        {
            return dictionary[CrossSceneInformation.CheckpointReached];
        }
        else
        {
            return dictionary[dictionary.Keys.Max()];
        }
    }

    private Vector3 GetLocationFromDictionary(Dictionary<bool, Vector3> dictionary)
    {
        return dictionary[CrossSceneInformation.Level5EnemyCheckpointHit];
    }
}

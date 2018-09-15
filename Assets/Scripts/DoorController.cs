using UnityEngine;
using System.Collections;

public class DoorController : AbstractSwitchable, IBurnable, IFreezable, IGustable, ICloneable {

    private GameObject door;

    // Use this for initialization
    protected override void Start() {
        if (name == "Door")
        {
            door = gameObject;
        }
        else
        {
            door = transform.Find("Door").gameObject;
        }
	}
	
	// Update is called once per frame
	protected override void Update () {
        door.SetActive(!AllSwitchesAreOn());
    }

    public void Burn()
    {
        Debug.Log("burning door");
    }

    public void Freeze()
    {
        Debug.Log("freezing door");
    }

    public void Gust()
    {
        Debug.Log("gusting door");
    }

    public void Clone()
    {
        Debug.Log("cloning door");
    }
}

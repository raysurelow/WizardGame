using UnityEngine;
using System.Collections;

public class DoorController : AbstractSwitchable, IBurnable, IFreezable, IGustable, ICloneable {

    private GameObject door;
    public bool startHidden = false;

    // Use this for initialization
    protected override void Start() {
        /*if (name == "Door")
        {
            door = gameObject;
        }
        else
        {
            door = transform.Find("Door").gameObject;
        }*/
	}
	
	// Update is called once per frame
	protected override void Update () {
        //  door.SetActive(!AllSwitchesAreOn());
        if (switches.Length > 0)
        {
            if (AllSwitchesAreOn())
            {
                if (startHidden)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(0, 0, 0);
                }
            }
            else
            {
                if (startHidden)
                {
                    transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void Burn()
    {
        Debug.Log("burning door");
    }

    public void Freeze()
    {
        Debug.Log("freezing door");
    }

    public void Gust(Vector2 velocity)
    {
        Debug.Log("gusting door");
    }

    public void Clone()
    {
        Debug.Log("cloning door");
    }
}

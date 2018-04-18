using UnityEngine;
using System.Collections;

public class DoorController : AbstractSwitchable {

    private GameObject door;

	// Use this for initialization
	protected override void Start () {
        door = transform.Find("Door").gameObject;
	}
	
	// Update is called once per frame
	protected override void Update () {
        door.SetActive(!AllSwitchesAreOn());
    }

    
}

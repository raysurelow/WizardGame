using UnityEngine;
using System.Collections;

public class MovingPlatformController : AbstractSwitchable {

    public float speed;
    private Vector3 startingPointPosition;
    private Vector3 endingPointPosition;


	// Use this for initialization
	protected override void Start () {
        Transform startingPoint = transform.Find("StartingPoint");
        startingPointPosition = startingPoint.position;
        Transform endingPoint = transform.Find("EndingPoint");
        endingPointPosition = endingPoint.position;
	}
	
	// Update is called once per frame
	protected override void Update () {
	    if (AllSwitchesAreOn())
        {
            transform.position = Vector3.MoveTowards(transform.position, endingPointPosition, speed * Time.deltaTime);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPointPosition, speed * Time.deltaTime);
        }
	}
}

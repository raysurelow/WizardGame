﻿using UnityEngine;
using System.Collections;

public class MovingPlatformController : AbstractSwitchable {

    public float speed;
    public GameObject startingPoint;
    public GameObject endingPoint;

	// Use this for initialization
	protected override void Start () {
        transform.position = startingPoint.transform.position;
	}
	
	// Update is called once per frame
	protected override void Update () {
	    if (AllSwitchesAreOn())
        {
            transform.position = Vector3.MoveTowards(transform.position, endingPoint.transform.position, speed * Time.deltaTime);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPoint.transform.position, speed * Time.deltaTime);
        }
	}
}

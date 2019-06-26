using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWallController : MonoBehaviour, IBurnable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Burn()
    {
        Destroy(gameObject);
    }
}

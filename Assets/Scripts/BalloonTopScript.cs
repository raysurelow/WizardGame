using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTopScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y < -2)
        {
            BalloonController balloon = GetComponentInParent<BalloonController>();
            balloon.TopTriggerShoot();
        }
    }
}

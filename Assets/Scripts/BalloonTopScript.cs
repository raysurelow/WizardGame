using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTopScript : MonoBehaviour {

    private bool isColliding = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isColliding) return;
        isColliding = true;
        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y < -2)
        {
            print("shooting code hit");
            BalloonController balloon = GetComponentInParent<BalloonController>();
            balloon.TopTriggerShoot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}

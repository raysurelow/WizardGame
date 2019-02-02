using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTopScript : MonoBehaviour {

    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       /** 
        Was trying to resolve an issue where TriggerEnter was being called multiple times when jumping on 
        (I think due to scaling down causing trigger enter to be called again)
        Decided to just control it with a timer instead

        if (isColliding) return;
        isColliding = true;
        **/
        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y < -1 && timer > .8f)
        {
            BalloonController balloon = GetComponentInParent<BalloonController>();
            balloon.TopTriggerShoot();
            timer = 0f;
        }
    }
}

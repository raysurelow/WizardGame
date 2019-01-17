using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonInflationController : MonoBehaviour, IGustable, IFreezable, IBurnable
{
    private BalloonController balloonController;

	// Use this for initialization
	void Start () {
        balloonController = FindObjectOfType<BalloonController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Gust(Vector2 velocity)
    {
        transform.parent.localScale = new Vector3(transform.parent.localScale.x * 1.1f, transform.parent.localScale.y * 1.1f, transform.parent.localScale.z);
    }

    public void Freeze()
    {
        balloonController.Freeze();
    }

    public void Burn()
    {
        balloonController.Burn();
    }
}

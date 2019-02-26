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
        if (!balloonController.IsFrozen())
        { 
            if (transform.parent.localScale.x < balloonController.maxScale)
            {
                transform.parent.localScale = new Vector3(transform.parent.localScale.x * 1.1f, transform.parent.localScale.y * 1.1f, transform.parent.localScale.z);
            }
            else
            {
                transform.parent.localScale = balloonController.GetStartingScale();
            }
        }
    }

    public void Freeze()
    {
        print("balloon inflation freeze called");
        balloonController.Freeze();
    }

    public void Burn()
    {
        balloonController.Burn();
    }
}

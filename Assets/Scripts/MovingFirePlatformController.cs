using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFirePlatformController : MovingPlatformController, IBurnable {

   
    private EdgeCollider2D edgeCollider;

    protected override void Start()
    {
        base.Start();
        freezeStopsMovement = false;
        edgeCollider = GetComponent<EdgeCollider2D>();

    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}

    public override void Burn()
    {
        if (isFrozen)
        {
            edgeCollider.enabled = false;
        }
        base.Burn();
    }

    public override void Freeze()
    {
        base.Freeze();
        edgeCollider.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isFrozen)
        {
            MonoBehaviour go = collision.gameObject.GetComponent<MonoBehaviour>();
            if (go is IBurnable)
            {
                ((IBurnable)go).Burn();
            }
        }
    }

}

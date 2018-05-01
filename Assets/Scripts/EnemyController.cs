using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform groundCheck;
    public LayerMask edgeLayerMask;
    public float speed;
    public float horizontal;
    private bool atEdge;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		atEdge = !Physics2D.OverlapPoint(groundCheck.position, edgeLayerMask, 0);

        if (!atEdge)
        {
            transform.Translate(speed * horizontal * Time.deltaTime, 0f, 0f);
        }
        else
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Flip();
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1f);
        horizontal *= -1;
        transform.Translate(speed * horizontal * Time.deltaTime, 0f, 0f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallController : AbstractSwitchable, IFreezable, IBurnable{

    public float frozenDuration;
    private float frozenElapsedTime;
    private bool isFrozen;
    public bool startHidden = false;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Vector3 startingScale;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        startingScale = transform.localScale;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (isFrozen)
        {
            frozenElapsedTime += Time.deltaTime;
            if (frozenElapsedTime > frozenDuration)
            {
                isFrozen = false;
                frozenElapsedTime = 0;
            }
        }
        animator.SetBool("IsFrozen", isFrozen);
        if (switches.Length > 0)
        {
            if (!isFrozen)
            {
                if (AllSwitchesAreOn())
                {
                    if (startHidden)
                    {
                        transform.localScale = startingScale;
                    }
                    else
                    {
                        transform.localScale = new Vector3(0, 0, 0);
                    }
                }
                else
                {
                    if (startHidden)
                    {
                        transform.localScale = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        transform.localScale = startingScale;
                    }
                }
            }
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        frozenElapsedTime = 0;
        boxCollider.enabled = true;
    }

    
    public void Burn()
    {
        isFrozen = false;
        boxCollider.enabled = false;
        frozenElapsedTime = 0;

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

 /*   private void OnTriggerEnter2D(Collision2D collision)
    {
        if (!isFrozen)
        {
            MonoBehaviour go = collision.gameObject.GetComponent<MonoBehaviour>();
            if (go is IBurnable)
            {
                ((IBurnable)go).Burn();
            }
        }
    }*/

}

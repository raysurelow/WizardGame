using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallController : AbstractSwitchable, IFreezable, IBurnable{

    public float frozenDuration;
    private float frozenElapsedTime;
    private bool isFrozen;
    private Animator animator;
    private BoxCollider2D boxCollider;

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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
            if (AllSwitchesAreOn())
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
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

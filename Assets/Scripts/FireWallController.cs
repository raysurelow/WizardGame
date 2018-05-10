using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallController : MonoBehaviour, IFreezable, IBurnable{

    public float frozenDuration;
    private float frozenElapsedTime;
    private bool isFrozen;
    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();   
    }
	
	// Update is called once per frame
	void Update () {
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
    }

    public void Freeze()
    {
        isFrozen = true;
        frozenElapsedTime = 0;
    }

    
    public void Burn()
    {
        isFrozen = false;
        frozenElapsedTime = 0;

    }
    
    private void OnCollisionStay2D(Collision2D collision)
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

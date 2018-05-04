using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, IFreezable, ICloneable, IBurnable {

    public float frozenDuration = 5.0f;
    private Animator animator;
    private float frozenElapsedTime;
    private bool isFrozen;
    private bool isCloned;

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
        animator.SetBool("IsCloned", isCloned);
    }

    public void Freeze()
    {
        isFrozen = true;
        frozenElapsedTime = 0;
    }

    public void Clone()
    {
        isCloned = true;
    }

    public void Burn()
    {
        isFrozen = false;
        frozenElapsedTime = 0;
    }
}

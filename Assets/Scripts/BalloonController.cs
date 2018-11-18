using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour, IGustable, IFreezable, IBurnable
{
    private Vector3 startingScale;
    private bool isFrozen;
    private float thawingElapsedTime;
    private float frozenElapsedTime;
    public float frozenDuration = 5.0f;
    public float thawingDuration = 5.0f;
    private bool isThawing;
    public float inflateSpeed;
    public float deflateSpeed;

    // Use this for initialization
    void Start () {
        startingScale = new Vector3(transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);


    }
	
	// Update is called once per frame
	void Update () {
        UpdateFrozenEffects();
        if (!isFrozen && (transform.parent.localScale.x > startingScale.x))
        {
            transform.parent.localScale = new Vector3(transform.parent.localScale.x * .999f, transform.parent.localScale.y * .999f, transform.parent.localScale.z);
        }
	}

    public void Gust(Vector2 velocity)
    {
        transform.parent.localScale = new Vector3(transform.parent.localScale.x * 1.1f, transform.parent.localScale.y * 1.1f, transform.parent.localScale.z);
    }

    public void UpdateFrozenEffects()
    {
        if (isFrozen && !isThawing)
        {
            frozenElapsedTime += Time.deltaTime;
            if (frozenElapsedTime > frozenDuration)
            {
                isFrozen = false;
                frozenElapsedTime = 0;
            }
        }
        else if (isFrozen && isThawing)
        {
            thawingElapsedTime += Time.deltaTime;
            if (thawingElapsedTime > thawingDuration)
            {
                isFrozen = false;
                thawingElapsedTime = 0;
            }
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        frozenElapsedTime = 0;
    }

    public void Burn()
    {
        isThawing = true;
        frozenElapsedTime = 0;
    }
}

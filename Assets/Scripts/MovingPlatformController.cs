using UnityEngine;
using System.Collections;

public class MovingPlatformController : AbstractSwitchable , IFreezable, IBurnable {

    public float onSpeed;
    public float offSpeed;
    private Vector3 startingPointPosition;
    private Vector3 endingPointPosition;
    private bool isFrozen;
    private float thawingElapsedTime;
    private float frozenElapsedTime;
    public float frozenDuration = 5.0f;
    public float thawingDuration = 5.0f;
    private bool isThawing;


    // Use this for initialization
    protected override void Start () {
        Transform startingPoint = transform.Find("StartingPoint");
        startingPointPosition = startingPoint.position;
        Transform endingPoint = transform.Find("EndingPoint");
        endingPointPosition = endingPoint.position;
	}

    // Update is called once per frame
    protected override void Update()
    {
        UpdateFrozenEffects();

        if (!isFrozen)
        { 
            if (AllSwitchesAreOn())
            {
                transform.position = Vector3.MoveTowards(transform.position, endingPointPosition, onSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startingPointPosition, offSpeed * Time.deltaTime);
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
}

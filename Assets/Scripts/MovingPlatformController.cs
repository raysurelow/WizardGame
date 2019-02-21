using UnityEngine;
using System.Collections;

public class MovingPlatformController : AbstractSwitchable , IFreezable, IBurnable {

    public float onSpeed;
    public float offSpeed;
    protected Vector3 startingPointPosition;
    protected Vector3 endingPointPosition;
    protected bool isFrozen;
    protected float thawingElapsedTime;
    protected float frozenElapsedTime;
    public float frozenDuration = 5.0f;
    public float thawingDuration = 5.0f;
    protected bool isThawing;
    public bool pacing = false;
    protected bool movingForwards = true;
    protected bool freezeStopsMovement;
    protected Animator animator;



    // Use this for initialization
    protected override void Start () {
        Transform startingPoint = transform.Find("StartingPoint");
        startingPointPosition = startingPoint.position;
        Transform endingPoint = transform.Find("EndingPoint");
        endingPointPosition = endingPoint.position;
        freezeStopsMovement = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        UpdateFrozenEffects();

        animator.SetBool("isFrozen", isFrozen);

        if (!(isFrozen && freezeStopsMovement))
        {
            if (!pacing)
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
            else
            {
                if (AllSwitchesAreOn())
                {
                    Pace();
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, startingPointPosition, offSpeed * Time.deltaTime);
                }
            }
        }
	}

    private void Pace()
    {
        if(movingForwards)
        {
            if(transform.position == endingPointPosition)
            {
                movingForwards = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, endingPointPosition, offSpeed * Time.deltaTime);
            }
        }else
        { 
            if ((transform.position == startingPointPosition))
            {
                movingForwards = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startingPointPosition, offSpeed * Time.deltaTime);
            }
        }
    }

    public virtual void Freeze()
    {
        isFrozen = true;
        frozenElapsedTime = 0;
    }

    public virtual void Burn()
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

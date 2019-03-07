using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IFreezable, IBurnable, ICloneable, IGustable {

    public Transform flipcheck;
    public Transform groundCheck;
    public LayerMask edgeLayerMask;
    public float speed;
    public float horizontal;
    public bool isCloneable = true;
    private bool isFrozen;
    private bool isCloned;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private float frozenElapsedTime;
    private bool isThawing;
    private float thawingElapsedTime;
    public float frozenDuration = 5.0f;
    public float thawingDuration = 2.0f;
    private Vector3 startingPosition;
    private bool hasVelocity;
    private bool inAir;
    private bool atEdge;
    private List<int> layersToIgnore;
    private bool isStuck;



    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        startingPosition = rigidBody.transform.position;
        layersToIgnore = new List<int>();
        layersToIgnore.Add(LayerMask.NameToLayer("Ladder"));
        layersToIgnore.Add(LayerMask.NameToLayer("LevelEnd"));
        layersToIgnore.Add(LayerMask.NameToLayer("Spell"));
        layersToIgnore.Add(LayerMask.NameToLayer("Switch"));
    }
	
	// Update is called once per frame
	void Update () {
		atEdge = !Physics2D.OverlapPoint(flipcheck.position, edgeLayerMask, 0);
        inAir = !Physics2D.OverlapPoint(groundCheck.position, edgeLayerMask, 0);
        hasVelocity = (rigidBody.velocity.x != 0) || (rigidBody.velocity.y != 0);
        if (!isFrozen && !isCloned && !inAir && !hasVelocity)
        {
            if (!atEdge)
            {
                transform.Translate(speed * horizontal * Time.deltaTime, 0f, 0f);
            }
            else
            {
                Flip();
            }
        }else if (isFrozen)
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

        animator.SetBool("IsFrozen", isFrozen);
        animator.SetBool("IsCloned", isCloned);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (!inAir && !hasVelocity)
        {
            if (layersToIgnore.IndexOf(col.gameObject.layer) == -1)
            {
                Flip();
                
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        
        if (!inAir && !hasVelocity)
        {
            if (layersToIgnore.IndexOf(col.gameObject.layer) == -1)
            {
                if (!isStuck)
                {
                    isStuck = true;
                }
                else
                {
                    Flip();
                    isStuck = false;
                }              
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        isStuck = false;
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1f);
        horizontal *= -1;
        transform.Translate(speed * horizontal * 2 * Time.deltaTime, 0f, 0f);
    }

    public void Freeze()
    {
        isFrozen = true;
        frozenElapsedTime = 0;
    }

    public void Burn()
    {
        if (!isFrozen)
        {
            ResetEnemy();
        }
        else
        {
            isThawing = true;
        }
        frozenElapsedTime = 0;
    }

    public void Clone()
    {
        /*    if (isCloneable)
            {
                isCloned = true;
            }
        */
    }

    public void Gust(Vector2 velocity)
    {
        if (velocity.x > 0)
        {
            rigidBody.AddForce(new Vector2(1000, 0), ForceMode2D.Impulse);
        }
        else if (velocity.x < 0)
        {
            rigidBody.AddForce(new Vector2(-1000, 0), ForceMode2D.Impulse);
        }
    }

    public void ResetEnemy()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        transform.position = startingPosition;
        isFrozen = false;
        isCloned = false;
        isCloned = false;
    }

}

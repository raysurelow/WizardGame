using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IFreezable, IBurnable, ICloneable, IGustable {

    public Transform flipcheck;
    public Transform groundCheck;
    public LayerMask edgeLayerMask;
    public float speed;
    public float horizontal;
    private bool isFrozen;
    private bool isCloned;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private float frozenElapsedTime;
    private bool isThawing;
    private float thawingElapsedTime;
    public float frozenDuration = 5.0f;
    public float thawingDuration = 2.0f;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		bool atEdge = !Physics2D.OverlapPoint(flipcheck.position, edgeLayerMask, 0);
        bool inAir = !Physics2D.OverlapPoint(groundCheck.position, edgeLayerMask, 0);
        if (!isFrozen && !isCloned && !inAir)
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
        bool inAir = !Physics2D.OverlapPoint(groundCheck.position, edgeLayerMask, 0);
        if (!inAir)
        {
            if ((col.gameObject.layer != LayerMask.NameToLayer("Spell")) && (col.gameObject.tag != "Switch"))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1f);
        horizontal *= -1;
        transform.Translate(speed * horizontal * Time.deltaTime, 0f, 0f);
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
            Destroy(gameObject);
        }
        else
        {
            isThawing = true;
        }
        frozenElapsedTime = 0;
    }

    public void Clone()
    {
        isCloned = true;
    }

    public void Gust(Vector2 velocity)
    {
        if (velocity.x > 0)
        {
            rigidBody.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
        }
        else if (velocity.x < 0)
        {
            rigidBody.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
        }
    }

}

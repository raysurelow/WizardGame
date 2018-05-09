using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, IFreezable, ICloneable, IBurnable {

    public float frozenDuration = 5.0f;
    
    public bool isCloneable;
    public float moveSpeed;
    public float jumpSpeed;
    public float groundCheckRadius;
    public LayerMask jumpableLayerMask;
    public Transform groundCheck;
    private bool canJump;
    private Rigidbody2D rigidBody;

    private Animator animator;
    private float frozenElapsedTime;
    private bool isFrozen;
    private bool isCloned;



	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
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
        if (isCloned)
        {
            canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask);
            ClonedMovements();
        }
    }

    private void ClonedMovements()
    {
        // Handle movement inputs
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y);
        }

        // Handle jumping input
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed);
        }
    }

    public void Freeze()
    {
        isFrozen = true;
        frozenElapsedTime = 0;
    }

    public void Clone()
    {
        if (isCloneable)
        {
            isCloned = true;
        }
    }

    public void Burn()
    {
        if (!isFrozen)
        {
            Destroy(gameObject);
        }
        isFrozen = false;
        frozenElapsedTime = 0;
        
    }
}

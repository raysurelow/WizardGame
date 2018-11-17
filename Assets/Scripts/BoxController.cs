using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, IFreezable, ICloneable, IBurnable, IGustable {

    public float frozenDuration = 5.0f;
    public float thawingDuration = 5.0f;
    public Transform groundCheck;
    public bool isCloneable;
    private bool canJump;
    private Rigidbody2D rigidBody;

    private Animator animator;
    private float frozenElapsedTime;
    private bool isFrozen;
    private bool isCloned;
    private float massStore;
    private float gravityStore;
    private GameObject player;
    private float jumpSpeed;
    private float moveSpeed;
    private float groundCheckRadius;
    private LayerMask jumpableLayerMask;
    private bool isThawing;
    private float thawingElapsedTime;
    private Vector3 startingPosition;
    


    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        massStore = rigidBody.mass;
        gravityStore = rigidBody.gravityScale;
        startingPosition = rigidBody.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            jumpSpeed = player.GetComponent<WizardController>().jumpSpeed;
            moveSpeed = player.GetComponent<WizardController>().moveSpeed;
            groundCheck = player.GetComponent<WizardController>().groundCheck;
            groundCheckRadius = player.GetComponent<WizardController>().groundCheckRadius;
            jumpableLayerMask = player.GetComponent<WizardController>().jumpableLayerMask;
        }
    }

    // Update is called once per frame
    void Update() {
        if (isFrozen && !isThawing)
        {
            frozenElapsedTime += Time.deltaTime;
            if (frozenElapsedTime > frozenDuration)
            {
                isFrozen = false;
                frozenElapsedTime = 0;
            }
        }else if(isFrozen && isThawing)
        {
            thawingElapsedTime += Time.deltaTime;
            if(thawingElapsedTime > thawingDuration)
            {
                isFrozen = false;
                thawingElapsedTime = 0;
            }
        }
        animator.SetBool("IsFrozen", isFrozen);
        animator.SetBool("IsCloned", isCloned);
        if (isCloned)
        {
            ClonedMovements();
        }

        if (Input.GetKeyDown(KeyCode.Q) || !player.activeSelf)
        {
            if (isCloned)
            {
                rigidBody.mass = massStore;
                rigidBody.gravityScale = gravityStore;
                isCloned = false;
            }
        }
    }

    private void ClonedMovements()
    {
        canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask);

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
            if (player != null)
            {
                rigidBody.mass = player.GetComponent<Rigidbody2D>().mass;
                rigidBody.gravityScale = player.GetComponent<Rigidbody2D>().gravityScale;
            }
        }
    }

    public void Burn()
    {
        if (!isFrozen)
        {
            isCloned = false;
            ResetBox();
        }
        else
        {
            isThawing = true;
        }
        frozenElapsedTime = 0;
    }

    public void Gust(Vector2 velocity)
    {
        if (velocity.x > 0)
        {
            rigidBody.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
        }else if (velocity.x < 0)
        {
            rigidBody.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
        }
    }

    public void ResetBox()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        transform.position = startingPosition;
    }
}

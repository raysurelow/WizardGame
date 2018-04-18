using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

    public float moveSpeed;
    public float jumpSpeed;
    public float groundCheckRadius;
    public LayerMask jumpableLayerMask;
    public Transform groundCheck;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool canJump;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask);

	    if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(1f, 1f);
        } else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-1f, 1f);
        } else
        {
            rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed);
        }

        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
	}
}

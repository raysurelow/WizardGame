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
    private GameObject activeSpell;
    private Transform horizontalSpellTransform;
    public float projectileSpeed;
    private Transform activeSpellTransform;
    private Transform upSpellTransform;
    private Transform downSpellTransform;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        horizontalSpellTransform = transform.Find("HorizontalSpellFirePosition");
        upSpellTransform = transform.Find("UpSpellFirePosition");
        downSpellTransform = transform.Find("DownSpellFirePosition");
    }

    // Update is called once per frame
    void Update()
    {
        canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask);

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-1f, 1f);
        }
        else
        {
            rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed);
        }

        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

        UpdateActiveTransform();

        // Fire spell
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
        {
            FireSpell();
        }



    }

    private void UpdateActiveTransform()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            activeSpellTransform = upSpellTransform;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            activeSpellTransform = downSpellTransform;
        }
        else
        {
            activeSpellTransform = horizontalSpellTransform;
        }
    }

    public void FireSpell()
    {
        GameObject spell = null;
        spell = Instantiate(activeSpell, activeSpellTransform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        Rigidbody2D spellRigidBody = spell.GetComponent<Rigidbody2D>();
        spellRigidBody.velocity = new Vector2(projectileSpeed, 0);
    }
}
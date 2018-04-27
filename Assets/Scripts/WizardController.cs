using UnityEngine;
using System.Collections;

public class WizardController : MonoBehaviour {

    public float moveSpeed;
    public float jumpSpeed;
    public float groundCheckRadius;
    public LayerMask jumpableLayerMask;
    public Transform groundCheck;
    public Spell[] availableSpells;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool canJump;
    private Spell activeSpell;
    private Transform horizontalSpellTransform;
    private Transform upSpellTransform;
    private Transform downSpellTransform;
    public float projectileSpeed;
    private Transform activeSpellTransform;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        horizontalSpellTransform = transform.Find("HorizontalSpellFirePosition");
        upSpellTransform = transform.Find("UpSpellFirePosition");
        downSpellTransform = transform.Find("DownSpellFirePosition");
        activeSpell = availableSpells.Length > 0 ? availableSpells[0] : null;
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
        Rigidbody2D spell = null;
        spell = Instantiate(activeSpell.spellRigidBody, activeSpellTransform.position, activeSpellTransform.rotation) as Rigidbody2D;
        spell.velocity = activeSpellTransform.transform.right * projectileSpeed;
    }
}
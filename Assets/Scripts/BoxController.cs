using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class BoxController : MonoBehaviour, IFreezable, ICloneable, IBurnable, IGustable {

    public float frozenDuration = 5.0f;
    public float thawingDuration = 5.0f;
    public Transform groundCheck;
    public Transform wizardGroundCheck;
    public bool isCloneable;
    public bool regenerates;
    private bool canJump;
    private bool wizardCanJump;
    private bool boxCanJump;
    private Rigidbody2D rigidBody;

    private Animator animator;
    private float frozenElapsedTime;
    private bool isFrozen;
    private bool isCloned;
    private bool isThawing;
    private float thawingElapsedTime;
    private Vector3 startingPosition;
    private EdgeCollider2D boxEdgeCollider;

    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player

    //inputs from Wizard when cloned
    private GameObject wizard;
    private float jumpSpeed;
    private float moveSpeed;
    private float groundCheckRadius;
    private LayerMask jumpableLayerMask;
    private PhysicsMaterial2D wizardMaterial;

    //initial parameters for box when not cloned
    private float massStore;
    private float gravityStore;
    private float dragStore;
    private PhysicsMaterial2D boxMaterial;

    private bool burning;
    private bool wizardOnTop;
    private bool timeScaleWas0;
    private SceneLoadData sceneLoadData;

   


    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("Cloneable", isCloneable);
        rigidBody = GetComponent<Rigidbody2D>();
        boxEdgeCollider = GetComponent<EdgeCollider2D>();
        sceneLoadData = FindObjectOfType<SceneLoadData>();
        startingPosition = rigidBody.transform.position;
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        //get Wizard physics for cloned box 
        wizard = GameObject.FindGameObjectWithTag("Player");
        if (wizard != null)
        {
            jumpSpeed = wizard.GetComponent<WizardController>().jumpSpeed;
            moveSpeed = wizard.GetComponent<WizardController>().moveSpeed;
            wizardGroundCheck = wizard.GetComponent<WizardController>().groundCheck;
            groundCheckRadius = wizard.GetComponent<WizardController>().groundCheckRadius;
            jumpableLayerMask = wizard.GetComponent<WizardController>().jumpableLayerMask;
            wizardMaterial = wizard.GetComponent<BoxCollider2D>().sharedMaterial;

        }
        //store box physics for resetting after clone
        massStore = rigidBody.mass;
        gravityStore = rigidBody.gravityScale;
        dragStore = rigidBody.drag;
        boxMaterial = boxEdgeCollider.sharedMaterial;
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
                isThawing = false;
            }
        }
        animator.SetBool("IsFrozen", isFrozen);
        animator.SetBool("IsCloned", isCloned);
        if (isCloned)
        {
            ClonedMovements();
        }

        if (player.GetButtonDown("Quit Clone") || !wizard.activeSelf)
        {
            if (isCloned)
            {
                rigidBody.mass = massStore;
                rigidBody.gravityScale = gravityStore;
                rigidBody.drag = dragStore;
                isCloned = false;
                boxEdgeCollider.sharedMaterial = boxMaterial;
            }
        }
    }

    private void ClonedMovements()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        Vector3 wizardVelocity = wizard.GetComponent<Rigidbody2D>().velocity;
        Vector3 targetVelocity = new Vector3();
        wizardCanJump = Physics2D.OverlapCircle(wizardGroundCheck.position, groundCheckRadius, jumpableLayerMask);
        boxCanJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask);
        if (wizardOnTop)
        {
            if (wizardCanJump && boxCanJump)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
        }
        else
        {
            canJump = wizardCanJump;
        }

        float horizontalMovement = player.GetAxis("Move Horizontal");
        float horizontalMovementRaw = player.GetAxisRaw("Move Horizontal");

        //want the y velocity from the wizard for jumping but want the box to fall via gravity like normal 
        //when wizard is still on platform and box isn't

        if (!wizard.GetComponent<WizardController>().gusted)
        {
            if (player.GetButtonDown("Jump") && canJump)
            {
                //don't shoot spell when coming out of pause or paused
                if (Time.timeScale > 0 && timeScaleWas0)
                {
                    timeScaleWas0 = false;
                }
                else
                {
                    targetVelocity.y = jumpSpeed;
                }
                
            }
            else
            {
                targetVelocity.y = rigidBody.velocity.y;
            }
        }

        //handle situation where wizard is walking against a wall but box should walk free
        if(wizardVelocity.x == 0 && horizontalMovementRaw != 0)
        {
            if (horizontalMovementRaw == 1 || horizontalMovement > .6)
            {
                targetVelocity.x = moveSpeed;
                transform.localScale = new Vector3(1, transform.localScale.y);
            }
            else if (horizontalMovementRaw == -1 || horizontalMovement < -.6)
            {
                targetVelocity.x = -moveSpeed;
                transform.localScale = new Vector3(-1, transform.localScale.y);
            }
        }
        else
        {
            if (wizard.GetComponent<WizardController>().gusted)
            {
                targetVelocity.x = 0f;
            }
            else
            {
                targetVelocity.x = wizardVelocity.x;
            }
        }

        rigidBody.velocity = targetVelocity;
        

        /*
        canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask);
        // Handle movement inputs
        if (player.GetAxisRaw("Move Horizontal") > 0f)
        {
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (player.GetAxisRaw("Move Horizontal") < 0f)
        {
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y);
        }

        // Handle jumping input
        
        */
    }

    public void Freeze()
    {
        if (!burning)
        {
            isFrozen = true;
            frozenElapsedTime = 0;
            boxEdgeCollider.sharedMaterial = wizardMaterial;
        }
    }

    public void Clone()
    {
        if (isCloneable && !isFrozen && !burning)
        {
            isCloned = true;
            if (player != null)
            {
                rigidBody.mass = wizard.GetComponent<Rigidbody2D>().mass;
                rigidBody.gravityScale = wizard.GetComponent<Rigidbody2D>().gravityScale;
                rigidBody.drag = wizard.GetComponent<Rigidbody2D>().drag;
                boxEdgeCollider.sharedMaterial = wizardMaterial;
            }
        }
    }

    public void Burn()
    {
        frozenElapsedTime = 0;
        boxEdgeCollider.sharedMaterial = boxMaterial;

        if (!isFrozen && !burning)
        {
            burning = true;
            isCloned = false;
            animator.SetTrigger("Burn");
            //ResetBox();
        }
        else
        {
            isThawing = true;
        }
        
    }

    public void Gust(Vector2 velocity)
    {
        if (velocity.x > 0)
        {
            rigidBody.AddForce(new Vector2(1000, 0), ForceMode2D.Impulse);
        }else if (velocity.x < 0)
        {
            rigidBody.AddForce(new Vector2(-1000, 0), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "KillPlane")
        {
            ResetBox();
        }

        if(col.gameObject.tag == "Player")
        {
            wizardOnTop = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            wizardOnTop = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.gameObject.transform;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.gameObject.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    public void ResetBox()
    {
        if (regenerates)
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            isCloned = false;
            isFrozen = false;
            burning = false;
            isThawing = false;
            boxEdgeCollider.sharedMaterial = boxMaterial;
            Vector3? position = sceneLoadData.FindCurrentReloadPosition(SceneManager.GetActiveScene().name, gameObject.name);
            if (position == null)
            {
                transform.position = startingPosition;
            }
            else
            {
                transform.position = (Vector3)position;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

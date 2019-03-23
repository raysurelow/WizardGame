using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Rewired;

public class WizardController : MonoBehaviour, IBurnable, IFreezable, ICloneable, IGustable {

    public float moveSpeed;
    public float jumpSpeed;
    public LayerMask jumpableLayerMask;
    public float groundCheckRadius;
    public float projectileSpeed;
    public Transform groundCheck;
    public Transform groundCheck2;
    public Spell[] availableSpells;
    public bool onLadder;
    public bool climbInitiated;
    public float climbSpeed;
    public float climbJumpSpeed;
    public float gravityStore;
    public bool isCloneable;
    public float frozenDuration = 5.0f;
    public float thawingDuration = 2.0f;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool canJump;
    private int activeSpellPosition;
    private Spell activeSpell;
    private Transform horizontalSpellTransform;
    private Transform upSpellTransform;
    private Transform downSpellTransform;
    private Transform activeSpellTransform;
    private LadderController ladder;
    private bool isFrozen;
    private bool isCloned;
    private float frozenElapsedTime;
    private bool isThawing;
    private float thawingElapsedTime;
    private LevelManagerController levelManager;
    private PauseMenuController pauseMenu;
    private Vector3 horizontalMovement;
    private Vector3 horizontalMovementRaw;
    public bool gusted;
    private bool burning;
    private bool timeScaleWas0;

    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player


    // Use this for initialization
    void Start () {
        if(CrossSceneInformation.CheckpointReached == 0)
        {
            CrossSceneInformation.LoadPosition = transform.position;
        }

        transform.position = CrossSceneInformation.LoadPosition;
        burning = false;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        horizontalSpellTransform = transform.Find("HorizontalSpellFirePosition");
        upSpellTransform = transform.Find("UpSpellFirePosition");
        downSpellTransform = transform.Find("DownSpellFirePosition");
        activeSpellPosition = 0;
        activeSpell = availableSpells.Length > 0 ? availableSpells[activeSpellPosition] : null;
        gravityStore = rigidBody.gravityScale;
        levelManager = FindObjectOfType<LevelManagerController>();
        pauseMenu = FindObjectOfType<PauseMenuController>();
        activeSpell = availableSpells[activeSpellPosition];
        levelManager.UpdateActiveSpellText(activeSpell);
        SendAnimatorActiveSpell(activeSpell.spellName);
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    // Update is called once per frame
    void Update()
    {
        //don't call input for wizard when game is paused
        if (Time.timeScale == 0)
        {
            timeScaleWas0 = true;
            return;
        }

        if (!pauseMenu.gamePaused && !isFrozen && !burning)
        {
            // Check if grounded
            canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask) || Physics2D.OverlapCircle(groundCheck2.position, groundCheckRadius, jumpableLayerMask);
            horizontalMovement.x = player.GetAxis("Move Horizontal");
            horizontalMovementRaw.x = player.GetAxisRaw("Move Horizontal");

            // Handle non-ladder movement inputs
            if (!climbInitiated)
            {
                if (horizontalMovementRaw.x == 1 || horizontalMovement.x > .6)
                {
                    gusted = false;
                    rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y);
                    transform.localScale = new Vector3(1, transform.localScale.y);
                }
                else if (horizontalMovementRaw.x == -1 || horizontalMovement.x < -.6)
                {
                    gusted = false;
                    rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y);
                    transform.localScale = new Vector3(-1, transform.localScale.y);
                }
                else if (gusted)
                {
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                }
                else
                {
                    rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y);
                }

                // Handle jumping input
                if (player.GetButtonDown("Jump") && canJump)
                {
                    //don't shoot spell when coming out of pause or paused
                    if (Time.timeScale > 0 && timeScaleWas0)
                    {
                        timeScaleWas0 = false;
                        return;
                    }
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed);
                }
            }

            //Handle onLadder climbing
            if (onLadder)
            {
                if (!climbInitiated)
                {
                    //dont want to cancel gravity until climbing is initiated by hitting up or down
                    if (Mathf.Abs(player.GetAxisRaw("Climb Ladder")) == 1)
                    {
                        climbInitiated = true;
                    }
                }

                if (climbInitiated)
                {
                    rigidBody.gravityScale = 0;
                    rigidBody.velocity = new Vector3(moveSpeed * horizontalMovement.x, climbSpeed * player.GetAxisRaw("Climb Ladder"));
                    if (horizontalMovement.x > 0.5f)
                    {
                        transform.localScale = new Vector3(1, transform.localScale.y);
                    }
                    else if (horizontalMovement.x < -0.5f)
                    {
                        transform.localScale = new Vector3(-1, transform.localScale.y);
                    }
                }

                //Jumping cancels climbing so gravity is restored 
                if (player.GetButtonDown("Jump") && climbInitiated)
                {
                    climbInitiated = false;
                    rigidBody.gravityScale = gravityStore;
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, climbJumpSpeed);
                }

                animator.SetFloat("ClimbingSpeed", Mathf.Abs(rigidBody.velocity.y));

            }
            else
            {
                climbInitiated = false;
                rigidBody.gravityScale = gravityStore;
            }
            animator.SetBool("IsClimbing", climbInitiated);


            // Handle change spell input
            if (player.GetButtonDown("Rotate Spell"))
            {

                activeSpellPosition = (activeSpellPosition + 1) % availableSpells.Length;
                activeSpell = availableSpells[activeSpellPosition];
                levelManager.UpdateActiveSpellText(activeSpell);
                SendAnimatorActiveSpell(activeSpell.spellName);
            }


            // Handle aiming input
            UpdateActiveTransform();

            // Handle shooting spell input
            if (player.GetButtonDown("Fire"))
            {
                //don't shoot spell when coming out of pause or paused
                if (Time.timeScale > 0 && timeScaleWas0)
                { 
                    timeScaleWas0 = false;
                    return;
                }

                if (!(climbInitiated && (Mathf.Abs(rigidBody.velocity.y) > .3)))
                {
                    animator.SetTrigger(string.Format("Shoot{0}Spell", activeSpell.spellName));
                }      
            }

            animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        }else if (isFrozen)
        {
            if (isFrozen && !isThawing)
            {
                frozenElapsedTime += Time.deltaTime;
                if (frozenElapsedTime > frozenDuration)
                {
                    IsFrozen(false);
                    frozenElapsedTime = 0;
                }
            }
            else if (isFrozen && isThawing)
            {
                thawingElapsedTime += Time.deltaTime;
                if (thawingElapsedTime > thawingDuration)
                {
                    IsFrozen(false);
                    thawingElapsedTime = 0;
                    isThawing = false;
                }
            }
        }
    }

    private void UpdateActiveTransform()
    {
        if (Input.GetAxis("Vertical") > 0.8f)
        {
            activeSpellTransform = upSpellTransform;
            animator.SetInteger("VerticalPosition", 1);

        }
        else if (Input.GetAxis("Vertical") < -0.8f)
        {
            activeSpellTransform = downSpellTransform;
            animator.SetInteger("VerticalPosition", -1);
        }
        else
        {
            activeSpellTransform = horizontalSpellTransform;
            animator.SetInteger("VerticalPosition", 0);
        }
    }

    private void ShootSpell()
    {
        if(climbInitiated && (Mathf.Abs(rigidBody.velocity.y) > .3))
        {
            return;
        }

        Rigidbody2D spell = null;
        spell = Instantiate(activeSpell.spellRigidBody, activeSpellTransform.position, activeSpellTransform.rotation) as Rigidbody2D;
        spell.GetComponent<SpellController>().Spell = activeSpell;
        Vector3 spellVelocity = activeSpellTransform.right * projectileSpeed;
        spell.velocity = transform.localScale.x > 0 ? spellVelocity : -spellVelocity;
        spell.transform.localScale = transform.localScale;
    }

    public void Freeze()
    {
        IsFrozen(true);
        frozenElapsedTime = 0;
    }

    public void Clone()
    {
        if (isCloneable)
        {
            isCloned = true;
        }
    }

    public void Burn(){
        if (!isFrozen && !isThawing && !burning)
        {
            burning = true;
            animator.SetTrigger("Burn");
            //Destroy(gameObject);
        }
        else
        {
            isThawing = true;
        }
        frozenElapsedTime = 0;

    }

    public void Gust(Vector2 velocity)
    {
        if (climbInitiated)
        {
            climbInitiated = false;
            rigidBody.gravityScale = gravityStore;
        }

        if (velocity.x > 0)
        {
            rigidBody.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
            gusted = true;
        }
        else if (velocity.x < 0)
        {
            rigidBody.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
            gusted = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "KillPlane")
        {
            //gameObject.SetActive(false);
            LevelReset();            
        }

        if(col.gameObject.tag == "Checkpoint")
        {
            col.gameObject.GetComponent<CheckpointController>().CheckpointReached();
        }

        if(col.gameObject.tag == "Enemy")
        {
            if (!col.gameObject.GetComponent<EnemyController>().IsFrozen())
            {
                LevelReset();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (!col.gameObject.GetComponent<EnemyController>().IsFrozen())
            {
                LevelReset();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
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

    public void UpdateActiveSpell(string spellName)
    {
        switch (spellName)
        {
            case "Fire":
                activeSpell = availableSpells[0];
                break;
            case "Ice":
                activeSpell = availableSpells[1];
                break;
            case "Gust":
                activeSpell = availableSpells[2];
                break;
            case "Clone":
                activeSpell = availableSpells[3];
                break;
        }
        SendAnimatorActiveSpell(spellName);
        levelManager.UpdateActiveSpellText(activeSpell);
    }

    public void SendAnimatorActiveSpell(string spellName)
    {
        switch (spellName)
        {
            case "Fire":
                animator.SetBool("IceActive", false);
                animator.SetBool("GustActive", false);
                animator.SetBool("CloneActive", false);
                animator.SetBool("FireActive", true);
                break;
            case "Ice":
                animator.SetBool("FireActive", false);
                animator.SetBool("GustActive", false);
                animator.SetBool("CloneActive", false);
                animator.SetBool("IceActive", true);
                break;
            case "Gust":
                animator.SetBool("FireActive", false);
                animator.SetBool("IceActive", false);
                animator.SetBool("CloneActive", false);
                animator.SetBool("GustActive", true);
                break;
            case "Clone":
                animator.SetBool("FireActive", false);
                animator.SetBool("IceActive", false);
                animator.SetBool("GustActive", false);
                animator.SetBool("CloneActive", true);
                break;
        }
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Destroy(gameObject);
    }

    private void IsFrozen(bool frozen)
    {
        if (frozen)
        {
            isFrozen = true;
            animator.SetBool("IsFrozen", true);            
        }
        else
        {
            isFrozen = false;
            animator.SetBool("IsFrozen", false);
        }
    }
}
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

    //rewired parametres
    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player


    // Use this for initialization
    void Start () {
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
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.gamePaused && !isFrozen)
        {
            // Check if grounded
            canJump = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpableLayerMask) || Physics2D.OverlapCircle(groundCheck2.position, groundCheckRadius, jumpableLayerMask);
            horizontalMovement.x = player.GetAxis("Move Horizontal");

            // Handle movement inputs
            if (horizontalMovement.x > 0f) 
            {
                rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y);
                transform.localScale = new Vector3(1, transform.localScale.y);
            }
            else if (horizontalMovement.x < 0f)
            {
                rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y);
                transform.localScale = new Vector3(-1, transform.localScale.y);
            }
            else
            {
                rigidBody.velocity = new Vector3(0f, rigidBody.velocity.y);
            }

            // Handle jumping input
            if (player.GetButtonDown("Jump") && canJump && !climbInitiated)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpSpeed);
            }


            // Handle change spell input
            if (player.GetButtonDown("Rotate Spell"))
            {
                activeSpellPosition = (activeSpellPosition + 1) % availableSpells.Length;
                activeSpell = availableSpells[activeSpellPosition];
                levelManager.UpdateActiveSpellText(activeSpell);
            }


            // Handle aiming input
            UpdateActiveTransform();

            // Handle shooting spell input
            if (player.GetButtonDown("Fire"))
            {
                animator.SetTrigger(string.Format("Shoot{0}Spell", activeSpell.spellName));
            }

            animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

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
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, climbSpeed * player.GetAxisRaw("Climb Ladder"));
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
    }

    private void UpdateActiveTransform()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            activeSpellTransform = upSpellTransform;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            activeSpellTransform = downSpellTransform;
        }
        else
        {
            activeSpellTransform = horizontalSpellTransform;
        }
    }

    private void ShootSpell()
    {
        Rigidbody2D spell = null;
        spell = Instantiate(activeSpell.spellRigidBody, activeSpellTransform.position, activeSpellTransform.rotation) as Rigidbody2D;
        spell.GetComponent<SpellController>().Spell = activeSpell;
        Vector3 spellVelocity = activeSpellTransform.right * projectileSpeed;
        spell.velocity = transform.localScale.x > 0 ? spellVelocity : -spellVelocity;
        spell.transform.localScale = transform.localScale;
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
            //Destroy(gameObject);
            LevelReset();
        }
        else
        {
            isThawing = true;
        }
        frozenElapsedTime = 0;

    }

    public void Gust(Vector2 velocity)
    {
        //todo
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "KillPlane")
        {
            //gameObject.SetActive(false);
            LevelReset();            
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
        levelManager.UpdateActiveSpellText(activeSpell);
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Destroy(gameObject);
    }

}
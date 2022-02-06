using UnityEngine;
using System.Collections;

public class DoorController : AbstractSwitchable, IBurnable, IFreezable, IGustable, ICloneable {

    private GameObject door;
    public bool startHidden = false;
    private bool isFrozen = false;
    private Animator animator;
    private Vector3 startingScale;

    // Use this for initialization
    protected override void Start() {
        animator = GetComponent<Animator>();
        startingScale = transform.localScale;
        /*if (name == "Door")
        {
            door = gameObject;
        }
        else
        {
            door = transform.Find("Door").gameObject;
        }*/
    }
	
	// Update is called once per frame
	protected override void Update () {
        //  door.SetActive(!AllSwitchesAreOn());
        animator.SetBool("IsFrozen", isFrozen);
        if (!isFrozen && switches.Length > 0)
        {
            if (AllSwitchesAreOn())
            {
                if (startHidden)
                {
                    transform.localScale = startingScale;
                }
                else
                {
                    transform.localScale = new Vector3(0, 0, 0);
                }
            }
            else
            {
                if (startHidden)
                {
                    transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.localScale = startingScale;
                }
            }
        }
    }

    public void Burn()
    {
        if (isFrozen)
        {
            isFrozen = false;
        }
    }

    public void Freeze()
    {
        isFrozen = true;
    }

    public void Gust(Vector2 velocity)
    {
    }

    public void Clone()
    {
    }
}

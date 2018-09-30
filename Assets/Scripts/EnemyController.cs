using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IFreezable, IBurnable, ICloneable {

    public Transform groundCheck;
    public LayerMask edgeLayerMask;
    public float speed;
    public float horizontal;
    private bool isFrozen;
    private bool isCloned;
    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		bool atEdge = !Physics2D.OverlapPoint(groundCheck.position, edgeLayerMask, 0);
        if (!isFrozen && !isCloned)
        {
            if (!atEdge)
            {
                transform.Translate(speed * horizontal * Time.deltaTime, 0f, 0f);
            }
            else
            {
                Flip();
            }
        }

        animator.SetBool("IsFrozen", isFrozen);
        animator.SetBool("IsCloned", isCloned);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.layer != LayerMask.NameToLayer("Spell")) && (col.gameObject.tag != "Switch"))
        {
            print(col.gameObject.name);
            print(col.gameObject.tag);
            Flip();
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
    }

    public void Burn()
    {
        isFrozen = false;
    }

    public void Clone()
    {
        isCloned = true;
    }

}

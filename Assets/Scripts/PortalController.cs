using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour {

    public GameObject outputPortal;
    private Transform outputTransform;

	// Use this for initialization
	void Start () {
        outputTransform = transform.Find("OutputPosition");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.GetComponent<Collider2D>().gameObject;
        if (colliderObject.layer == LayerMask.NameToLayer("Spell"))
        {
            Destroy(colliderObject);
            if (outputPortal)
            {
                Rigidbody2D originalSpell = colliderObject.GetComponent<Rigidbody2D>();
                PortalController outputPortalController = outputPortal.GetComponent<PortalController>();
                if (outputPortalController)
                {
                    outputPortalController.ShootSpell(originalSpell);
                }
            }
            
        }
    }

    public void ShootSpell(Rigidbody2D spell)
    {
        Rigidbody2D clonedSpell = Instantiate(spell, outputTransform.position, outputTransform.rotation) as Rigidbody2D;
        clonedSpell.velocity = spell.velocity.magnitude * outputTransform.right;
    }
}

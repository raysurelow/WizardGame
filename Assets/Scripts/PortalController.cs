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
        GameObject colliderObject = collision.gameObject;
        if (colliderObject.layer == LayerMask.NameToLayer("Spell"))
        {
            if (outputPortal)
            {
                PortalController outputPortalController = outputPortal.GetComponent<PortalController>();
                if (outputPortalController)
                {
                    outputPortalController.ShootSpell(colliderObject);
                }
                Destroy(colliderObject);
            }
            
        }
    }

    public void ShootSpell(GameObject spell)
    {
        GameObject clonedSpell = Instantiate(spell, outputTransform.position, outputTransform.rotation) as GameObject;
        clonedSpell.GetComponent<SpellController>().Spell = spell.GetComponent<SpellController>().Spell;
        clonedSpell.GetComponent<Rigidbody2D>().velocity = spell.GetComponent<Rigidbody2D>().velocity.magnitude * outputTransform.right;
    }
}

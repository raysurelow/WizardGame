using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PortalController : MonoBehaviour {

    public GameObject outputPortal;
    private Transform outputTransform;
    private LevelManagerController levelManager;
    public GameObject fireSpell;
    public GameObject iceSpell;
    public GameObject cloneSpell;
    public GameObject gustSpell;

	// Use this for initialization
	void Start () {
        outputTransform = transform.Find("OutputPosition");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void sendSpellToExitPortal(Collider2D collision)
    {

        GameObject colliderObject = collision.gameObject;
        if (colliderObject.layer == LayerMask.NameToLayer("Spell"))
        {
            if (outputPortal)
            {
                PortalController outputPortalController = outputPortal.GetComponent<PortalController>();
                if (outputPortalController)
                {
                    //need to create a new gameobject of the Prefab otherwise it will be treated as pointer to the original causing problems on the destroy
                    Spell spell = colliderObject.GetComponent<SpellController>().Spell;
                    GameObject exitSpell = null;
                    switch (spell.spellName)
                    {
                        case "Fire":
                            exitSpell = fireSpell;
                            break;
                        case "Ice":
                            exitSpell = iceSpell;
                            break;
                        case "Gust":
                            exitSpell = gustSpell;
                            break;
                        case "Clone":
                            exitSpell = cloneSpell;
                            break;
                    }
                    if (exitSpell != null)
                    {
                        exitSpell.GetComponent<SpellController>().Spell = spell;
                        outputPortalController.ShootSpell(exitSpell, colliderObject.GetComponent<Rigidbody2D>().velocity);
                    }
                }
                Destroy(colliderObject);
            }
        }
    }

    public void ShootSpell(GameObject spell, Vector2 velocity)
    { 
        GameObject clonedSpell = Instantiate(spell, outputTransform.position, outputTransform.rotation) as GameObject;
        clonedSpell.GetComponent<SpellController>().Spell = spell.GetComponent<SpellController>().Spell;
        clonedSpell.GetComponent<Rigidbody2D>().velocity = velocity.magnitude * outputTransform.right;
    }
}

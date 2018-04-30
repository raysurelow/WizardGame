using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    private string spellName;
    private LayerMask impactedLayers;

	// Use this for initialization
	void Start () {

	}

    public void Initialize(Spell spell)
    {
        spellName = spell.spellName;
        impactedLayers = spell.impactedLayers;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(spellName + " hit something");
    }

}

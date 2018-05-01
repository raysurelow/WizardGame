using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    public Spell Spell { get; set; }

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonoBehaviour go = collision.gameObject.GetComponent<MonoBehaviour>();
        switch (Spell.spellName)
        {
            case "Fire":
                if (go is IBurnable)
                {
                    ((IBurnable)go).Burn();
                }
                break;
            case "Ice":
                if (go is IFreezable)
                {
                    ((IFreezable)go).Freeze();
                }
                break;
            case "Gust":
                if (go is IGustable)
                {
                    ((IGustable)go).Gust();
                }
                break;
            case "Clone":
                if (go is ICloneable)
                {
                    ((ICloneable)go).Clone();
                }
                break;
        }

        if (collision.gameObject.layer != LayerMask.NameToLayer("Portal"))
        {
            Destroy(gameObject);
        }

    }

}

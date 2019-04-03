using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    public Spell Spell { get; set; }
    private List<int> layersToIgnore;


    // Use this for initialization
    void Start () {
        layersToIgnore = new List<int>();
        layersToIgnore.Add(LayerMask.NameToLayer("PortalFront"));
        layersToIgnore.Add(LayerMask.NameToLayer("Ladder"));
        layersToIgnore.Add(LayerMask.NameToLayer("LadderTop"));
        layersToIgnore.Add(LayerMask.NameToLayer("LevelEnd"));
        layersToIgnore.Add(LayerMask.NameToLayer("DialogueTrigger"));
        layersToIgnore.Add(LayerMask.NameToLayer("EnemyTrigger"));
        layersToIgnore.Add(LayerMask.NameToLayer("EnemyEndpoint"));
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
                    ((IGustable)go).Gust(GetComponent<Rigidbody2D>().velocity);
                }
                break;
            case "Clone":
                if (go is ICloneable)
                {
                    ((ICloneable)go).Clone();
                }
                break;
        }
        
        if (layersToIgnore.IndexOf(collision.gameObject.layer) == -1)
        {
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellChooserController : MonoBehaviour {
    public GameObject player;
    private WizardController wizard;


    // Use this for initialization
    void Start () {
        wizard = FindObjectOfType<WizardController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (wizard != null)
        {
            transform.localPosition = new Vector3(wizard.transform.position.x, wizard.transform.position.y, 0f);
        }
    }
}

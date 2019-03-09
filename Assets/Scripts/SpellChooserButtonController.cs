using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpellChooserButtonController : MonoBehaviour{
    private WizardController wizard;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void UpdateActiveSpell()
    {
        FindObjectOfType<WizardController>().UpdateActiveSpell(name);
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        wizard.UpdateActiveSpell(name);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellChooserButtonController : MonoBehaviour, IPointerEnterHandler{
    private WizardController wizard;
    // Use this for initialization
    void Start () {
        wizard = FindObjectOfType<WizardController>();
}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        wizard.UpdateActiveSpell(name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpellChooserButtonController : MonoBehaviour{
    public enum SpellType
    {
        Fire,
        Ice,
        Mimic,
        Gust

    };
    public SpellType spellType;
    private WizardController wizard;
    Camera mCamera;
    private RectTransform rt;
    // Use this for initialization
    void Start()
    {
        wizard = FindObjectOfType<WizardController>();
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wizard != null)
        {
            // transform.position = new Vector3(wizard.transform.position.x, wizard.transform.position.y, 0f);
    
            switch (spellType)
            {
                case SpellType.Fire:
                    rt.position = mCamera.WorldToScreenPoint(new Vector3(wizard.transform.position.x - 3, wizard.transform.position.y, wizard.transform.position.z));
                    break;
                case SpellType.Ice:
                    rt.position = mCamera.WorldToScreenPoint(new Vector3(wizard.transform.position.x, wizard.transform.position.y + 1, wizard.transform.position.z));
                    break;
                case SpellType.Gust:
                    rt.position = mCamera.WorldToScreenPoint(new Vector3(wizard.transform.position.x + 3, wizard.transform.position.y, wizard.transform.position.z));
                    break;
                case SpellType.Mimic:
                    rt.position = mCamera.WorldToScreenPoint(new Vector3(wizard.transform.position.x, wizard.transform.position.y - 1, wizard.transform.position.z));
                    break;
            }
        }
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

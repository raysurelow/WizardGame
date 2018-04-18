using UnityEngine;
using System.Collections;

public abstract class AbstractSwitchable : MonoBehaviour {

    public GameObject[] switches;

    protected abstract void Start();

    protected abstract void Update();

    protected bool AllSwitchesAreOn()
    {
        bool allSwitchesAreOn = true;

        foreach (GameObject switchObject in switches)
        {
            AbstractSwitch switchController = switchObject.GetComponent<AbstractSwitch>();
            if (!switchController.IsSwitchedOn)
            {
                allSwitchesAreOn = false;
                break;
            }
        }

        return allSwitchesAreOn;
    }
}

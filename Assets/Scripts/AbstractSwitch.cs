using UnityEngine;
using System.Collections;

public abstract class AbstractSwitch : MonoBehaviour {

    public bool IsSwitchedOn { get; set; }

    protected abstract void Start();

    protected abstract void Update();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Spell : ScriptableObject {

    public string spellName;
    public Rigidbody2D spellRigidBody;
    public LayerMask impactedLayers;

}

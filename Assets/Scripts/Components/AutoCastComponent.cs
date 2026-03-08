using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCastComponent : MonoBehaviour
{
    public Character owner;

    private void Awake()
    {
        owner = GetComponent<Character>();
    }

    private void Update()
    {
        foreach(Ability ability in owner.abilities)
        {
            ability.Activate(ability.targetingComponent.FindNearestTarget());
        }
    }
}

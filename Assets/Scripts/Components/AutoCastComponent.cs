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
        foreach (AbilityRuntime abilityRuntime in owner.abilityComponent.abilitiesRuntime)
        {
            if(owner.targetingComponent.target)
            abilityRuntime.ability.Activate(owner, owner.targetingComponent.target);
        }
    }
}

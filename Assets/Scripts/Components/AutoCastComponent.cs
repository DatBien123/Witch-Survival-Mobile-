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
            owner.abilityComponent.ActivateAbility(abilityRuntime);
        }
    }
}

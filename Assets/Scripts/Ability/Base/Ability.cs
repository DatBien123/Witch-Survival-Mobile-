using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AbilityData
{
    public float cooldown;
    public List<SO_Effect> effects;
}
public class Ability : MonoBehaviour, IAbility
{
    public Character causer;
    public Character targetApplied;

    [Header("Ability Components")]
    public Trajectory trajectory;

    [Header("Ability Data")]
    public SO_Ability abilityData;

    private void Awake()
    {
        trajectory = GetComponent<Trajectory>();
    }
    private void Start()
    {

    }
    public void Activate(Character causer, Character target)
    {

        targetApplied = target;
        transform.LookAt(target.transform.position);

        trajectory.Execute(causer, target, this);
    }


    public void Cancel()
    {
    }

    public void Deactivate()
    {
    }

    public void Unlock(Character target = null)
    {
    }

    public void Upgrade(Character target = null)
    {
    }
}
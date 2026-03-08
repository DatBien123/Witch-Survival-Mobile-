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

    public bool isActivateAble = false;
    public float currentCooldown;

    [Header("Ability Components")]
    public TargetingComponent targetingComponent;
    public Trajectory trajectory;

    [Header("Ability Data")]
    public SO_Ability abilityData;

    private void Awake()
    {
        targetingComponent = GetComponent<TargetingComponent>();
        trajectory = GetComponent<Trajectory>();
    }
    private void Start()
    {
        currentCooldown = 0.0f;
        isActivateAble = true;
    }
    public void Activate(Character target)
    {
        if (target == null || !isActivateAble) return;

        targetApplied = target;
        transform.LookAt(target.transform.position);

        trajectory.Execute(causer, target, this);

        StartCooldown();
    }
    Coroutine C_Cooldown;
    public void StartCooldown()
    {
        if (C_Cooldown != null) StopCoroutine(C_Cooldown);
        C_Cooldown = StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        currentCooldown = abilityData.data.cooldown;
        isActivateAble = false;

        while(currentCooldown >= 0.0f)
        {
            currentCooldown -= Time.deltaTime;
            yield return null;
        }
        isActivateAble = true;
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Offset
{
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public Vector3 scaleOffset;
}
[System.Serializable]
public struct AbilityData
{
    [Header("Logics")]
    public float cooldown;
    public bool isDestroyAfterCollide;
    public bool isChaseTarget;

    [Header("Settings")]
    public bool spawnVFX;
    public int vfxIndex;
    public float vfxLifeTime;
    public Offset offset;

    public bool playSFX;
    public AudioClip sfxClip;
    public float volume;

    [Header("Logics")]
    public List<SO_Effect> effects;
}
public class Ability : MonoBehaviour, IAbility
{
    public Character causer;
    public Character target;

    [Header("Ability Components")]
    public Trajectory trajectory;
    public Effects effects;

    [Header("Ability Data")]
    public SO_Ability abilityData;

    private void Awake()
    {
        trajectory = GetComponent<Trajectory>();
    }
    private void Start()
    {

    }
    public void Activate(Character causerTemp, Character targetTemp, Effects effectTemp)
    {
        causer = causerTemp;
        target = targetTemp;
        effects = effectTemp;

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
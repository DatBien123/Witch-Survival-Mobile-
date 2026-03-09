using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum EffectType
{
    Instant,
    Duration
}

[System.Serializable]
public struct EffectData
{
    [Header("Logics")]
    public Attributes attributes;
    public EffectType type;

    [Header("Settings")]
    public bool spawnVFX;
    public int vfxIndex;
    public float vfxLifeTime;
    public Offset offset;

    public bool playSFX;
    public AudioClip sfxClip;
    public float volume;

    [Header("Logics")]
    public bool isApplyOnStart;
    public float duration;
    public float perTimeUpdate;
}

[System.Serializable]
public class EffectRuntime
{
    public Character target;

    public SO_Effect effect;
    public ParticleSystem particleSystem;
    public int level;
}

public class Effects : MonoBehaviour
{
    public List<EffectRuntime> effectsRuntime;

    public Character currentTarget;

    public UnityAction<Character, EffectRuntime> OnEffectApplied;

    void OnEnable()
    {
        OnEffectApplied += OnEffectApply;
    }
    void OnDisable()
    {
        OnEffectApplied -= OnEffectApply;
    }

    private void OnEffectApply(Character target, EffectRuntime effectRuntime)
    {
        if (effectRuntime.effect.data.spawnVFX)
            VFXManager.Instance.SpawnEffect(effectRuntime);
    }

    public void ApplyEffect(Character target)
    {
        currentTarget = target;

        foreach (EffectRuntime effectRuntime in effectsRuntime)
        {
            StartEffect(effectRuntime);
        }
    }

    public void StartEffect(EffectRuntime effectRuntime)
    {
        // Fire Event
        OnEffectApplied?.Invoke(currentTarget, effectRuntime);

        if (effectRuntime.effect.data.type == EffectType.Instant)
        {
            currentTarget.attributeComponent.UpdateAttribute(effectRuntime.effect.data);
        }
        else if (effectRuntime.effect.data.type == EffectType.Duration)
        {
            StartCoroutine(UpdateEffect(effectRuntime));
        }
    }

    float elapsedTime = 0.0f;
    float elapsedPerTimeUpdate = 0.0f;

    IEnumerator UpdateEffect(EffectRuntime effectRuntime)
    {
        while (elapsedTime < effectRuntime.effect.data.duration)
        {
            if (elapsedPerTimeUpdate >= effectRuntime.effect.data.perTimeUpdate)
            {
                currentTarget.attributeComponent.UpdateAttribute(effectRuntime.effect.data);
                elapsedPerTimeUpdate = 0;
                OnEffectApplied?.Invoke(currentTarget, effectRuntime);
            }

            elapsedTime += Time.deltaTime;
            elapsedPerTimeUpdate += Time.deltaTime;

            yield return null;
        }

        elapsedPerTimeUpdate = 0;
        elapsedTime = 0;
    }
}
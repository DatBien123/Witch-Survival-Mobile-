using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum VFXType
{
    Ability,
    Effect
}
public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [SerializeField]
    private List<VFXPoolEntry> vfxAttackPools = new List<VFXPoolEntry>();
    [SerializeField]
    private List<VFXPoolEntry> vfxReactionPools = new List<VFXPoolEntry>();

    private Dictionary<int, VFXPoolEntry> poolAttackLookup;
    private Dictionary<int, VFXPoolEntry> poolReactionLookup;

    private void Awake()
    {
        Instance = this;

        poolAttackLookup = new Dictionary<int, VFXPoolEntry>();
        poolReactionLookup = new Dictionary<int, VFXPoolEntry>();

        foreach (var entry in vfxAttackPools)
        {
            entry.pool = new ObjectPooler<VFXInstance>();
            entry.pool.Initialize(this, entry.poolCount, entry.prefab, transform);

            poolAttackLookup.Add(entry.index, entry);
        }

        foreach (var entry in vfxReactionPools)
        {
            entry.pool = new ObjectPooler<VFXInstance>();
            entry.pool.Initialize(this, entry.poolCount, entry.prefab, transform);

            poolReactionLookup.Add(entry.index, entry);
        }
    }

    public void SpawnAbility(AbilityRuntime abilityRuntime)
    {

                if (!poolAttackLookup.ContainsKey(abilityRuntime.ability.abilityData.data.vfxIndex))
                {
                    Debug.LogWarning($"VFX index {abilityRuntime.ability.abilityData.data.vfxIndex} not found!");
                    return;
                }

                var entry = poolAttackLookup[abilityRuntime.ability.abilityData.data.vfxIndex];

                var projectile = entry.pool.GetNew();
                projectile.Play(abilityRuntime.causer.transform.position + abilityRuntime.ability.abilityData.data.offset.positionOffset,
                    abilityRuntime.ability.abilityData.data.vfxLifeTime);

                Ability ability = projectile.GetComponent<Ability>();
                ability.Activate(abilityRuntime.causer, abilityRuntime.target, abilityRuntime.ability.effects);
    }

    public void SpawnEffect(EffectRuntime effectRuntime)
    {

        if (!poolReactionLookup.ContainsKey(effectRuntime.effect.data.vfxIndex))
        {
            Debug.LogWarning($"VFX index {effectRuntime.effect.data.vfxIndex} not found!");
            return;
        }

        var entry = poolReactionLookup[effectRuntime.effect.data.vfxIndex];

        var effects = entry.pool.GetNew();
        effects.Play(effectRuntime.target.transform.position + effectRuntime.effect.data.offset.positionOffset,
            effectRuntime.effect.data.vfxLifeTime);
    }
}
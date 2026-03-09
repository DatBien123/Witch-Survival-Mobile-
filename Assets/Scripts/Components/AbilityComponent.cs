using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class AbilityRuntime
{
    public Ability ability;

    public int level;
    public float currentCooldown;
    public float Cooldown;
    public bool isActivateAble = true;
}
public class AbilityComponent : MonoBehaviour
{
    public Character owner;
    public List<AbilityRuntime> abilitiesRuntime;

    private void Awake()
    {
        owner = GetComponent<Character>();
    }
    void Start()
    {
        InitAbility();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Debug.Log("AAA");
            if (abilitiesRuntime.Count > 0)
            {
                ActivateAbility(abilitiesRuntime[0]);
            }
        }
    }
    void InitAbility()
    {
        foreach (var ability in abilitiesRuntime)
        {
            ability.currentCooldown = 0;
            ability.Cooldown = ability.ability.abilityData.data.cooldown;
            ability.isActivateAble = true;
        }

    }
    public void ActivateAbility(AbilityRuntime abilityRuntime)
    {
        if (owner.targetingComponent.target == null || abilityRuntime.isActivateAble == false) return;

        StartCooldown(abilityRuntime);

        Ability abilityInstance = Instantiate(
            abilityRuntime.ability,
            transform.position + new Vector3(0, 1.5f, 0),
            transform.rotation
        );
        abilityInstance.causer = owner;

        abilityInstance.Activate(owner, owner.targetingComponent.target);
    }

    #region [Cooldown]
    Coroutine C_Cooldown;
    public void StartCooldown(AbilityRuntime abilityRuntime)
    {
        if (C_Cooldown != null) StopCoroutine(C_Cooldown);
        C_Cooldown = StartCoroutine(Cooldown(abilityRuntime));
    }
    IEnumerator Cooldown(AbilityRuntime abilityRuntime)
    {
        abilityRuntime.currentCooldown = abilityRuntime.ability.abilityData.data.cooldown;
        abilityRuntime.isActivateAble = false;

        while (abilityRuntime.currentCooldown >= 0.0f)
        {
            abilityRuntime.currentCooldown -= Time.deltaTime;
            yield return null;
        }
        abilityRuntime.isActivateAble = true;
    }
    #endregion
    public void AddAbility(AbilityRuntime ability)
    {
        abilitiesRuntime.Add(ability);
    }
    public void RemoveAbility(AbilityRuntime ability)
    {
        abilitiesRuntime.Remove(ability);
    }
}

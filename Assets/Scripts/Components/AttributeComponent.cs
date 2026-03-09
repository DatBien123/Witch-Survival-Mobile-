using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeComponent : MonoBehaviour
{
    public Character owner;

    public Attributes HP;
    public Attributes HP_Max;
    public Attributes MP;
    public Attributes MP_Max;
    public Attributes MP_Regen;
    public Attributes HP_Regen;
    public Attributes AD;
    public Attributes AP;
    public Attributes Speed;

    private void Awake()
    {
        owner = GetComponent<Character>();
    }
    public void StartUpdateAttributes(Effects effectData)
    {
        if (effectData.type == EffectType.Instant)
        {
            //Update Attribute Instantly
            UpdateAttribute(effectData);
        }
        else if(effectData.type == EffectType.Duration)
        {
            //Update Attribute Duration
            StartCoroutine(UpdateAttributes(effectData));
        }
    }

    float elapsedTime = 0.0f;
    float elapsedPerTimeUpdate = 0.0f;
    IEnumerator UpdateAttributes(Effects effectData)
    {
        //if (effectData.isApplyOnStart)
        //{
        //    UpdateAttribute(effectData);
        //}

        while (elapsedTime < effectData.duration)
        {
            if(elapsedPerTimeUpdate >= effectData.perTimeUpdate)
            {
                UpdateAttribute(effectData);
                elapsedPerTimeUpdate = 0;
            }
            elapsedTime += Time.deltaTime;
            elapsedPerTimeUpdate+= Time.deltaTime;
            yield return null;
        }

        elapsedPerTimeUpdate = 0;
        elapsedTime = 0;
    }

    void UpdateAttribute(Effects effectData)
    {
        switch (effectData.attributes.AttributeType)
        {
            case EAttributeType.HP:
                HP.OnChange(effectData.attributes.amount);
                //Clamp
                HP.amount = Mathf.Clamp(HP.amount, 0, HP_Max.amount);
                break;
            case EAttributeType.HP_Regen:
                HP_Regen.OnChange(effectData.attributes.amount);
                break;
            case EAttributeType.MP:
                MP.OnChange(effectData.attributes.amount);
                //Clamp
                MP.amount = Mathf.Clamp(MP.amount, 0, MP_Max.amount);
                break;
            case EAttributeType.MP_Regen:
                MP_Regen.OnChange(effectData.attributes.amount);
                break;
            case EAttributeType.Speed:
                Speed.OnChange(effectData.attributes.amount);
                break;
            case EAttributeType.AD:
                AD.OnChange(effectData.attributes.amount);
                break;
            case EAttributeType.AP:
                AP.OnChange(effectData.attributes.amount);
                break;
        }
    }
}

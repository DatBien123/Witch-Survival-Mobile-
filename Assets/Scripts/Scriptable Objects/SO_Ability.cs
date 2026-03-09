using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ability Data", fileName = "Ability Data")]
public class SO_Ability : ScriptableObject
{
    public AbilityData data;
}

[CreateAssetMenu(menuName = "Ability/Effect Data", fileName = "Effect Data")]
public class SO_Effect : ScriptableObject
{
    public EffectData data;
}
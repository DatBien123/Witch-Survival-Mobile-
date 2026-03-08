[System.Serializable]
public enum EffectType
{
    Instant,
    Duration
}
[System.Serializable]
public struct Effects
{
    public Attributes attributes;
    public EffectType type;

    public bool isApplyOnStart;
    public float duration;
    public float perTimeUpdate;

    public void ApplyEffect(Character target)
    {
        target.attributeComponent.StartUpdateAttributes(this);
    }
}

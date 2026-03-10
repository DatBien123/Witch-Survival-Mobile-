public enum EAttributeType
{
    HP,
    HP_Max,
    HP_Regen,
    MP,
    MP_Max,
    MP_Regen,
    AD,
    AP,
    Speed
}

[System.Serializable]
public struct Attributes 
{
    public EAttributeType AttributeType;
    public float amount;
    public Attributes(EAttributeType attributeType, float amount)
    {
        AttributeType = attributeType;
        this.amount = amount;
    }
    public void OnChange(float amount)
    {
        this.amount += amount;
    }
}

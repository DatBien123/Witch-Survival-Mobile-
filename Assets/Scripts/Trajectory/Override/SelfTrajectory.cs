public class SelfTrajectory : Trajectory
{
    public override void Execute(Character caster, Character target, Ability ability)
    {
        foreach(SO_Effect effect in ability.abilityData.data.effects)
        {
            effect.data.ApplyEffect(caster);
        }
    }
}
public class SelfTrajectory : Trajectory
{
    public override void Execute(Character caster, Character target, Ability ability)
    {
            ability.effects.ApplyEffect(caster);
    }
}
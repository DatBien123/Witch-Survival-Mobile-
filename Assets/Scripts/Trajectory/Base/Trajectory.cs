using UnityEngine;

public abstract class Trajectory : MonoBehaviour, ITrajectory
{
    public Character caster;
    public Character target;
    public Ability ability;
    public abstract void Execute(Character caster, Character target, Ability ability);
}
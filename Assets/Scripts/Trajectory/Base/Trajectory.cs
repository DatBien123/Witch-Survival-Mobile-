using UnityEngine;

public abstract class Trajectory : MonoBehaviour, ITrajectory
{
    public Character caster;
    public Character target;
    [Header("Components")]
    public HitTraceComponent hitTraceComponent;
    public Ability ability;
    public VFXInstance vfxInstance;

    protected virtual void Awake()
    {
        hitTraceComponent = GetComponent<HitTraceComponent>();
        ability = GetComponent<Ability>();
        vfxInstance = GetComponent<VFXInstance>();
    }
    public abstract void Execute(Character caster, Character target, Ability ability);
}
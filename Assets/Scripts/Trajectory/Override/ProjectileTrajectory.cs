using UnityEngine;

public class ProjectileTrajectory : Trajectory
{
    public Rigidbody rb;
    public float speed = 10f;

    public LayerMask targetLayer;   // layer mà projectile được phép hit

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Execute(Character casterTemp, Character targetTemp, Ability abilityTemp)
    {
        caster = casterTemp;
        target = targetTemp;
        ability = abilityTemp;

        Vector3 dir = (target.transform.position - caster.transform.position).normalized;

        rb.velocity = dir * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check layer trước
        if ((targetLayer.value & (1 << other.gameObject.layer)) == 0)
            return;

        Character hitTarget = other.GetComponent<Character>();
        if (hitTarget == null || hitTarget == caster)
            return;

        Debug.Log("Hit target is: " + hitTarget.gameObject.name);

        foreach (SO_Effect effect in ability.abilityData.data.effects)
        {
            effect.data.ApplyEffect(hitTarget);
        }

        Destroy(gameObject);
    }
}
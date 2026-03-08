using UnityEngine;

public class ArcTrajectory : Trajectory
{
    public GameObject projectilePrefab;
    public float force = 15f;

    public override void Execute(Character caster, Character target, Ability ability)
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            caster.transform.position,
            Quaternion.identity
        );

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 dir = (target.transform.position - caster.transform.position);
        dir.y += 1.5f;

        rb.AddForce(dir.normalized * force, ForceMode.Impulse);
    }
}
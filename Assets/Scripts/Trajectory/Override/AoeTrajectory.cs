using UnityEngine;

public class AoeTrajectory : Trajectory
{
    public float radius = 5f;

    public override void Execute(Character caster, Character target, Ability ability)
    {
        Collider[] hits = Physics.OverlapSphere(
            caster.transform.position,
            radius
        );

        foreach (var hit in hits)
        {
            Character enemy = hit.GetComponent<Character>();

            if (enemy != null && enemy != caster)
            {
                //enemy.TakeDamage(ability.damage);
            }
        }
    }
}
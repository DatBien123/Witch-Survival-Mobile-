using UnityEngine;

public class LineTrajectory : Trajectory
{
    public float range = 10f;

    public override void Execute(Character caster, Character target, Ability ability)
    {
        Ray ray = new Ray(caster.transform.position, caster.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Character enemy = hit.collider.GetComponent<Character>();

            if (enemy != null)
            {
                //ability.effects.ApplyEffect(enemy, )
            }
        }
    }
}
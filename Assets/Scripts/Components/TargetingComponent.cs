using UnityEngine;

public class TargetingComponent : MonoBehaviour
{
    public Ability abilityOwned;
    public Character target;

    [Header("Targeting Settings")]
    public float searchRadius = 10f;
    public LayerMask enemyLayer;

    private Collider[] results = new Collider[32];

    private void Awake()
    {
        abilityOwned = GetComponent<Ability>();
    }

    public Character FindNearestTarget()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            searchRadius,
            results,
            enemyLayer
        );

        float closestDistance = float.MaxValue;
        Character closestTarget = null;

        for (int i = 0; i < hitCount; i++)
        {
            Character enemy = results[i].GetComponent<Character>();

            if (enemy == null || enemy == abilityOwned.causer)
                continue;

            float dist = (enemy.transform.position - transform.position).sqrMagnitude;

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestTarget = enemy;
            }
        }

        target = closestTarget;
        return closestTarget;
    }
}
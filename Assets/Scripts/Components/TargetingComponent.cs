using UnityEngine;

public class TargetingComponent : MonoBehaviour
{
    public Character owner;
    public Character target;

    public float searchRadius = 10f;
    public LayerMask enemyLayer;

    Collider[] results = new Collider[32];

    TickTimer tick;

    void Awake()
    {
        owner = GetComponent<Character>();

        tick = new TickTimer(0.3f);
        tick.OnTick += UpdateTarget;
    }

    void Update()
    {
        tick.Update(Time.deltaTime);
    }

    void UpdateTarget()
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

            if (enemy == null || enemy == owner)
                continue;

            float dist = (enemy.transform.position - transform.position).sqrMagnitude;

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestTarget = enemy;
            }
        }

        target = closestTarget;
    }
}
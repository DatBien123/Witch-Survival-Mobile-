using System.Collections;
using UnityEngine;

public class ProjectileTrajectory : Trajectory
{
    public float speed = 10f;

    public LayerMask targetLayer;

    public float maxDistance = 10f;

    public bool isDestroyAfterCollide = true;
    public bool isChaseTarget = false;

    Vector3 startPosition;

    Coroutine moveRoutine;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Execute(Character casterTemp, Character targetTemp, Ability abilityTemp)
    {
        caster = casterTemp;
        target = targetTemp;
        ability = abilityTemp;
        

        startPosition = transform.position;

        moveRoutine = StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Vector3 direction;

        while (true)
        {
            if (isChaseTarget && target != null)
            {
                direction = (target.transform.position + new Vector3(0, 0.75f, 0) - transform.position).normalized;
            }
            else
            {
                direction = transform.forward;
            }

            transform.position += direction * speed * Time.deltaTime;

            float distance = (transform.position - startPosition).sqrMagnitude;

            if (distance > maxDistance * maxDistance)
            {
                gameObject.GetComponent<VFXInstance>().pool.Free(vfxInstance);
                yield break;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayer.value & (1 << other.gameObject.layer)) == 0)
            return;

        CharacterEnemy hitTarget = other.GetComponent<CharacterEnemy>();
        Debug.Log("Hit Target: " + hitTarget.gameObject.name);

        if (hitTarget == null || hitTarget == caster)
            return;

            ability.effects.ApplyEffect(hitTarget);


        if (isDestroyAfterCollide)
        {
            gameObject.GetComponent<VFXInstance>().pool.Free(vfxInstance);
        }
    }
}
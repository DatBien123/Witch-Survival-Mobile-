using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StunData
{
    public bool IsStun;
    public float Duration;

    public StunData(bool isStun, float duration)
    {
        IsStun = isStun;
        Duration = duration;
    }
}
[System.Serializable]
public struct KnockbackData
{
    public bool IsKnockback;
    public Vector3 Direction;
    public float Force;
    public float Damping;

    public KnockbackData(bool isKnockback, Vector3 direction, float force, float damping = 5f)
    {
        IsKnockback = isKnockback;
        Direction = direction;
        Force = force;
        Damping = damping;
    }
}
[System.Serializable]
public struct AirborneData
{
    public bool IsAirborne;
    public float UpwardForce;

    public AirborneData(bool isAirborne, float upwardForce)
    {
        IsAirborne = isAirborne;
        UpwardForce = upwardForce;
    }
}
[System.Serializable]
public struct SlowData
{
    public bool IsSlow;
    public float SlowPercent;
    public float Duration;

    public SlowData(bool isSlow, float slowPercent, float duration)
    {
        IsSlow = isSlow;
        SlowPercent = slowPercent;
        Duration = duration;
    }
}
[System.Serializable]
public struct FreezeData
{
    public bool IsFreeze;
    public float Duration;

    public FreezeData(bool isFreeze, float duration)
    {
        IsFreeze = isFreeze;
        Duration = duration;
    }
}


[System.Serializable]
public struct HitReactionData
{
    public StunData Stun;
    public KnockbackData Knockback;
    public AirborneData Airborne;
    public SlowData Slow;
    public FreezeData Freeze;

    public static HitReactionData Default => new HitReactionData
    {
        Stun = new StunData(false, 0),
        Knockback = new KnockbackData(false, Vector3.zero, 0),
        Airborne = new AirborneData(false, 0),
        Slow = new SlowData(false, 0, 0),
        Freeze = new FreezeData(false, 0)
    };
}

[System.Serializable]
public struct PullData
{
    public bool IsPull;
    public Vector3 Center;
    public float Force;
    public float Duration;
    public float StopDistance;

    public PullData(bool isPull, Vector3 center, float force, float duration, float stopDistance = 0.5f)
    {
        IsPull = isPull;
        Center = center;
        Force = force;
        Duration = duration;
        StopDistance = stopDistance;
    }
}
public class HitReactionComponent : MonoBehaviour
{
    public Character owner;

    private void Awake()
    {
        owner = GetComponent<Character>();
    }

    Coroutine C_Knockback;
    Coroutine C_Airborne;
    Coroutine C_Slow;
    Coroutine C_Pull;
    Coroutine C_Freeze;
    public void StartKnockback(KnockbackData data)
    {
        if (C_Knockback != null) StopCoroutine(Knockback(data));
        C_Knockback = StartCoroutine(Knockback(data));
    }
    IEnumerator Knockback(KnockbackData data)
    {
        Vector3 velocity = data.Direction.normalized * data.Force;

        while (velocity.magnitude > 0.1f)
        {
            owner._controller.Move(velocity * Time.deltaTime);

            velocity = Vector3.Lerp(
                velocity,
                Vector3.zero,
                data.Damping * Time.deltaTime
            );

            yield return null;
        }
    }
    public void StartAirborne(AirborneData data)
    {
        if (C_Airborne != null) StopCoroutine(Airborne(data));
        C_Airborne = StartCoroutine(Airborne(data));
    }
    IEnumerator Airborne(AirborneData data)
    {
        float verticalVelocity = data.UpwardForce;

        while (!owner.Grounded || verticalVelocity > 0)
        {
            verticalVelocity += owner.Gravity * Time.deltaTime;

            Vector3 move = new Vector3(0, verticalVelocity, 0);

            owner._controller.Move(move * Time.deltaTime);

            yield return null;
        }
    }
    public void StartSlow(SlowData data)
    {
        if (C_Slow != null) StopCoroutine(Slow(data));
        C_Slow = StartCoroutine(Slow(data));
    }
    IEnumerator Slow(SlowData data)
    {
        float originalSpeed = owner.MoveSpeed;

        owner.MoveSpeed *= (1f - data.SlowPercent);

        yield return new WaitForSeconds(data.Duration);

        owner.MoveSpeed = originalSpeed;
    }
    public void StartFreeze(FreezeData data)
    {
        if (C_Pull != null) StopCoroutine(Freeze(data));
        C_Pull = StartCoroutine(Freeze(data));
    }
    IEnumerator Freeze(FreezeData data)
    {
        owner.MoveSpeed = 0;

        yield return new WaitForSeconds(data.Duration);

        owner.MoveSpeed = 2.0f;
    }
    public void StartPull(PullData data)
    {
        if (C_Freeze != null) StopCoroutine(Pull(data));
        C_Freeze = StartCoroutine(Pull(data));
    }
    IEnumerator Pull(PullData data)
    {
        float timer = 0;

        while (timer < data.Duration)
        {
            Vector3 dir = data.Center - transform.position;
            float distance = dir.magnitude;

            if (distance > data.StopDistance)
            {
                Vector3 move = dir.normalized * data.Force * Time.deltaTime;
                owner._controller.Move(move);
            }

            timer += Time.deltaTime;

            yield return null;
        }
    }
}

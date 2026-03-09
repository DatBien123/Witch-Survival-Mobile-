using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour, IDamageable
{
    public CharacterController _controller;
    public Animator _animator;
    public HitReactionComponent hitReactionComponent;
    public AttributeComponent attributeComponent;



    #region [Settings]
    [Header("Character Settings")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;
    #endregion
    protected virtual void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        hitReactionComponent = GetComponent<HitReactionComponent>();
        attributeComponent = GetComponent<AttributeComponent>();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void LateUpdate() { }

    protected virtual void FixedUpdate() { }

    public void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);

        // update animator if using character
        _animator.SetBool(AnimationParams.GROUNDED, Grounded);
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
    public List<Ability> abilities;
    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }
    public void RemoveAbility(Ability ability) { 
        abilities.Remove(ability); 
    }
    public void ActivateAbility(Ability abilityPrefab)
    {
        Ability abilityInstance = Instantiate(
            abilityPrefab,
            transform.position + new Vector3(0, 1.5f, 0),
            transform.rotation
        );
        abilityInstance.causer = this;

        abilityInstance.Activate(abilityInstance.targetingComponent.FindNearestTarget());
    }
}

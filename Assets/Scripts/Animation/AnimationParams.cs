using UnityEngine;

public static class AnimationParams
{
    //Paramaters
    public static readonly int SPEED = Animator.StringToHash("Speed");
    public static readonly int GROUNDED = Animator.StringToHash("Grounded");
    public static readonly int JUMP = Animator.StringToHash("Jump");
    public static readonly int FREE_FALL = Animator.StringToHash("FreeFall");
    public static readonly int MOTION_SPEED = Animator.StringToHash("MotionSpeed");
}
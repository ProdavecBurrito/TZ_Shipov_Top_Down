using UnityEngine;

public class PlayerAnimationController
{
    private AnimationClip _run;
    private AnimationClip _leftRun;
    private AnimationClip _rightRun;
    private AnimationClip _backRun;
    private AnimationClip _idle;
    private Animator _animator;

    public PlayerAnimationController(Animator animator)
    {
        _animator = animator;
        _run = Resources.Load<AnimationClip>("Run_Rifle");
        _leftRun = Resources.Load<AnimationClip>("Run_Left_Rifle");
        _rightRun = Resources.Load<AnimationClip>("Run_Right_Rifle");
        _backRun = Resources.Load<AnimationClip>("Back_Run_Rifle");
        _idle = Resources.Load<AnimationClip>("Idle");
    }

    public void PlayClip(string clipName)
    {
        _animator.Play(clipName);
    }
}
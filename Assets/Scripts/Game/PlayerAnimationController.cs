using UnityEngine;

public class PlayerAnimationController
{
    public Animator _animator;

    public PlayerAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void SetAnimationTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
using UnityEngine;

public class PlayerAnimationController
{
    private Animator _animator;

    public Animator Animator => _animator;

    public PlayerAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void SetFloat(string name, float value)
    {
        _animator.SetFloat(name, value);
    }

    public void SetAnimationTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
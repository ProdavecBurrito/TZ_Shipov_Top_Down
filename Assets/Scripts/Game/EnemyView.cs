using UnityEngine;
using System;

public class EnemyView : MonoBehaviour, IEnemy
{
    public event Action OnDie = delegate () { };

    private Rigidbody[] _ragDollRigidbodies;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = true;

        _ragDollRigidbodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < _ragDollRigidbodies.Length; i++)
        {
            _ragDollRigidbodies[i].isKinematic = true;
            _ragDollRigidbodies[i].mass = 1000;
        }
    }

    public void Die()
    {
        OnDie.Invoke();
        foreach (var item in _ragDollRigidbodies)
        {
            item.isKinematic = false;
        }
        _animator.enabled = false;
    }

    public void SetActivity(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void Resurrect()
    {
        for (int i = 0; i < _ragDollRigidbodies.Length; i++)
        {
            _ragDollRigidbodies[i].isKinematic = true;
        }
        transform.rotation = Quaternion.identity;
        _animator.enabled = true;
    }
}
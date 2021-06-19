using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public event Action CanFinishing = delegate () { };
    public event Action CantFinishing = delegate () { };
    public event Action OnFinishingOver = delegate () { };

    [SerializeField] private Transform _rotatingPart;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _gun;

    private IEnemy _enemy;
    private GameObject _target;

    public Transform RotatingPart => _rotatingPart;
    public Animator Animator => _animator;
    public GameObject Target => _target;
    internal IEnemy Enemy => _enemy;

    private void OnTriggerEnter(Collider other)
    {
        var enemyTarget = other.GetComponent<EnemyView>();
        if (enemyTarget)
        {
            if (enemyTarget is IEnemy enemy)
            {
                _enemy = enemy;
                CanFinishing.Invoke();
                _target = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CantFinishing.Invoke();
        _target = null;
    }

    private void FinishingOver()
    {
        OnFinishingOver.Invoke();
    }

    public void SetSwordActivity(bool activity)
    {
        _sword.gameObject.SetActive(activity);
        _gun.gameObject.SetActive(!activity);
    }
}
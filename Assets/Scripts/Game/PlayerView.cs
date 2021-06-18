using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _rotatingPart;
    [SerializeField] private Animator _animator;

    public Transform RotatingPart => _rotatingPart;
    public Animator Animator => _animator;
}
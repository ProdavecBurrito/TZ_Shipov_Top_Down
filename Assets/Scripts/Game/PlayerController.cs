using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : IUpdate, ILateUpdate, IDisposable
{
    private PlayerAnimationController _animationController;
    private PlayerView _playerView;
    private Camera _camera;
    private Text _uiText;

    private RaycastHit _hit;
    private Vector3 _pointToLook;
    private Vector3 _rotationVector;

    private bool _isCanFinisingOff;
    private bool _isCanMove;
    private float _movementSpeed;

    public PlayerController(PlayerView playerView, Text uiText)
    {
        _isCanMove = true;
        _movementSpeed = 8.0f;
        _playerView = playerView;
        _camera = Camera.main;
        _animationController = new PlayerAnimationController(_playerView.Animator);
        _uiText = uiText;

        _playerView.CanFinishing += FinishOff;
        _playerView.CantFinishing += CantFinishOff;
        _playerView.OnFinishingOver += CanMove;
    }

    public void LateUpdateTick()
    {
        Rotate();
    }

    public void UpdateTick()
    {
        Move();
        FinishingOff();
    }

    public void Move()
    {
        if(!IsMove())
        {
            _animationController.SetAnimationTrigger("Idle");
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                _playerView.transform.Translate(MovementVector(Vector3.forward + Vector3.left, true), Space.World);
                _animationController.SetAnimationTrigger("Run");
                _animationController._animator.SetFloat("Direction", -0.5f);
                return;
                
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                _playerView.transform.Translate(MovementVector(Vector3.forward + Vector3.right, true), Space.World);
                _animationController.SetAnimationTrigger("Run");
                _animationController._animator.SetFloat("Direction", 0.5f);
                return;
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                _playerView.transform.Translate(MovementVector(-Vector3.forward + Vector3.left, true), Space.World);
                _animationController.SetAnimationTrigger("RunBack");
                _animationController._animator.SetFloat("Direction", 0.5f);
                return;
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                _playerView.transform.Translate(MovementVector(-Vector3.forward + Vector3.right, true), Space.World);
                _animationController.SetAnimationTrigger("RunBack");
                _animationController._animator.SetFloat("Direction", -0.5f);
                return;
            }
            if (Input.GetKey(KeyCode.W))
            {
                _playerView.transform.Translate(MovementVector(Vector3.forward, false), Space.World);
                _animationController.SetAnimationTrigger("Run");
                _animationController._animator.SetFloat("Direction", 0);
                return;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _playerView.transform.Translate(MovementVector(-Vector3.forward, false), Space.World);
                _animationController.SetAnimationTrigger("RunBack");
                _animationController._animator.SetFloat("Direction", 0);
                return;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _playerView.transform.Translate(MovementVector(Vector3.left, false), Space.World);
                _animationController.SetAnimationTrigger("Run");
                _animationController._animator.SetFloat("Direction", -1);
                return;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _playerView.transform.Translate(MovementVector(Vector3.right, false), Space.World);
                _animationController.SetAnimationTrigger("Run");
                _animationController._animator.SetFloat("Direction", 1);
                return;
            }
        }
    }

    private bool IsMove()
    {
        if (_isCanMove)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                return true;
            }
        }
        return false;
    }

    private void FinishingOff()
    {
        if (_isCanFinisingOff)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RotateToTarget();
                _playerView.SetSwordActivity(true);
                _animationController.SetAnimationTrigger("Finishing");
                _isCanMove = false;
                
            }
        }
    }

    private void CanMove()
    {
        _isCanMove = true;
        _playerView.transform.rotation = Quaternion.identity;
        _playerView.SetSwordActivity(false);
        _playerView.Enemy.Die();
    }

    private void FinishOff()
    {
        _isCanFinisingOff = true;
        _uiText.gameObject.SetActive(true);
    }

    private void CantFinishOff()
    {
        _isCanFinisingOff = false;
        _uiText.gameObject.SetActive(false);
    }

    private Vector3 MovementVector(Vector3 vector, bool isMixingButtons)
    {
        if (!isMixingButtons)
        {
            return vector * _movementSpeed * Time.deltaTime;
        }
        else
        {
            return vector * _movementSpeed / 1.5f * Time.deltaTime;
        }
    }

    private void RotateToTarget()
    {
        var navigationVector = new Vector3(_playerView.Target.transform.position.x, 0, _playerView.Target.transform.position.z);
        _playerView.transform.LookAt(navigationVector);
    }

    private void Rotate()
    {
        if (_isCanMove)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit, 100f))
            {
                _pointToLook = _hit.point;
                _rotationVector = Vector3.ProjectOnPlane(_playerView.RotatingPart.position - _pointToLook, Vector3.up);
                _rotationVector = Quaternion.LookRotation(-_rotationVector).eulerAngles;
                _rotationVector.Set(-_rotationVector.y, 0.0f, 0.0f);
                _playerView.RotatingPart.Rotate(_rotationVector, Space.Self);
            }
        }
    }

    public void Dispose()
    {
        _playerView.CanFinishing -= FinishOff;
        _playerView.CantFinishing -= CantFinishOff;
        _playerView.OnFinishingOver -= CanMove;
    }
}
using UnityEngine;

public class PlayerController : IUpdate, ILateUpdate
{
    private PlayerAnimationController _animationController;
    private PlayerView _playerView;
    private Camera _camera;

    private RaycastHit _hit;
    private Vector3 _pointToLook;
    private Vector3 _rotationVector;

    private float _movementSpeed;

    public PlayerController(PlayerView playerView)
    {
        _movementSpeed = 5.0f;
        _playerView = playerView;
        _camera = Camera.main;
        _animationController = new PlayerAnimationController(_playerView.Animator);
    }

    public void LateUpdateTick()
    {
        Rotate();
    }

    public void UpdateTick()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _playerView.transform.Translate(MovementVector(Vector3.forward), Space.World);
            _animationController.PlayClip("Run_Rifle");
        }
        if (Input.GetKey(KeyCode.S))
        {
            _playerView.transform.Translate(MovementVector(-Vector3.forward), Space.World);
            _animationController.PlayClip("Back_Run_Rifle");
        }
        if (Input.GetKey(KeyCode.D))
        {
            _playerView.transform.Translate(MovementVector(Vector3.left), Space.World);
            _animationController.PlayClip("Run_Left_Rifle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            _playerView.transform.Translate(MovementVector(Vector3.right), Space.World);
            _animationController.PlayClip("Run_Right_Rifle");
        }
        else
        {
            _animationController.PlayClip("Idle");
        }
    }

    private Vector3 MovementVector(Vector3 vector)
    {
        return vector * _movementSpeed * Time.deltaTime;
    }

    private void Rotate()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit, 100f))
        {
            _pointToLook = _hit.point;
            _rotationVector = Vector3.ProjectOnPlane(_playerView.RotatingPart.position -_pointToLook, Vector3.up);
            _rotationVector = Quaternion.LookRotation(-_rotationVector).eulerAngles;
            _rotationVector.Set(-_rotationVector.y, 0.0f, 0.0f);
            _playerView.RotatingPart.Rotate(_rotationVector, Space.Self);
        }
    }
}
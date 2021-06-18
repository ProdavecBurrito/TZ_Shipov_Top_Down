using UnityEngine;

public class CameraController : ILateUpdate
{
    private const float SMOOTH = 0.2f;

    private Transform _followingTarget;
    private Camera _camera;

    private Vector3 _offset;
    private Vector3 _velosity = Vector3.zero;

    public CameraController(Transform followingTarget)
    {
        _followingTarget = followingTarget;
        _camera = Camera.main;
        _offset = _camera.transform.position - _followingTarget.transform.position;
        _camera.transform.position = _followingTarget.transform.position + _offset;
    }

    public void LateUpdateTick()
    {
        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, _followingTarget.transform.position + _offset, ref _velosity, SMOOTH);
    }
}

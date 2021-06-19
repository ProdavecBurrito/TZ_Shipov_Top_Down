using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private Text _text;

    private PlayerController _playerController;
    private EnemyController _enemyController;
    private CameraController _cameraController;

    private void Start()
    {
        _playerController = new PlayerController(_playerView, _text);
        _enemyController = new EnemyController(_enemyView);
        _cameraController = new CameraController(_playerView.transform);

        UpdatingController.AddToUpdate(_playerController);
        UpdatingController.AddToUpdate(_enemyController);
        UpdatingController.AddToLateUpdate(_cameraController);
        UpdatingController.AddToLateUpdate(_playerController);
    }

    private void Update()
    {
        UpdatingController.UpdateAll();
    }

    private void FixedUpdate()
    {
        UpdatingController.FixedUpdateAll();
    }

    private void LateUpdate()
    {
        UpdatingController.LateUpdateAll();
    }
}

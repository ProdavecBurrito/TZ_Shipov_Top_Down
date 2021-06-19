using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemyController : IUpdate, IDisposable
{
    private EnemyView _enemyView;
    private Timer _timer;
    private Vector3 _spawnVector;

    public EnemyController(EnemyView enemyView)
    {
        _enemyView = enemyView;
        _timer = new Timer();

        _enemyView.OnDie += Die;
        _timer.OnEndCountDown += CalculateSpawnPosition;
    }

    public void UpdateTick()
    {
        _timer.CountTime();
    }

    private void MoveTo(Vector3 position)
    {
        _enemyView.transform.position = position;
        _enemyView.SetActivity(true);
    }

    private void CalculateSpawnPosition()
    {
        _enemyView.SetActivity(false);
        var xPosition = Random.Range(-25, 25);
        var zPosition = Random.Range(-25, 25);
        _spawnVector.Set(xPosition, 0, zPosition);
        MoveTo(_spawnVector);
        _enemyView.Resurrect();
    }

    private void Die()
    {
        _timer.Init(5.0f);
    }

    public void Dispose()
    {
        _enemyView.OnDie -= Die;
        _timer.OnEndCountDown -= CalculateSpawnPosition;
    }
}
using System;
using System.Collections.Generic;

public static class UpdatingController
{
    private static List<IUpdate> _updatingObjects = new List<IUpdate>();
    private static List<IFixedUpdate> _fixedUpdates = new List<IFixedUpdate>();
    private static List<ILateUpdate> _lateUpdates = new List<ILateUpdate>();

    public static event Action OnUpdate = delegate () { };

    public static void UpdateAll()
    {
        for (int i = 0; i < _updatingObjects.Count; i++)
        {
            _updatingObjects[i].UpdateTick();
        }
        OnUpdate.Invoke();
    }

    public static void FixedUpdateAll()
    {
        for (int i = 0; i < _fixedUpdates.Count; i++)
        {
            _fixedUpdates[i].FixedUpdateTick();
        }
    }

    public static void LateUpdateAll()
    {
        for (int i = 0; i < _lateUpdates.Count; i++)
        {
            _lateUpdates[i].LateUpdateTick();
        }
    }

    public static void AddToFixedUpdate(IFixedUpdate update)
    {
        if (!_fixedUpdates.Contains(update))
        {
            _fixedUpdates.Add(update);
        }
    }

    public static void RemoveFromFixedUpdate(IFixedUpdate update)
    {
        if (_fixedUpdates.Contains(update))
        {
            _fixedUpdates.Remove(update);
        }
    }

    public static void AddToLateUpdate(ILateUpdate update)
    {
        if (!_lateUpdates.Contains(update))
        {
            _lateUpdates.Add(update);
        }
    }

    public static void RemoveFromLateUpdate(ILateUpdate update)
    {
        if (_lateUpdates.Contains(update))
        {
            _lateUpdates.Remove(update);
        }
    }

    public static void AddToUpdate(IUpdate update)
    {
        if (!_updatingObjects.Contains(update))
        {
            _updatingObjects.Add(update);
        }
    }

    public static void RemoveFromUpdate(IUpdate update)
    {
        if (_updatingObjects.Contains(update))
        {
            _updatingObjects.Remove(update);
        }
    }

    public static void RemoveAllFromUpdate()
    {
        _updatingObjects.Clear();
    }

    public static void SubscribeToTUpdate(Action action)
    {
        OnUpdate += action;
    }

    public static void UnsubscribeFromUpdate(Action action)
    {
        OnUpdate -= action;
    }
}
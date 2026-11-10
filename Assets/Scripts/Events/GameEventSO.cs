using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Game Event")]
public class GameEventSO : ScriptableObject
{
    private readonly List<IGameEventListener> listeners = new();

    public void RegisterListener(IGameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(IGameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void Raise(Object data = null)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] == null)
                listeners.RemoveAt(i);
            else
                listeners[i].OnEventRaised(data);
        }
    }
}

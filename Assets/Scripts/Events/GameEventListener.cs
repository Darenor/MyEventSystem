using UnityEngine;

public class GameEventListener : MonoBehaviour, IGameEventListener
{
    [SerializeField] private GameEventSO gameEvent;

    private void OnEnable()
    {
        if (gameEvent != null)
            gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (gameEvent != null)
            gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Object data)
    {
        Debug.Log($"Получено событие: {gameEvent.name}");
    }
}

using UnityEngine;
using UnityEngine.UI;
using deVoid.Utils;

public enum UIState
{
    MainMenu,
    AddMenu,
    RemoveMenu
}

public class UIStateMachine : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject addMenuPanel;
    [SerializeField] private GameObject removeMenuPanel;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button addMenuButton;
    [SerializeField] private Button removeMenuButton;

    [SerializeField] private GameEventSO uiStateChangedEvent;

    public UIState CurrentState { get; private set; }

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(
            () => Signals.Get<UIStateSignal>().Dispatch(UIState.MainMenu));

        addMenuButton.onClick.AddListener(
            () => Signals.Get<UIStateSignal>().Dispatch(UIState.AddMenu));

        removeMenuButton.onClick.AddListener(
            () => Signals.Get<UIStateSignal>().Dispatch(UIState.RemoveMenu));
    }

    private void OnEnable()
    {
        Signals.Get<UIStateSignal>().AddListener(SetState);
    }

    private void Start()
    {
        Signals.Get<UIStateSignal>().Dispatch(UIState.MainMenu);
    }

    private void OnDisable()
    {
        Signals.Get<UIStateSignal>().RemoveListener(SetState);
    }

    private void SetState(UIState state)
    {
        CurrentState = state;

        mainMenuPanel.SetActive(state == UIState.MainMenu);
        addMenuPanel.SetActive(state == UIState.AddMenu);
        removeMenuPanel.SetActive(state == UIState.RemoveMenu);

        if (uiStateChangedEvent != null)
            uiStateChangedEvent.Raise(null);
    }
}

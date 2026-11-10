
using System;
using System.Collections.Generic;
using deVoid.Utils;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private readonly Dictionary<ResourceType, int> resources = new();

    public event Action ResourcesChanged;

    [SerializeField] private GameEventSO resetEvent;
    [SerializeField] private GameEventSO addEvent;
    [SerializeField] private GameEventSO removeEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = 0;
        }
    }

    private void OnEnable()
    {
        Signals.Get<AddResourceSignal>().AddListener(OnAddResource);
        Signals.Get<RemoveResourceSignal>().AddListener(OnRemoveResource);
        Signals.Get<ResetResourcesSignal>().AddListener(OnResetResources);
    }

    private void OnDisable()
    {
        Signals.Get<AddResourceSignal>().RemoveListener(OnAddResource);
        Signals.Get<RemoveResourceSignal>().RemoveListener(OnRemoveResource);
        Signals.Get<ResetResourcesSignal>().RemoveListener(OnResetResources);
    }

    public int GetAmount(ResourceType type)
    {
        return resources[type];
    }

    private void AddResource(ResourceType type, int amount)
    {
        if (amount <= 0)
            return;

        resources[type] += amount;

        ResourcesChanged?.Invoke();

        if (addEvent != null)
            addEvent.Raise(null);
    }

    private void RemoveResource(ResourceType type, int amount)
    {
        if (amount <= 0)
            return;

        resources[type] = Mathf.Max(0, resources[type] - amount);

        ResourcesChanged?.Invoke();

        if (removeEvent != null)
            removeEvent.Raise(null);
    }

    private void ResetResources()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = 0;
        }

        ResourcesChanged?.Invoke();

        if (resetEvent != null)
            resetEvent.Raise(null);
    }

    private void OnAddResource(ResourceType type, int amount)
    {
        AddResource(type, amount);
    }

    private void OnRemoveResource(ResourceType type, int amount)
    {
        RemoveResource(type, amount);
    }

    private void OnResetResources()
    {
        ResetResources();
    }
}

using System;
using System.Collections.Generic;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private ResourceItemUI itemPrefab;
    [SerializeField] private Button resetButton;

    private readonly List<ResourceItemUI> items = new();

    private void Start()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            items.Add(Instantiate(itemPrefab, container));
        }

        ResourceManager.Instance.ResourcesChanged += Refresh;

        resetButton.onClick.AddListener(ResetResources);

        Refresh();
    }

    private void ResetResources()
    {
        Signals.Get<ResetResourcesSignal>().Dispatch();
    }

    private void Refresh()
    {
        if (ResourceManager.Instance == null)
            return;

        int index = 0;

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            items[index].Setup(
                type,
                ResourceManager.Instance.GetAmount(type));

            index++;
        }
    }

    private void OnDestroy()
    {
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.ResourcesChanged -= Refresh;

        if (resetButton != null)
            resetButton.onClick.RemoveListener(ResetResources);
    }
}

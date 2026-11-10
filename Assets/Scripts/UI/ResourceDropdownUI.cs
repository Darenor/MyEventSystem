using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ResourceDropdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown.ClearOptions();

        dropdown.AddOptions(
            Enum.GetNames(typeof(ResourceType)).ToList()
            );
    }

    public ResourceType SelectedResource =>
        (ResourceType)dropdown.value;

    public void ResetSelection()
    {
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }
}

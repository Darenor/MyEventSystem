using deVoid.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoveMenuUI : MonoBehaviour
{
    [SerializeField] private ResourceDropdownUI resourceDropdown;
    [SerializeField] private TMP_InputField amountInput;
    [SerializeField] private Button removeButton;

    private void Awake()
    {
        amountInput.contentType =
            TMP_InputField.ContentType.IntegerNumber;

        removeButton.onClick.AddListener(RemoveResource);
    }

    private void RemoveResource()
    {
        if (!int.TryParse(amountInput.text, out int amount))
            return;

        if (amount <= 0)
            return;

        Signals.Get<RemoveResourceSignal>().Dispatch(
            resourceDropdown.SelectedResource,
            amount);

        resourceDropdown.ResetSelection();
        amountInput.text = "";
    }

    private void OnDestroy()
    {
        if (removeButton != null)
            removeButton.onClick.RemoveListener(RemoveResource);
    }
}

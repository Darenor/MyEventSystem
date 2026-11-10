using deVoid.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddMenuUI : MonoBehaviour
{
    [SerializeField] private ResourceDropdownUI resourceDropdown;
    [SerializeField] private TMP_InputField amountInput;
    [SerializeField] private Button addButton;

    private void Awake()
    {
        amountInput.contentType =
            TMP_InputField.ContentType.IntegerNumber;

        addButton.onClick.AddListener(AddResource);
    }

    private void AddResource()
    {
        if (!int.TryParse(amountInput.text, out int amount))
            return;

        if (amount <= 0)
            return;

        Signals.Get<AddResourceSignal>().Dispatch(
            resourceDropdown.SelectedResource,
            amount);

        ResetFields();
    }

    private void ResetFields()
    {
        resourceDropdown.ResetSelection();
        amountInput.text = "";
    }

    private void OnDestroy()
    {
        if (addButton != null)
            addButton.onClick.RemoveListener(AddResource);
    }
}

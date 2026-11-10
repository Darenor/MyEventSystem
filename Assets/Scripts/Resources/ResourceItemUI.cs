using TMPro;
using UnityEngine;

public class ResourceItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text amountText;

    public void Setup(ResourceType type, int amount)
    {
        nameText.text = type.ToString();
        amountText.text = amount.ToString();
    }
}

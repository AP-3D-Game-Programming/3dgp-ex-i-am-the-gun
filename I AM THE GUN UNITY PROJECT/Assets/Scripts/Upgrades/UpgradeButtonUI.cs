using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButtonUI : MonoBehaviour
{
    [Header("UI References")]
    public Button button;
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public void Setup(Upgrade upgrade, int index, UpgradeSelector selector, PlayerUpgradeManager manager)
    {
        if (titleText != null)
            titleText.text = upgrade.upgradeName;

        if (descriptionText != null)
            descriptionText.text = upgrade.description;

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                selector.SelectUpgrade(index, manager);
            });
        }
    }
}

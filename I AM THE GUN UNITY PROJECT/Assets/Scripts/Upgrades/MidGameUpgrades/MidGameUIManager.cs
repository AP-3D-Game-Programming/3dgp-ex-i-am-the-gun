using UnityEngine;

public class MidGameUIManager : MonoBehaviour
{
    public MidGameUpgradeSelector selector;
    public PlayerUpgradeManager playerManager;

    public UpgradeButtonUI[] upgradeButtons;

    private void Start()
    {
        GameManager.Instance.TogglePause();
        selector.GenerateChoices();

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].Setup(selector.currentChoices[i], i, selector, playerManager);
        }
    }
}

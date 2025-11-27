using UnityEngine;

public class PreGameUIManager : MonoBehaviour
{
    public PreGameUpgradeSelector selector;
    public PlayerUpgradeManager playerManager;

    public UpgradeButtonUI[] upgradeButtons;

    private void Start()
    {
        selector.GenerateChoices();

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].Setup(selector.currentChoices[i], i, selector, playerManager);
        }
    }
}

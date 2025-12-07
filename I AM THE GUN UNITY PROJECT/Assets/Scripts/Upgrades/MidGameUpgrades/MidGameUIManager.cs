using UnityEngine;

public class MidGameUIManager : MonoBehaviour
{
    [Header("Root UI Object (UpgradeMenu)")]
    public GameObject root;
    public MidGameUpgradeSelector selector;
    public PlayerUpgradeManager playerManager;

    public UpgradeButtonUI[] upgradeButtons;

    public void ShowChoices()
{
    root.SetActive(true);

    Time.timeScale = 0f;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    for (int i = 0; i < upgradeButtons.Length; i++)
    {
        upgradeButtons[i].Setup(
            selector.currentChoices[i],
            i,
            selector,
            playerManager
        );
    }
}

public void Hide()
{
    Time.timeScale = 1f;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    root.SetActive(false);
}

}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MidGameUpgradeSelector : UpgradeSelector
{
    public MidGameUpgrade[] allMidGameUpgrades;

    [HideInInspector]
    public MidGameUpgrade[] currentChoices = new MidGameUpgrade[3];

    public MidGameUIManager uiManager;

    private PlayerUpgradeManager playerManager;

    public void GenerateChoices(PlayerUpgradeManager manager)
    {
        playerManager = manager;

        currentChoices = allMidGameUpgrades
            .OrderBy(x => Random.value)
            .Take(3)
            .ToArray();
    }

    public override void SelectUpgrade(int choiceIndex, PlayerUpgradeManager manager)
    {
        if (choiceIndex < 0 || choiceIndex >= currentChoices.Length)
            return;

        MidGameUpgrade chosen = currentChoices[choiceIndex];

        manager.ApplyUpgrade(chosen);

        uiManager.Hide();

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneIndex);
        else
            Debug.Log("No more levels in build settings!");
    }
}

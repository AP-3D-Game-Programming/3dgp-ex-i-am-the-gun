using UnityEngine;
using System.Linq;

public class MidGameUpgradeSelector : UpgradeSelector
{
    public MidGameUpgrade[] allMidGameUpgrades;

    [HideInInspector]
    public MidGameUpgrade[] currentChoices = new MidGameUpgrade[3];

    public void GenerateChoices()
    {
        currentChoices = allMidGameUpgrades.OrderBy(x => Random.value).Take(3).ToArray();

        // TODO: Update UI visuals for the 3 upgrades
    }

    public override void SelectUpgrade(int choiceIndex, PlayerUpgradeManager manager)
    {
        if (choiceIndex < 0 || choiceIndex >= currentChoices.Length) return;

        MidGameUpgrade chosen = currentChoices[choiceIndex];
        manager.ApplyUpgrade(chosen);

        gameObject.SetActive(false);
    }
}

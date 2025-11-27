using UnityEngine;

using System.Linq;
using UnityEngine;

public class PreGameUpgradeSelector : MonoBehaviour
{
    public PreGameUpgrade[] allPreGameUpgrades;

    [HideInInspector]
    public PreGameUpgrade[] currentChoices = new PreGameUpgrade[3];

    public void GenerateChoices()
    {
        currentChoices = allPreGameUpgrades.OrderBy(x => Random.value).Take(3).ToArray();

        // TODO: Update UI visuals for the 3 upgrades
    }

    public void SelectUpgrade(int choiceIndex, PlayerUpgradeManager manager)
    {
        if (choiceIndex < 0 || choiceIndex >= currentChoices.Length) return;

        PreGameUpgrade chosen = currentChoices[choiceIndex];
        manager.ApplyUpgrade(chosen);

        gameObject.SetActive(false);
    }
}

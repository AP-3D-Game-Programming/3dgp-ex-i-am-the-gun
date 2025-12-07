using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    public PlayerCharacter1 PlayerStats;

    private List<MidGameUpgrade> midGameUpgrades = new();

    public void ApplyUpgrade(Upgrade upgrade)
    {
        upgrade.OnApply(this);

        if (upgrade is MidGameUpgrade mid)
            midGameUpgrades.Add(mid);
    }

    private void Update()
    {
        foreach (var u in midGameUpgrades)
            u.OnUpdate(this);
    }

    public void ClearMidGame()
    {
        midGameUpgrades.Clear();
    }
}

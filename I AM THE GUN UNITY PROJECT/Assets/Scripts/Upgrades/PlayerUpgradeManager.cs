using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    // public PlayerStats PlayerStats;
    // public PlayerManager PlayerManager;

    private List<MidGameUpgrade> midGameUpgrades = new List<MidGameUpgrade>();
    private List<PreGameUpgrade> preGameUpgrades = new List<PreGameUpgrade>();

    public void AddUpgrade(Upgrade upgrade)
    {
        //upgrades.Add(upgrade);
        upgrade.OnApply(this);
    }

    private void Update()
    {
        foreach (var u in midGameUpgrades)
            u.OnUpdate(this);
    }
}

using UnityEngine;

public abstract class MidGameUpgrade : Upgrade
{
    public abstract void OnUpdate(PlayerUpgradeManager manager);
}

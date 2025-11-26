using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    public string description;

    public abstract void OnApply(PlayerUpgradeManager manager);
}

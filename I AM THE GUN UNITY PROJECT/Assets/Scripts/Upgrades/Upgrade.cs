using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;

    public abstract void OnApply(PlayerUpgradeManager manager);

    public virtual void OnUpdate(PlayerUpgradeManager manager) { }
}

using UnityEngine;

public abstract class UpgradeSelector : MonoBehaviour
{
    public abstract void SelectUpgrade(int choiceIndex, PlayerUpgradeManager manager);
}

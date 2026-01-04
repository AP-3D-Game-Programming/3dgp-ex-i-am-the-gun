using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/MidGame/Speed Upgrade")]
public class SpeedMidUp : MidGameUpgrade
{
    public float speedIncrease;

    public override void OnApply(PlayerUpgradeManager manager)
    {
        manager.PlayerStats.walkSpeed += speedIncrease;
    }
    
}

using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/MidGame/Coyote Time Upgrade")]
public class CoyoteMidUp : MidGameUpgrade
{
    public float coyoteTimeIncrease;

    public override void OnApply(PlayerUpgradeManager manager)
    {
        manager.PlayerStats.coyoteTime += coyoteTimeIncrease;
    }
}

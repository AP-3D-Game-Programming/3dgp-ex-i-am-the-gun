using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/MidGame/Crouch Speed Upgrade")]
public class CrouchSpMidUp: MidGameUpgrade
{
    public float crouchSpeedIncrease;

    public override void OnApply(PlayerUpgradeManager manager)
    {
        manager.PlayerStats.crouchSpeed += crouchSpeedIncrease;
    }
}

using Mono.Cecil;
using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/MidGame/Gravoity Downgrade")]
public class GravityMidDown:MidGameUpgrade
{
    public float gravityDecrease;

    public override void OnApply(PlayerUpgradeManager manager)
    {
        manager.PlayerStats.gravity += gravityDecrease;
    }
}

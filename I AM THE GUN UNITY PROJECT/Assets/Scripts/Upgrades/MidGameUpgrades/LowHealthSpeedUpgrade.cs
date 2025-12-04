using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/MidGame/Low Health Speed Upgrade")]
public class LowHealthSpeedUpgrade : MidGameUpgrade
{
    private float healthThreshold; // e.g. 0.3f = 30%
    private float speedMultiplier; // e.g. 1.2f = +20%

    private bool active;

    public LowHealthSpeedUpgrade(float threshold, float multiplier)
    {
        healthThreshold = threshold;
        speedMultiplier = multiplier;

        upgradeName = "Low Health Speed Boost";
        description = "Gain speed when HP is low.";
    }

    public override void OnApply(PlayerUpgradeManager manager)
    {
        // Nothing on application
    }

    public override void OnUpdate(PlayerUpgradeManager manager)
    {
        // work ts out
        //float hpRatio = manager.PlayerStats.CurrentHealth / manager.PlayerStats.MaxHealth;

        /*if (!active && hpRatio <= healthThreshold)
        {
            manager.PlayerStats.Speed *= speedMultiplier;
            active = true;
        }
        else if (active && hpRatio > healthThreshold)
        {
            manager.PlayerStats.Speed /= speedMultiplier;
            active = false;
        }*/
    }
}


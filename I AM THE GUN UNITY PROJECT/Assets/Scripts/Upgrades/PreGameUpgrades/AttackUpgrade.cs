using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/PreGame/Attack Upgrade")]
public class AttackUpgrade : PreGameUpgrade
{
    [Header("Stat Modifiers")]
    public int FlatAmount = 0;
    public float Multiplier = 0f;

    public AttackUpgrade(int flat, float mult)
    {
        FlatAmount = flat;
        Multiplier = mult;
        upgradeName = "Attack Upgrade";
        if (FlatAmount != 0 && Multiplier != 0f)
            description = $"Increases your attack power by {FlatAmount} and adds a multiplier of +{Multiplier}.";
        else if (FlatAmount != 0)
            description = $"Increases your attack power by {FlatAmount}.";
        else if (Multiplier != 0f)
            description = $"Adds an attack multiplier of +{Multiplier}.";
        else
        description = "No Effect.";
    }

    public override void OnApply(PlayerUpgradeManager manager)
    {
        if (FlatAmount != 0)
        ;
            //manager.PlayerStats.AttackDamage += flatAmount;

        if (Multiplier != 0f)
        ;
            //manager.PlayerStats.AttackDamageMultiplier *= multiplier;
    }
}

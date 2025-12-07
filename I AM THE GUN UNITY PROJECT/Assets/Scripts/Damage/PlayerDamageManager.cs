using UnityEngine;

public class PlayerDamageManager : DamageManager
{
    protected override void Die()
    {
        Debug.Log("PLAYER DEAD!");
        // TODO: Respawn, reload level, show death UI, etc.
    }
}


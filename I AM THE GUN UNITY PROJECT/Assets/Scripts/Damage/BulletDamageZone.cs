using UnityEngine;

public class BulletDamageZone : MonoBehaviour
{
    public int damageAmount = 5;

    private void OnTriggerEnter(Collider other)
    {
        DamageManager dmg = other.GetComponent<DamageManager>();
        if (dmg != null)
        {
            dmg.TakeDamage(damageAmount);
        }
    }
}

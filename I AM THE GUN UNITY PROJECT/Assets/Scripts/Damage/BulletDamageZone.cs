using UnityEngine;

// damaging zone on a plane, NOT BULLET ITSELF
public class BulletDamageZone : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        DamageManager dmg = other.GetComponent<DamageManager>();
        if (dmg != null)
        {
            dmg.TakeDamage(damageAmount);
        }
    }
}

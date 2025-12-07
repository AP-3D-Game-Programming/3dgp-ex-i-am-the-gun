using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Attached Gun = Health Source")]
    public Gun gun;

    [Header("Damage Settings")]
    public int damagePerHit = 1;

    protected virtual void Awake()
    {
        if (gun == null)
            gun = GetComponent<Gun>();
    }

    public virtual void TakeDamage(int amount)
    {
        if (gun == null)
        {
            Debug.LogError($"{name} has no Gun assigned but uses ammo-as-health!");
            return;
        }

        gun.BulletCount -= amount;

        if (gun.BulletCount <= 0)
        {
            gun.BulletCount = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{name} has died.");
        Destroy(gameObject);
    }
}

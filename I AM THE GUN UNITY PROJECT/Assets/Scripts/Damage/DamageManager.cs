using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Attached Gun = Health Source")]
    public Gun gun;

    public GameObject Weapon; 
    private bool isDead;

    public virtual void Awake()
    {
        if (gun == null)
            gun = GetComponent<Gun>();
    }
    void Update()
    {
        if (!isDead && gun != null && gun.BulletCount <= 0)
        {
            isDead = true;
            gun.BulletCount = 0;
            Die();
        }
    }

    public virtual void TakeDamage(int amount)
    {
        if (gun == null)
        {
            Debug.LogError($"{name} has no Gun assigned but uses ammo as health!");
            return;
        }

        gun.BulletCount -= amount;
    }

    protected virtual void Die()
    {
        Debug.Log($"{name} has died.");
        // make it drop its weapon (please god, please work)
        Instantiate(Weapon, transform.position, Quaternion.identity);
        // destroy enemy :skull_emoji:
        Destroy(gameObject);
    }
}

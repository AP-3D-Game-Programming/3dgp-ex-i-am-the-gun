using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Attached Gun = Health Source")]
    public Gun gun;

    public GameObject Weapon; 

    protected virtual void Awake()
    {
        if (gun == null)
            gun = GetComponent<Gun>();
    }

    public virtual void TakeDamage(int amount)
    {
        if (gun == null)
        {
            Debug.LogError($"{name} has no Gun assigned but uses ammo as health!");
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
        // make it drop its weapon (please god, please work)
        Weapon = GameObject.FindWithTag("Weapon"); // all gun prefabs normally have this 
        Instantiate(Weapon, transform.position, Quaternion.identity);
        // destroy enemy :skull_emoji:
        Destroy(gameObject);
    }
}

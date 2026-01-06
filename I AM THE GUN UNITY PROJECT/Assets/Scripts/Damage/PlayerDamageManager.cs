using UnityEngine;

public class PlayerDamageManager : DamageManager
{
    public UseWeapon useWeapon;
    public bool isDead;

    public DeathScreen deathScreen;
    private bool isReloading = false;  // prevent reload spam

    public override void Awake()
    {
        base.Awake();
        CacheWeapon();
    }

    void Update()
    {
        CacheWeapon();

        if (isDead)
            return;

        // If bullets empty but have cartridges, reload once
        if (gun != null && gun.BulletCount <= 0 && useWeapon.cartridgesCount > 0 && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }

        // If no ammo and no cartridges, die
        if (gun != null && gun.BulletCount <= 0 && useWeapon.cartridgesCount <= 0)
        {
            Die();
        }
    }

    void CacheWeapon()
    {
        if (useWeapon != null && useWeapon.Weapon != null)
        {
            gun = useWeapon.Weapon.GetComponent<Gun>();
            Weapon = useWeapon.Weapon;
        }
    }

    public override void TakeDamage(int amount)
    {
        if (isDead)
            return;

        if (gun != null)
        {
            gun.BulletCount -= amount;
            gun.BulletCount = Mathf.Max(gun.BulletCount, 0);
            Debug.Log($"{name} took {amount} damage! Bullets left: {gun.BulletCount}");
        }
        else
        {
            Debug.LogWarning($"{name} has no gun assigned, ignoring damage.");
        }
    }

    private System.Collections.IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        // Optional: add reload delay here with yield return new WaitForSeconds(x);

        useWeapon.cartridgesCount--;
        gun.BulletCount = gun.BulletCapacity;
        Debug.Log($"Reloaded! Cartridges left: {useWeapon.cartridgesCount}, Bullets refilled to: {gun.BulletCount}");

        isReloading = false;

        yield return null;
    }

    protected override void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("PLAYER DEAD!");
        DropWeapon();
        // Assume deathScreen.Show() is called elsewhere or you add it here
        deathScreen.Show();
    }

    void DropWeapon()
    {
        if (Weapon != null)
        {
            Instantiate(Weapon, transform.position, Quaternion.identity);
        }
    }

    public void TriggerDeath()
    {
        Die();
    }
}

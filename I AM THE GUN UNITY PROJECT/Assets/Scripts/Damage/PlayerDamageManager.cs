using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageManager : DamageManager
{
    public PlayerUpgradeManager upgradeManager;
    public UseWeapon useWeapon;
    private bool isDead;
    public DeathScreen deathScreen;

    public override void Awake()
    {
        CacheWeapon();
    }

    void Update()
    {
        // testing only
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }

        CacheWeapon();

        if (!isDead && gun != null && gun.BulletCount <= 0)
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

    protected override void Die()
    {
        if (isDead) return;
        isDead = true;

        // get the game over screen in here
        Debug.Log("PLAYER DEAD!");

        DropWeapon();
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

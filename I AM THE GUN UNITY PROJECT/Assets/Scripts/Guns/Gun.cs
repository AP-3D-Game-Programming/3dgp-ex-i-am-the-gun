using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float BulletVelocity = 30;
    public float BulletPrefabLifeTime = 3f;
    public int BulletCapacity;
    public int BulletCount;

    public Animator gunAnimation;

    public GameObject gun;

    [SerializeField] private Text AmmoCount;
    [SerializeField] private Text AmmoCap;
    [SerializeField] private float kickbackForce = 10f; 
    private void Start()
    {
        BulletCount = BulletCapacity;
    }
    private void Update()
    {
        AmmoCap.text = BulletCapacity.ToString();
        AmmoCount.text = BulletCount.ToString();

    }

    public void FireWeapon()
    {
        if (BulletCount <= 0) return;

        // Instantiate the bullet
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, transform.rotation);

        // Assign shooter to avoid self-damage
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.shooter = gameObject;

        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward.normalized * BulletVelocity, ForceMode.Impulse);
        StartCoroutine(GunRecoil());

        // Apply kickback to the player
        Player1 player = GetComponentInParent<Player1>();
        if (player != null)
        {
            player.ApplyKickback(kickbackForce);
        }

        // Decrease ammo
        BulletCount--;

        // Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, BulletPrefabLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    IEnumerator GunRecoil()
    {
        gun.GetComponent<Animator>().Play("GunRecoil");
        yield return new WaitForSeconds(0.20f);
        gun.GetComponent<Animator>().Play("New State");
    }
}

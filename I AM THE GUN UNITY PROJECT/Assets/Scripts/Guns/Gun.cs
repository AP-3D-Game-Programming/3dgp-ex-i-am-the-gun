using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject muzzleFlash;
    public AudioSource gunShot;
    public AudioClip emptyMag;

    public bool IsPlayerGun = false;

    public GameObject gun;

    [SerializeField] public float kickbackForce = 10f; 
    public float fireRate = 10f;
    protected float nextFireTime;

    //AmmoUI
    [SerializeField] private GameObject bulletUiPrefab;
    [SerializeField] private Transform ammoUiContainer;
    private List<GameObject> bulletUiStack = new List<GameObject>();

    private void Awake()
    {
        gun = gameObject;
    }
    
    private void Update()
    {
        if (!IsPlayerGun) return;
    }

    public virtual void FireWeapon()
    {
        if (!CanFire()) return;

        nextFireTime = Time.time + 1f / fireRate;

        if (BulletCount <= 0)
        {
            gunShot.PlayOneShot(emptyMag);
            return;
        }

        // Instantiate the bullet
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, transform.rotation);

        // Assign shooter to avoid self-damage
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.shooter = gameObject;

        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward.normalized * BulletVelocity, ForceMode.Impulse);
        StartCoroutine(GunRecoil());
        StartCoroutine(MuzzleFlash());
        gunShot.PlayOneShot(gunShot.clip);

        // Apply kickback to the player
        Player1 player = GetComponentInParent<Player1>();
        if (player != null)
        {
            player.ApplyKickback(kickbackForce);
        }

        // Decrease ammo
        BulletCount--;
        RemoveAmmoUI();

        // Destroy the bullet after some time
        StartCoroutine(DestroyBulletAfterTime(bullet, BulletPrefabLifeTime));
    }

    public void AddAmmoUI()
    {
        ClearAmmoUI();
        for (int i = 0; i < BulletCount; i++)
        {
            GameObject bulletUI = Instantiate(bulletUiPrefab, ammoUiContainer);
            bulletUiStack.Add(bulletUI);
        }
        
    }

    public void RemoveAmmoUI()
    {
        if (bulletUiStack.Count == 0) return;

        GameObject bullet = bulletUiStack[bulletUiStack.Count - 1];
        bulletUiStack.RemoveAt(bulletUiStack.Count - 1);
        Destroy(bullet);
    }

    public void ClearAmmoUI()
    {
        foreach (GameObject bullet in bulletUiStack)
        {
            Destroy(bullet);
        }
        bulletUiStack.Clear();
    }

    public IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public IEnumerator GunRecoil()
    {
        gun.GetComponent<Animator>().Play("GunRecoil");
        yield return new WaitForSeconds(0.20f);
        gun.GetComponent<Animator>().Play("New State");
    }

    public IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
    }

    protected bool CanFire()
    {
        return Time.time >= nextFireTime;
    }

    public void OnEquippedByPlayer(UseWeapon owner)
    {
        ClearAmmoUI();
        IsPlayerGun = true;

        if (ammoUiContainer == null)
        {
            GameObject container = GameObject.FindWithTag("AmmoStack");
            if (container != null)
            {
                ammoUiContainer = container.transform;
            }
        }

        BulletCount = BulletCapacity;
        AddAmmoUI();
    }
}

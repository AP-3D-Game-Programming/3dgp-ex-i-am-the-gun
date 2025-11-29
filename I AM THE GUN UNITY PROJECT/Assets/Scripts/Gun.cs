using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float BulletVelocity = 30;
    public float BulletPrefabLifeTime = 3f;
    public int BulletCapacity;
    public int BulletCount;
    private void Start()
    {
        BulletCount = BulletCapacity;
    }

    public void FireWeapon()
    {
        if (BulletCount <= 0) return;

        // Instantiate the bullet
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);

        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward.normalized * BulletVelocity, ForceMode.Impulse);

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
}

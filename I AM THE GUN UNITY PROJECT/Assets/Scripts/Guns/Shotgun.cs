using UnityEngine;

public class Shotgun : Gun
{
    public int pelletCount = 7;
    public float spreadAngle = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FireWeapon()
    {
        if (BulletCount <= 0) return;

        for (int i = 0; i < pelletCount; i++)
        {
            GameObject pellet = Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
            Debug.Log(pellet);

            Vector3 spread = new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            );
            Vector3 direction = BulletSpawn.forward + spread * 0.01f;

            pellet.GetComponent<Rigidbody>().AddForce(direction.normalized * BulletVelocity, ForceMode.Impulse);

            StartCoroutine(DestroyBulletAfterTime(pellet, BulletPrefabLifeTime));
        }

        Player1 player = GetComponentInParent<Player1>();
        if (player != null)
        {
            player.ApplyKickback(kickbackForce);
        }

        BulletCount--;

        StartCoroutine(GunRecoil());
        StartCoroutine(MuzzleFlash());
    }
}

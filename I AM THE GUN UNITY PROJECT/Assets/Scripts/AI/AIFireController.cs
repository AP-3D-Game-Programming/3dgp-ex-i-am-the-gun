using UnityEngine;

public class AIFireController : MonoBehaviour
{
    public Gun gun;
    public Transform target;

    public float fireCooldown = 0.5f;
    private float nextFireTime;

    public void FireAtTarget()
    {
        if (Time.time < nextFireTime)
            return;
        if (gun == null || target == null)
            return;

        // Aim
        Vector3 dir = (target.position - gun.BulletSpawn.position).normalized;
        gun.BulletSpawn.rotation = Quaternion.LookRotation(dir);

        gun.FireWeapon();

        nextFireTime = Time.time + fireCooldown;
    }
}

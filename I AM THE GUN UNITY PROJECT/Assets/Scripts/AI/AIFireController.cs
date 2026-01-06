using UnityEngine;
using UnityEngine.AI;

public class AIFireController : MonoBehaviour
{
    public Gun gun;
    public Transform target;
    public Transform aimPivot;
    public float aimSpeed = 15f;
    public float fireCooldown = 0.5f;

    private float nextFireTime;
    private NavMeshAgent agent;
    private Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (rb != null) rb.isKinematic = true;
    }

    void Update()
    {
        if (target != null)
        {
            if (agent != null) agent.updateRotation = false;
            AimAtTarget();
        }
        else
        {
            if (agent != null) agent.updateRotation = true;
        }
    }

    public void AimAtTarget()
    {
        if (target == null || aimPivot == null) return;

        //Calculate direction
        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - aimPivot.position;
        dir.y = 0;

        if (dir.sqrMagnitude < 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(dir);

        //Smoothly rotate
        aimPivot.rotation = Quaternion.Slerp(
            aimPivot.rotation,
            targetRotation,
            Time.deltaTime * aimSpeed
        );

        //FORCE the root to match if aimPivot is a child, 
        //ensuring the "legs" eventually face you too.
        if (aimPivot != transform)
        {
            transform.rotation = Quaternion.Slerp(
               transform.rotation,
               targetRotation,
               Time.deltaTime * (aimSpeed * 0.5f)
           );
        }
    }

    public void FireAtTarget()
    {
        if (Time.time < nextFireTime || gun == null || target == null) 
            return;

        Vector3 dir = (target.position - gun.BulletSpawn.position).normalized;
        gun.BulletSpawn.rotation = Quaternion.LookRotation(dir);

        gun.FireWeapon();
        nextFireTime = Time.time + fireCooldown;
    }
}
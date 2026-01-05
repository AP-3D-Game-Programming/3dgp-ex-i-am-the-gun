using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player;

    public Transform playerLocation;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Attacking
    public Gun enemyGun;
    public float fireCooldown = 1.2f;
    private float nextFireTime;

    public Transform gunPivot;

    private void Awake()
    {
        agent.updateRotation = false;
        player = GameObject.Find("Player1");
        playerLocation = player.transform;
        agent = GetComponent<NavMeshAgent>();

        enemyGun = GetComponentInChildren<Gun>();

        enemyGun.IsPlayerGun = false;

        gunPivot = enemyGun.BulletSpawn;

    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && HasLineOfSight() && InFOV()) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(playerLocation.position);

        Vector3 dir = playerLocation.position - gunPivot.position;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(dir),
            Time.deltaTime * 6f
        );

        AimAtPlayer();
    }

    private void AttackPlayer()
{
    // Stop moving
    agent.SetDestination(transform.position);

    // Look at player (Y locked)
    Vector3 lookDir = playerLocation.position - transform.position;
    lookDir.y = 0;
    Quaternion targetRot = Quaternion.LookRotation(lookDir);
    transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRot,
        Time.deltaTime * 8f
    );

    AimAtPlayer();

    // Fire weapon with cooldown
    if (Time.time >= nextFireTime)
    {
        enemyGun.FireWeapon();
        nextFireTime = Time.time + fireCooldown;
    }
}

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void AimAtPlayer()
{
    Vector3 dir = playerLocation.position - gunPivot.position;
    gunPivot.rotation = Quaternion.LookRotation(dir);
}

bool HasLineOfSight()
{
    Vector3 origin = gunPivot.position;
    Vector3 dir = (playerLocation.position - origin).normalized;

    if (Physics.Raycast(origin, dir, out RaycastHit hit, attackRange))
    {
        return hit.transform == playerLocation;
    }
    return false;
}

bool InFOV()
{
    Vector3 dirToPlayer = (playerLocation.position - transform.position).normalized;
    return Vector3.Angle(transform.forward, dirToPlayer) < 60f;
}



}

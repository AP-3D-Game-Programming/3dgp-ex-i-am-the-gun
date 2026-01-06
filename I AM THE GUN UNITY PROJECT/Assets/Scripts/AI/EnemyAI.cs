using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player;

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
        player = GameObject.Find("Character1");
        agent = GetComponent<NavMeshAgent>();

        enemyGun = GetComponentInChildren<Gun>();

        enemyGun.IsPlayerGun = false;

        gunPivot = enemyGun.BulletSpawn;

    }

    private void Update()
    {
        //Check for sight and attack range
        player = GameObject.Find("Character1");
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }
   private void Patroling()
{
    if (!walkPointSet) SearchWalkPoint();

    if (walkPointSet)
    {
        agent.SetDestination(walkPoint);
        RotateTowards(walkPoint, 4f); // slower rotation while patrolling
    }

    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    if (distanceToWalkPoint.magnitude < 1f)
        walkPointSet = false;
}


private void ChasePlayer()
{
    agent.SetDestination(player.transform.position);

    // Rotate toward movement direction (so it faces where it’s walking)
    Vector3 velocity = agent.desiredVelocity;
    velocity.y = 0;
    if (velocity != Vector3.zero)
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * 8f);

    AimAtPlayer();
}



   private void AttackPlayer()
{
    // Stop moving
    agent.SetDestination(transform.position);

    // Turn faster to fully face the player
    RotateTowards(player.transform.position, 12f); // fast rotation while attacking

    // Aim gun (can tilt up/down)
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
    Vector3 dir = player.transform.position - gunPivot.position + new Vector3(0, 1.5f, 0);
    gunPivot.rotation = Quaternion.LookRotation(dir);
}

private void RotateTowards(Vector3 target, float speed = 6f)
{
    Vector3 dir = target - transform.position;
    dir.y = 0; // lock Y rotation
    if (dir == Vector3.zero) return;
    Quaternion targetRot = Quaternion.LookRotation(dir);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * speed);
}

}

using UnityEngine;
using UnityEngine.AI;
public class RangedEnemy : Enemy
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    // public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    // public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [SerializeField] private Transform _shootingPos;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isDead) return;
        if (_isPaused) return;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, _data.VisionRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, _data.AttackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
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
        float randomZ = Random.Range(-_data.WalkPointRange, _data.WalkPointRange);
        float randomX = Random.Range(-_data.WalkPointRange, _data.WalkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            GameObject newBullet = _bulletsPool.GetPooledObject();
            if (!newBullet) return;

            newBullet.transform.position = _shootingPos.position;
            newBullet.transform.rotation = transform.rotation;

            if (newBullet.TryGetComponent(out Bullets bullet))
            {
                bullet.SetData(_data.Weapon.Damage, _data.Weapon.AmmoSpeed, _data.Weapon.AmmoType);
                newBullet.SetActive(true);
                bullet.Shoot(_data.Weapon.Accuracy.x, _data.Weapon.Accuracy.y);
                alreadyAttacked = true;
            }

            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 64f, ForceMode.Impulse);
            // rb.AddForce(transform.up * Random.Range(8f, -2f), ForceMode.Impulse);
            // rb.AddForce(transform.right * Random.Range(5f, -5f), ForceMode.Impulse);

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //public void TakeDamage(int damage)
    //{
    //health -= damage;

    //if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    //}
    // private void DestroyEnemy()
    // {
    //     Destroy(gameObject);
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _data.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _data.VisionRange);
    }

}


using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform coop;

    public LayerMask whatIsGround, whatIsPlayer, whatIsCoop;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange, coopRange;
    public bool playerInSightRange, playerInAttackRange, coopInRange;

    public Animator enemyAnimation;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        coop = GameObject.FindGameObjectWithTag("HenCoop").transform;
        agent.speed = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henSpeed;
        sightRange = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henSightRange;
        walkPointRange = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henWalkPointRange;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange,whatIsPlayer);
        coopInRange = Physics.CheckSphere(transform.position, coopRange, whatIsCoop);


      //  if (GameManager.instance.gameWon) EnterCoop();
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange ) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();


    }

    private void Patroling()
    {
      
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
        enemyAnimation.SetBool("chase", false);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float X, Z;
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        X = transform.position.x + randomX;
        Z = transform.position.z + randomZ;
        if(X < -7)
            X = -6;
        if(X > 28)
            X = 27;
        if(Z < -27)
            Z = -26;
        if(Z > 28)
            Z = 27;


        walkPoint = new Vector3(X, transform.position.y, Z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f,whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {  
            agent.SetDestination(player.position);
            enemyAnimation.SetBool("chase", true);
              
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            enemyAnimation.SetBool("chase", false);
            enemyAnimation.SetBool("attack", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void EnterCoop() 
    {
        agent.SetDestination(coop.position);
        sightRange = 100f;
     
        enemyAnimation.SetBool("chase", true);
      //  transform.LookAt(coop);
        Debug.Log("Going to coop");

        if (coopInRange) 
        {
            Destroy(gameObject);
        }
    }
    private void ResetAttack()
    {
        //FoxAnimation.SetBool("attack", false);
        alreadyAttacked = false;
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
        Gizmos.DrawLine(transform.position, walkPoint);
        Gizmos.DrawLine(transform.position, player.position);

    }
}

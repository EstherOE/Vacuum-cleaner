using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform enemy;

    public LayerMask whatIsGround, whatIsPlayer, whatIsEnemy;

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
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator chickAnimation;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log(enemy.name);
    }

    private void Start()
    {
        if(GameManager.instance.gameLevel[GameManager.instance.currentLevelId].henCount > 0)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        }
        agent.speed = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickSpeed;
        sightRange = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickSightRange;
        walkPointRange = GameManager.instance.gameLevel[GameManager.instance.currentLevelId].chickWalkpointRange;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
      

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) RunAway();
        if (playerInAttackRange && playerInSightRange) ResetChick();
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
        chickAnimation.SetBool("chase", false);
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

    private void RunAway()
    {
        agent.SetDestination(enemy.position);
        chickAnimation.SetBool("chase", true);
    }

    private void ResetChick()
    {
        //Make sure enemy doesn't move

        agent.SetDestination(transform.position);

        transform.LookAt(enemy);

        if (!alreadyAttacked)
        {

            alreadyAttacked = true;
            chickAnimation.SetBool("chase", false);
            //chickAnimation.SetBool("attack", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        //chickAnimation.SetBool("attack", false);
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, walkPoint);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, enemy.position);
    }
}

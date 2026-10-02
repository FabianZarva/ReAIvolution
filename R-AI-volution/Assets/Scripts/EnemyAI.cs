using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float sightRange = 5f;
    [SerializeField] private float walkPointRange = 5f;

    private NavMeshAgent agent;
    [SerializeField]  private Transform player;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;

    private int health = 5;
    private LayerMask whatIsGround;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        whatIsGround = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        //Check if the enemy is close enough to the player to start chasing
        if (Vector3.Distance(transform.position, player.position) < sightRange && walkPointSet == false)
        {
            ChasePlayer();
        }
        //Check if the enemy is not close enough to the player and should wander
        else if (walkPointSet == false)
        {
            if (Vector3.Distance(transform.position, walkPoint) < 1f)
                SearchWalkPoint();
            else
                WalkToWalkPoint();
        }

        //Check if the enemy is close enough to the player to attack
        if (Vector3.Distance(transform.position, player.position) < attackRange && !alreadyAttacked)
        {
            alreadyAttacked = true;
            
            Invoke(nameof(ResetAttack), 1f);
        }
    }

    void WalkToWalkPoint()
    {
        agent.SetDestination(walkPoint);

        //Calculate the distance to the walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) DestroyEnemy();
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{
	private NavMeshAgent agent;

	public Transform player;
	
	public Transform mazeRenderer;
	private int mazeSize;		//Refference to maze size
	private float width;		//Refference to wall width

	public LayerMask playerMask;
	public LayerMask obstacleMask;

	public float health;

	//Patroling
	public Vector3 walkPoint;
	bool walkPointSet;

	//Attacking
	public float timeBetweenAttacks;
	bool alreadyAttacked;

	//States
	public float sightRange;
	public float attackRange;
	
	[SerializeField]
	private bool playerInSightRange;
	[SerializeField]
	private bool playerInAttackRange;
	[SerializeField]
	private bool hitWall;
	
	private void Awake(){
        //player = GameObject.Find("Player").transform;
        agent = this.GetComponent<NavMeshAgent>();
		mazeSize = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;	//Maze size
		width = mazeRenderer.GetComponent<MazeRenderer>().size;			//Wall width

	}
	
	private void Update(){
		//Check for sight and attack range
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);		//Range of minoteur
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);		//Range of minotaru's attack
		
		Vector3 directionToTarget = (player.position - transform.position).normalized;				//Direction from minotaur to player
		float distanseToPlayer = Vector3.Distance (player.position, transform.position);			//Distance between minotaur and player
		
		hitWall = Physics.Raycast(transform.position, directionToTarget, distanseToPlayer, obstacleMask);	//Check if there is a wall between player and minotaur
		Debug.DrawRay(transform.position, directionToTarget * distanseToPlayer, Color.blue);		//Visualization of RayCast
		
		if (!playerInSightRange && !playerInAttackRange){		//If player outside range, patrol
			Patroling();
		}
		
		if (playerInSightRange && !playerInAttackRange){		//If player in range
			if(!hitWall){
				ChasePlayer();									//If minotaur sees player, chase else patrol
			}else{
				Patroling();
			}
		}
		
		if (playerInAttackRange && playerInSightRange){		//If in range to attack
			if(!hitWall){
				AttackPlayer();								//If minotaur sees player, Attack else patrol
			}else{
				Patroling();
			}
		}
	}
	
	#region Patrolling Code
 
    private void Patroling(){
        if (!walkPointSet)
			SearchWalkPoint();

        if (walkPointSet)
			agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
			walkPointSet = false;
    }
	
    private void SearchWalkPoint(){
        //Calculate random point in range
        float randomX = Random.Range( -(mazeSize/2), mazeSize/2 ) * width;
        float randomZ = Random.Range( -(mazeSize/2), mazeSize/2 ) * width;

        walkPoint = new Vector3( randomX, transform.position.y, randomZ);
		walkPointSet = true;
    }
	
	#endregion

	private void ChasePlayer(){	
		walkPoint = player.position;		//Making last seen position of player the walk point
		agent.SetDestination(walkPoint);
	}

	#region Attack Code

    private void AttackPlayer(){
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked){
			
            ///Attack code here
            Debug.Log("Pew paw bam");
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
	
    private void DestroyEnemy(){
        Destroy(gameObject);
    }
	
	#endregion

	private void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

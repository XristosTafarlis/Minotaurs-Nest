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
	private bool seeWalls;
	
	private void Awake(){
        //player = GameObject.Find("Player").transform;
        agent = this.GetComponent<NavMeshAgent>();
		mazeSize = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;	//Maze size
		width = mazeRenderer.GetComponent<MazeRenderer>().size;			//Wall width

	}
	
	private void Update(){
		//Check for sight and attack range
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
		
		Vector3 directionToTarget = (player.position - transform.position).normalized;
		seeWalls = Physics.Raycast(transform.position, directionToTarget, sightRange * 10, obstacleMask);
		Debug.DrawRay(transform.position, directionToTarget * sightRange * 10, Color.blue);
		
		if (!playerInSightRange && !playerInAttackRange){
			Patroling();
		}
		
		if (playerInSightRange && !playerInAttackRange){
			if(!seeWalls){
				ChasePlayer();
			}else{
				Patroling();
			}
		}
		
		if (playerInAttackRange && playerInSightRange){
			if(!seeWalls){
				AttackPlayer();	
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

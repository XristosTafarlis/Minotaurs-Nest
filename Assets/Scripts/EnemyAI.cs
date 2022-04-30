using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{
	private NavMeshAgent agent;
	
	[Header("Transforms")]
	[SerializeField] Transform player;
	[SerializeField] Transform mazeRenderer;
	
	int mazeSize;		//Refference to maze size
	float width;		//Refference to wall width

	[Header("Layers")]
	[SerializeField] LayerMask playerMask;
	[SerializeField] LayerMask obstacleMask;

	//Patroling
	[Header("Patroling")]
	[SerializeField] Vector3 walkPoint;
	bool walkPointSet;
	
	[Space(10)]
	[SerializeField] float health;
	
	//Attacking
	[Header("Attacking")]
	[SerializeField] float timeBetweenAttacks;
	bool alreadyAttacked;
	
	//States
	[Header("States")]
	[SerializeField] float sightRange;
	[SerializeField] float attackRange;
	
	[SerializeField] bool playerInSightRange;	
	[SerializeField] bool playerInAttackRange;
	
	[SerializeField] bool hitWall;
	
	[Space(20)]
	[Range(0.5f, 5f)]
	[SerializeField] float smoothLookSpeed = 2f;
	Coroutine LookCoroutine;
	
	private void Awake(){
        agent = this.GetComponent<NavMeshAgent>();
		mazeSize = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;		//Maze size
		width = mazeRenderer.GetComponent<MazeRenderer>().size;				//Wall width

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
				ChasePlayer();									//If minotaur sees player, chase 
			}else{												//Else patrol
				Patroling();
			}
		}
		
		if (playerInAttackRange && playerInSightRange){		//If in range to attack
			if(!hitWall){
				AttackPlayer();								//If minotaur sees player, attack
			}else{											//Else patrol
				Patroling();
			}
		}
	}
	
	#region Minotaur Look At Player
	public void StartRotating(){
		if (LookCoroutine != null){
			StopCoroutine(LookCoroutine);
		}

		LookCoroutine = StartCoroutine(LookAt());
	}

	private IEnumerator LookAt(){
		Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);

		float timeBetweenAttacks = 0;

		Quaternion initialRotation = transform.rotation;
		while (timeBetweenAttacks < 1){
			transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, timeBetweenAttacks);
			
			timeBetweenAttacks += Time.deltaTime * smoothLookSpeed;

			yield return null;
		}
	}	
	#endregion
	
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
        //agent.SetDestination(transform.position);
		agent.velocity = Vector3.zero;

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

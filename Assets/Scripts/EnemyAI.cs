using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{
	//Refferences to minotaur's components
	NavMeshAgent agent;
	Animator anim;
	
	int mazeSize;		//Refference to maze size
	float width;		//Refference to wall width
	
	[Header("Refferences")]
	[SerializeField] Transform player;
	[SerializeField] Transform mazeRenderer;
	[SerializeField] AudioSource playerChaseSoundRefference;
	
	[Header("Layers")]
	[SerializeField] LayerMask playerMask;
	[SerializeField] LayerMask obstacleMask;
	
	//Patroling
	Vector3 walkPoint;
	bool walkPointSet;
	
	[Header("Minotar's stats")]
	[SerializeField] float health;
	[SerializeField] float chaseSpeed;
	[SerializeField] float walkSpeed;
	[SerializeField] [Range(0.5f, 10f)] float smoothLookSpeed = 2f;
	
	//Attacking
	[Header("Attack Properties")]
	[SerializeField] [Range( 10, 100)] int minotaurDamage;
	[SerializeField] float attackRadius = 0.35f;
	[SerializeField] float timeBetweenAttacks;
	[SerializeField] Transform attackPoint;
	[SerializeField] LayerMask playerLayer;
	bool alreadyAttacked;
	
	//States
	[Header("States")]
	[SerializeField] float sightRange;
	[SerializeField] float attackRange;
	
	[Header("Debug States <View only> ")]
	[SerializeField] bool playerInSightRange;	
	[SerializeField] bool playerInAttackRange;
	[SerializeField] bool minotaurSeesWall;
	
	[Space(20)]
	
	Coroutine LookCoroutine;
	
	private void Awake(){
		anim = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();
		mazeSize = mazeRenderer.GetComponent<MazeRenderer>().mazeSize;		//Maze size
		width = mazeRenderer.GetComponent<MazeRenderer>().size;				//Wall width
	}
	
	private void Start() {
		agent.speed = walkSpeed;
	}
	
	private void Update(){
		//Check for sight and attack range
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);		//Range of minoteur
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);		//Range of minotaru's attack
		
		Vector3 directionToTarget = (player.position - transform.position).normalized;				//Direction from minotaur to player
		float distanseToPlayer = Vector3.Distance (player.position, transform.position);			//Distance between minotaur and player
		
		minotaurSeesWall = Physics.Raycast(transform.position, directionToTarget, distanseToPlayer, obstacleMask);	//Check if there is a wall between player and minotaur
		Debug.DrawRay(transform.position, directionToTarget * distanseToPlayer, Color.blue);		//Visualization of RayCast
		
		if (!playerInSightRange && !playerInAttackRange){		//If player outside range, patrol
			Patroling();
		}
		
		if (playerInSightRange && !playerInAttackRange){		//If player in range
			if(!minotaurSeesWall){
				ChasePlayer();									//If minotaur sees player, chase
			}else{												//Else patrol
				Patroling();
			}
		}
		
		if (playerInAttackRange && playerInSightRange){		//If in range to attack
			if(!minotaurSeesWall){
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
		anim.SetBool("isRunning", false);
		agent.speed = walkSpeed;

		if(playerChaseSoundRefference.isPlaying){
			StartCoroutine(ChaseSoundFadeOut(playerChaseSoundRefference, 300));
		}
		
		if (!walkPointSet)
			SearchWalkPoint();
		
		if (walkPointSet){
			if(agent.isActiveAndEnabled){
				agent.SetDestination(walkPoint);
			}
		}

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
	
	#region Chase Code
	
	private void ChasePlayer(){
		
		if(!playerChaseSoundRefference.isPlaying){
			playerChaseSoundRefference.volume = 1;
			playerChaseSoundRefference.Play();
		}
		
		anim.SetBool("isRunning", true);
		agent.speed = chaseSpeed;
		walkPoint = player.position;		//Making last seen position of player the walk point
		if(agent.enabled == true)
			agent.SetDestination(walkPoint);
	}
	
	#endregion
	
	#region Attack Code
	
	private void AttackPlayer(){
		//Make sure enemy doesn't move
		agent.SetDestination(transform.position);
		//agent.velocity = Vector3.zero;
	
		if (!alreadyAttacked){
			anim.SetTrigger("Attack");
			
			Collider[] playerCollider = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);	//Getting the colliders inside damage range
			foreach(var Col in playerCollider){
				if(Col != null){
					playerCollider[0].GetComponent<PlayerScript>().PlayerTakeDamage(minotaurDamage);	//Damage the player
					Debug.Log("Player hitt");
				}
			}
			
			alreadyAttacked = true;
			Invoke("ResetAttack", timeBetweenAttacks);
		}
	}
	
	private void ResetAttack(){
		alreadyAttacked = false;
	}
	
	#endregion
	
	#region Get Attacked
	
	public void TakeDamage(int damage){
		agent.SetDestination(transform.position);
		//agent.velocity = Vector3.zero;
		
		//Damage sound
		gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.1f);
		gameObject.GetComponent<AudioSource>().volume = Random.Range(0.4f, 0.5f);
		gameObject.GetComponent<AudioSource>().Play();
		
		health -= damage;
		if (health <= 0){
			
			agent.enabled = false;
			Invoke("DestroyEnemy", 2f);
			anim.SetTrigger("isDead");
			return;
		}
		anim.SetTrigger("isAttacked");
	}
	
	private void DestroyEnemy(){
		Destroy(gameObject);
	}
	
	#endregion

	private IEnumerator ChaseSoundFadeOut(AudioSource audioSource, float FadeTime) {
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		audioSource.Stop();
	}
	
	private void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, sightRange);
		
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
	}
}
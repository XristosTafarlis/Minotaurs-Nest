using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour{
	
	[Header("Refferences")]
	[SerializeField] GameObject player;
	[SerializeField] GameObject sword;
	[SerializeField] Transform damagePoint;
	[SerializeField] AudioClip[] sounds;
	AudioSource source;
	Animator animator;
	int playerHealth;
	
	[Header("Damage Properties")]
	[SerializeField] [Range( 10, 50)]  int swordDamage;
	[SerializeField] [Range( 0.05f, 1f)] float swingDelay;
	[SerializeField] [Range( 0.1f, 1.5f)] float AOERadius = 0.5f;
	[SerializeField] LayerMask enemyLayer;
	float attackCooldown;
	
	void Start(){
		source = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		playerHealth = player.GetComponent<PlayerScript>().playerHealth;
	}
	
	void Update() {
		if (Input.GetMouseButton(0) && Time.time > attackCooldown){
			Attack();
		}
		
		if(playerHealth <= 0){
			this.enabled = false;
		}
		
	}
	
	void Attack(){		

		//Attack code
		attackCooldown = Time.time + swingDelay;
		animator.SetTrigger("Attack");									//Play attack animation
		source.clip = sounds[Random.Range(0, sounds.Length)];			//Play attack sound
		source.Play();
		Collider[] minotaurCollider = Physics.OverlapSphere(damagePoint.position, AOERadius, enemyLayer);	//Cast AOE sphere to get minotaur's collider
		
		foreach(var Col in minotaurCollider){
			if(Col != null){
				minotaurCollider[0].GetComponent<EnemyAI>().TakeDamage(swordDamage);	//Damage the minotaur
			}
		}
		
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(damagePoint.position, AOERadius);	//Radius of sword damage
	}
}
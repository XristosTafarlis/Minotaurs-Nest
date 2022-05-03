using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour{
	
	[Header("Refferences")]
	[SerializeField] GameObject player;
	[SerializeField] GameObject sword;
	[SerializeField] Transform damagePoint;
	
	[Header("Damage Properties")]
	[SerializeField] [Range( 10, 50)]  int swordDamage;
	[SerializeField] [Range( 0.05f, 1f)] float swingDelay;
	[SerializeField] [Range( 0.1f, 1.5f)] float AOERadius = 0.5f;
	[SerializeField] LayerMask enemyLayer;
	float attackCooldown;
	
	void Start(){
	}
	
	void Update() {
		if (Input.GetMouseButton(0) && Time.time > attackCooldown){
			Attack();
		}
		
		if(player.GetComponent<PlayerScript>().playerHealth <= 0){
			this.enabled = false;
		}
		
	}
	
	void Attack(){		

		//Attack code
		attackCooldown = Time.time + swingDelay;
		GetComponent<Animator>().SetTrigger("Attack");	//Play attack animation
		
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
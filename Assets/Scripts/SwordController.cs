using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour{
	
	[SerializeField] GameObject sword;
	[SerializeField] Transform AOEPoint;
	
	[Header("Damage Properties")]
	[SerializeField] [Range( 10, 50)] int swordDamage;
	[SerializeField] float fireDelay;
	float attackCooldown;
	[SerializeField] float AOERadius = 0.5f;
	[SerializeField] LayerMask enemyLayer;
	
	void Start(){
	}
	
	void Update() {
		if (Input.GetMouseButton(0) && Time.time > attackCooldown){
			Attack();
		}
	}
	
	void Attack(){		

		//Attack code
		attackCooldown = Time.time + fireDelay;
		GetComponent<Animator>().SetTrigger("Attack");	//Play attack animation
		
		Collider[] minotaurCollider = Physics.OverlapSphere(AOEPoint.position, AOERadius, enemyLayer);	//Cast AOE sphere to get minotaur's collider
		
		foreach(var Col in minotaurCollider){
			if(Col != null){
				minotaurCollider[0].GetComponent<EnemyAI>().TakeDamage(swordDamage);	//Damage the minotaur
			}
		}
		
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(AOEPoint.position, AOERadius);	//Radius of sword damage
	}
}
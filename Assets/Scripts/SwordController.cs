using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour{
	public GameObject sword;
	Animator anim;
	public bool canAttack = true;
	public float attackCooldown;
	
	void Start(){
		anim = sword.GetComponent<Animator>();
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)){
			Debug.Log("ButtonDown 0");
			if(canAttack){
				Debug.Log("ButtonDown 0 + Can Attack");
				SwordAttack();
			}
		}
	}
	
	void SwordAttack(){
		canAttack = false;
		anim.SetTrigger("Attack");
	}
	
	
	void ResetAttack(){
		anim.ResetTrigger("Attack");
	}
}
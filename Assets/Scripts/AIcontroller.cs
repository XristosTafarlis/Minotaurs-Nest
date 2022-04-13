using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIcontroller : MonoBehaviour{
	
	public NavMeshAgent navMeshAgent;
	public float startWaitTime;
	public float timeToRotate;
	public float speedWalk;
	public float speedRun;
	
	
	public float viewRadius;
	public float viewAngle;
	public LayerMask playerMask;
	public LayerMask obstacleMask;
	public float meshResolution;
	public int edgeInteractions;
	public float edgeDistance;
	
	public Transform[] waypoints;
	int m_CurrentWaypointIndex;
	
	Vector3 playerLastPosition = Vector3.zero;
	Vector3 m_PlayerPosition;
	
	float m_WaitTime;
	float m_TimeToRotate;
	bool m_PlayerInRange;
	bool m_PlayerNear;
	bool m_IsPatrol;
	bool m_CaughtPlayer;
	
    void Start(){
		m_PlayerPosition = Vector3.zero;
		m_IsPatrol = true;
		m_CaughtPlayer = false;
		m_PlayerInRange = false;
		m_WaitTime = startWaitTime;
		m_TimeToRotate = timeToRotate;
		
        m_CurrentWaypointIndex = 0;
		navMeshAgent = GetComponent<NavMeshAgent>();
		
		navMeshAgent.isStopped = false;
		navMeshAgent.speed = speedWalk;
		navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void Update(){
        
	}
	
	void Move(float speed){
		navMeshAgent.isStopped = false;
		navMeshAgent.speed = speed;
	}
	
	void CaughtPlayer(){
		m_CaughtPlayer = true;
	}
	
	void LookingPlayer(Vector3 player){
		navMeshAgent.SetDestination(player);
		if(Vector3.Distance(transform.position, player) <= 0.3){
			if(m_WaitTime <= 0){
				m_PlayerNear = false;
				Move(speedWalk);
			}
		}
		
	}
}
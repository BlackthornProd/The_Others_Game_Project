using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerCarrier : MonoBehaviour {

	private NavMeshAgent agent;
	private Animator anim;
	private Vector3 randomPos;
	private Player player;
	public GameObject deathEffect;

	[Header("Character Stats")]
	public float speed;
	public float changeTarget = 4f;
	public int willBoost;

	[Header ("Positions")]
	public float xPosMin;
	public float xPosMax;
	public float zPosMin;
	public float zPosMax;



	void Start(){

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();;
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;

		changeTarget = 0;
	}

	void Update(){

		if(transform.position == randomPos){
			anim.SetBool("IsRunning", false);
			changeTarget = 0;
		} else {
			anim.SetBool("IsRunning", true);
		}

		if(player.will <= 0){
			Destroy(gameObject);
		}

		if(changeTarget <= 0 && player.will > 0){
			float randomX = Random.Range(xPosMin, xPosMax);
			float randomZ = Random.Range(zPosMin, zPosMax);

			randomPos = new Vector3(randomX, transform.position.y, randomZ);

			changeTarget = 4f;
			agent.SetDestination(randomPos);


		} else {
			changeTarget -= Time.deltaTime;
		}
	}

	public void TakeDamage(){
		player.will += willBoost;
		Vector3 deathPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
		Instantiate(deathEffect, deathPos, transform.rotation);
		Destroy(gameObject);
	}

}

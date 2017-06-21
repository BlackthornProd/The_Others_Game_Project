using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour {

	[Header ("Character Stats")]
	public float speed;
	public float timeToChangeTarget;
	public int health;
	public int giveWill;

	[Header ("References")]
	private NavMeshAgent agent;
	private Vector3 target;
	private Transform playerTarget;
	private Animator anim;
	private Player player;

	[Header ("Movement")]
	public float xMin;
	public float xMax;
	public float zMin;
	public float zMax;

	private bool agro;

	void Start(){

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;

		anim.SetTrigger("JumpTrigger");

		int isWhat = Random.Range(1, 3);
		if(isWhat == 1){
			agro = false;
		} else if(isWhat == 2) {
			agro = true;
		}

	}

	void Update(){

		if(health <= 0){
			player.will += giveWill;
			Destroy(gameObject);
		}

		if(agro == true && health > 0){
			agent.SetDestination(playerTarget.position);
		}

		if(agro == false && health > 0){

			agent.SetDestination(target);

			if(timeToChangeTarget <= 0){

				float randomX = Random.Range(xMin, xMax);
				float randomZ = Random.Range(zMin, zMax);
				target = new Vector3(randomX, transform.position.y, randomZ);
				timeToChangeTarget = 5f;

			} else {
				timeToChangeTarget -= Time.deltaTime;
			}
		}
	}

	public void TakeDamage(){
		health = 0;
	}

	public void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			player.TakeDamage(3);
			health = 0;
		}
	}

}

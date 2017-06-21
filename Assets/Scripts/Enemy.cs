using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	[Header ("Enemy Stats")]
	public int health = 5;
	public float speed = 3f;
	public int damage = 10;
	public int giveWill = 5;

	// references
	private Player player;
	private SpawnerScript spawner;
	private Animator anim;
	public NavMeshAgent agent;
	public GameObject deathEffect;

	// nav mesh variables
	bool isAfterPlayer = true;
	private Transform target;

	public bool canBeDeltDamage = true;

	void Start(){
		spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerScript>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	
		agent.speed = speed;
	}

	void Update(){

		// when the enemy dies ...
		if(health <= 0 && player.will > 0){
			player.will += giveWill;
			spawner.enemiesToKill--;
			Vector3 effectPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
			Instantiate(deathEffect, effectPos, transform.rotation);
			Destroy(gameObject);
		}
		if(spawner.enemiesToKill <= 0 && spawner.spawnerNumber != 4){
			health = 0;
		}

		// the enemy runs after the player
		if(health > 0 && player.will > 0){
			if(isAfterPlayer == true){
				agent.SetDestination(target.transform.position);	
				anim.SetBool("IsRunning", true);
			} else {
				agent.SetDestination(transform.position);
				anim.SetBool("IsRunning", false);
			}
		}

	}


	public void TakeDamage(int damage){
		if(canBeDeltDamage == true){
			health -= damage;
			anim.SetTrigger("HitTrigger");
			StartCoroutine(HitWait());
		}
	
	}

	// just make the enemy stop moving for a bit when he gets hit
	IEnumerator HitWait(){
		agent.speed = 0f;
		yield return new WaitForSeconds(1f);
		agent.speed = speed;
	}

	// when he touches the player ...
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			player.TakeDamage(damage);
			health = 0;
		}
	}
}

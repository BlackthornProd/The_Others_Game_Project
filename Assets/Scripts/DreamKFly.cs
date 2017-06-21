using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamKFly : MonoBehaviour {

	[Header("Enemy Stats")]
	public float speed;
	public int health;
	public float timeBtwShots = 2.5f;
	public int giveWill;

	[Header("References")]
	private Animator anim;
	private Transform target;
	private Player player;
	private SpawnerScript spawner;

	public GameObject bullet;
	public Transform spawnPos;
	public GameObject deathEffect;


	void Start(){
		spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerScript>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}

	void Update(){

		if(health <= 0){
			player.will += giveWill;
			spawner.enemiesToKill --;
			Vector3 effectPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
			Instantiate(deathEffect, effectPos, transform.rotation);
			Destroy(gameObject);
		}
		if(player.will <= 0){
			Destroy(gameObject);
		}

		if(spawner.enemiesToKill <= 0 && spawner.spawnerNumber != 4){
			health = 0;
		}

		if(transform.position.y < 7){
			transform.Translate(Vector3.up * speed * Time.deltaTime);
		}

		transform.LookAt(target);

		if(timeBtwShots <= 0){
			timeBtwShots = 4f;
			anim.SetTrigger("AttackTrigger");
			Instantiate(bullet, spawnPos.position, spawnPos.rotation);
		} else {
			timeBtwShots -= Time.deltaTime;
		}
	}

	public void TakeDamage(int damage){
		health -= damage;
		anim.SetTrigger("HitTrigger");
	}
}

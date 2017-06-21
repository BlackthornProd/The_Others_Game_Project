using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : MonoBehaviour {

	[Header ("References")]
	public GameObject[] spawnBullet;
	private Rigidbody rb;
	private SpawnerMaster spawnerMaster;
	private Animator anim;
	public GameObject deathEffect;
	public GameObject halo;

	[Header ("Head Stats")]
	public float minTime;
	public float maxTime;
	public int health = 3;
	private float timeBtwSpawns;


	bool firstShot = false;
	float timeBtwNextAttacks;
	int firstShotYes;

	void Start(){

		anim = GetComponent<Animator>();
		spawnerMaster = GameObject.FindGameObjectWithTag("SpawnerMaster").GetComponent<SpawnerMaster>();
		rb = GetComponent<Rigidbody>();
		timeBtwSpawns = Random.Range(minTime, maxTime);


		spawnerMaster.headsInGame ++;
		timeBtwNextAttacks = Random.Range(25, 35);
		firstShotYes = Random.Range(1, 4);
		if(firstShotYes == 1){
			firstShot = true;
		} else if(firstShotYes != 1){
			firstShot = false;
		}

		if(firstShot == true){
			Spawn();
			firstShotYes = Random.Range(1, 4);
		} else if(firstShot == false){
			firstShotYes = Random.Range(1, 4);
		}

	}

	void Update(){

		if(spawnerMaster.firstBoost == true){
			minTime = 30;
			maxTime = 30;
		} else if(spawnerMaster.secondBoost == true){
			minTime = 20;
			maxTime = 20;
		} else if(spawnerMaster.thirdBoost == true){
			minTime = 10;
			maxTime = 10;
		}

		if(health <= 0){
			if(spawnerMaster.bossHit == false && spawnerMaster.timesToBeHit > 15){
				spawnerMaster.bossHit = true;
			}
			spawnerMaster.timesToBeHit++;
			Instantiate(deathEffect, transform.position, Quaternion.identity);
			spawnerMaster.headsInGame--;
			Destroy(gameObject);
		}

		if(spawnerMaster.bossHit == false){
			rb.isKinematic = true;
			rb.useGravity = false;
		} else {
			halo.transform.Translate(Vector3.down * 5f * Time.deltaTime);
			rb.isKinematic = false;
			rb.useGravity = true;
		}

		if(timeBtwNextAttacks <= 0 && firstShotYes == 1){
			Spawn();
			timeBtwNextAttacks = Random.Range(minTime, maxTime);
			firstShotYes = Random.Range(1, 3);
		} 
		else if(timeBtwNextAttacks <= 0 && firstShotYes != 1){
			timeBtwNextAttacks = Random.Range(minTime, maxTime);
			firstShotYes = Random.Range(1, 3);
		}
		else {	
			timeBtwNextAttacks -= Time.deltaTime;
		}
	}

	public void TakeDamage(int damage){
		health -= damage;
		anim.SetTrigger("HitTrigger");
	}

	void Spawn(){
		int randomShot = Random.Range(0, spawnBullet.Length);
		Instantiate(spawnBullet[randomShot], transform.position, transform.rotation);
		timeBtwSpawns = Random.Range(minTime, maxTime);
	}
}

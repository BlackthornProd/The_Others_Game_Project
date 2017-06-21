using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

	[Header ("Initial Enemies")]
	public List<Transform> initialSpawnPoints = new List<Transform>();
	public Transform[] positionsToSpawn;
	public int initialNumOfEnemies = 4;

	[Header ("Spawn Points and Enemies")]
	public Transform[] spawnPoints;
	public GameObject[] enemies;
	public GameObject boss;

	[Header ("Wave Stats")]
	private float timeBtwSpawns;
	public float startTimeBtwSpawns;
	public int enemiesToKill;
	public float timeBtwNextWave;

	public int startEnemiesToKill;


	[Header ("References")]
	public Text enemiesToKillDisplay;
	public int spawnerNumber;
	public SpawnerMaster spawnerMaster;
	public Text warning;
	private Player player;
	private WhatWave whatWave;
	public Text waveNumberDisplay;



	

	void Start(){
		whatWave = GameObject.FindGameObjectWithTag("What Wave").GetComponent<WhatWave>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		timeBtwSpawns = startTimeBtwSpawns;

		if(spawnerNumber == 1 && whatWave.waveNumber == 0){
			whatWave.waveNumber = 1;
		} else if(spawnerNumber == 2 && whatWave.waveNumber == 1){
			whatWave.waveNumber = 2;
		} else if(spawnerNumber == 3 && whatWave.waveNumber == 2){
			whatWave.waveNumber = 3;
		} else if(spawnerNumber == 4 && whatWave.waveNumber == 3){
			whatWave.waveNumber = 4;
		}
		StartCoroutine(Wait());
		if(whatWave.waveNumber == 4){
			Vector3 bosPos = new Vector3(-1.7f, 18f, 0f);
			Instantiate(boss, bosPos, Quaternion.identity);
		}

		if(whatWave.waveNumber == 4){
			enemiesToKillDisplay.text = "Destroy all the heads !";
		}
	}

	void Update(){


		waveNumberDisplay.text = "Wave : " + spawnerNumber;

		if(spawnerNumber == 1 && whatWave.waveNumber == 0){
			whatWave.waveNumber = 1;
		} else if(spawnerNumber == 2 && whatWave.waveNumber == 1){
			whatWave.waveNumber = 2;
		} else if(spawnerNumber == 3 && whatWave.waveNumber == 2){
			whatWave.waveNumber = 3;
		} else if(spawnerNumber == 4 && whatWave.waveNumber == 3){
			whatWave.waveNumber = 4;
		}

		if(whatWave.waveNumber == 1 && enemiesToKill > 0){
				spawnerMaster.spawner1.SetActive(true);
				spawnerMaster.spawner2.SetActive(false);
				spawnerMaster.spawner3.SetActive(false);
				spawnerMaster.spawner4.SetActive(false);
        } else if(whatWave.waveNumber == 2 && enemiesToKill > 0){
				spawnerMaster.spawner1.SetActive(false);
				spawnerMaster.spawner2.SetActive(true);
				spawnerMaster.spawner3.SetActive(false);
				spawnerMaster.spawner4.SetActive(false);
		} else if(whatWave.waveNumber == 3 && enemiesToKill > 0){
				spawnerMaster.spawner1.SetActive(false);
				spawnerMaster.spawner2.SetActive(false);
				spawnerMaster.spawner3.SetActive(true);
				spawnerMaster.spawner4.SetActive(false);
		} else if(whatWave.waveNumber == 4 && enemiesToKill > 0){
				spawnerMaster.spawner1.SetActive(false);
				spawnerMaster.spawner2.SetActive(false);
				spawnerMaster.spawner3.SetActive(false);
				spawnerMaster.spawner4.SetActive(true);
		}

		if(whatWave.waveNumber != 4){
			enemiesToKillDisplay.text = "Enemies to Kill : " + enemiesToKill;
		}		


		if(enemiesToKill <= 0 && whatWave.waveNumber != 4){
			enemiesToKillDisplay.text = "Complete !";
			if(timeBtwNextWave > 0){
				timeBtwNextWave -= Time.deltaTime;
				return;
			} 
			if(whatWave.waveNumber == 1){
				spawnerMaster.spawner1.SetActive(false);
				spawnerMaster.spawner2.SetActive(true);
				spawnerMaster.spawner3.SetActive(false);
				spawnerMaster.spawner4.SetActive(false);

			} else if(whatWave.waveNumber == 2){
				spawnerMaster.spawner1.SetActive(false);
				spawnerMaster.spawner2.SetActive(false);
				spawnerMaster.spawner3.SetActive(true);
				spawnerMaster.spawner4.SetActive(false);

			} else if(whatWave.waveNumber == 3){
				spawnerMaster.spawner1.SetActive(false);
				spawnerMaster.spawner2.SetActive(false);
				warning.enabled = true;

				warning.text = "LOOK UP !";				
				float wait = 10f;
				if(wait <= 0){
					spawnerMaster.spawner3.SetActive(false);
					spawnerMaster.spawner4.SetActive(true);
				} else {
					wait -= Time.deltaTime;
				}

			} 
		} 

		// enemies spawn at intervals of time...
		if(timeBtwSpawns <= 0 && enemiesToKill > 0 && whatWave.waveNumber != 4){
			int randomEnemyIndex = Random.Range(0, enemies.Length);
			int randomPosIndex = Random.Range(0, spawnPoints.Length);
			Instantiate(enemies[randomEnemyIndex], spawnPoints[randomPosIndex].position, spawnPoints[randomPosIndex].rotation);
			timeBtwSpawns = startTimeBtwSpawns;
		} else {
			timeBtwSpawns -= Time.deltaTime;
		}


	}

	void SpawnBurst(){
			Debug.Log("CAM HERE" + spawnerNumber + " " +  whatWave.waveNumber);
		for (int i = 0; i < initialNumOfEnemies; i++) {
			int randomPos = Random.Range(0, initialSpawnPoints.Count);
			int randomEnemy = Random.Range(0, enemies.Length);
			Instantiate(enemies[randomEnemy], initialSpawnPoints[randomPos].position, initialSpawnPoints[randomPos].rotation);
			initialSpawnPoints.Remove(initialSpawnPoints[randomPos]);
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds(3f);
		SpawnBurst();
	}
}

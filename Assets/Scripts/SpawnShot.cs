using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShot : MonoBehaviour {

	private Vector3 target;
	public float speed;
	public GameObject[] spawnCharacter;
	private Player player;
	public int damage;
	public int giveWill;

	public int Xmin;
	public int Xmax;
	public int zMin;
	public int zMax;


	public bool isGood = false;

	void Start(){
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		Vector3 randomDestintaion = new Vector3(Random.Range(Xmin, Xmax), 0, Random.Range(zMin, zMax));
		target = randomDestintaion;
	}

	void Update(){

		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if(transform.position == target){
			Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.11f, transform.position.z);
			player.will -= damage;
			int randomChar = Random.Range(0, spawnCharacter.Length);
			if(isGood == false){
				Instantiate(spawnCharacter[randomChar], pos, Quaternion.identity);
			}
			Die();
		}
	}

	public void TakeDamage(){
		player.will += giveWill;
		Die();
	}

	void Die(){

		//int randomPos = Random.Range(0, spawnPoints.Length);
		if(isGood == true){
			Vector3 randomDestintaion = new Vector3(Random.Range(Xmin, Xmax), 0, Random.Range(zMin, zMax));
			Instantiate(spawnCharacter[0], randomDestintaion, Quaternion.identity);
		}

		Destroy(gameObject);
	}

}

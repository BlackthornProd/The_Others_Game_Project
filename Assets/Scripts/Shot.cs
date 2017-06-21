using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	[Header ("Shot Stats")]
	public float speed;
	public float followTime;
	public int health = 1;
	public int damage = 3;
	public int giveWill;

	private Transform target;
	private Player player;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	void Update(){

		if(health <= 0){
			Destroy(gameObject);
		}

		followTime -= Time.deltaTime;
		if(followTime > 0){
			transform.LookAt(target);
		} 

		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	public void TakeDamage(int damage){
		player.will += giveWill;
		health -= damage;
	}

	void OnTriggerEnter(Collider other){
		health = 0;

		if(other.CompareTag("Player")){
			player.TakeDamage(damage);
		}
	}
}

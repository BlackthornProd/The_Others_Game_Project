using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePos : MonoBehaviour {

	public float changeState = 5f;
	private bool isDefending = false;
	public float speedBoost = 2f;

	private Enemy enemy;
	private Animator anim;
	private SphereCollider sphere;



	void Start(){
		enemy = GetComponent<Enemy>();
		anim = GetComponent<Animator>();
	}

	void Update(){

		if(changeState <= 0 && isDefending == false){
			changeState = 5f;
			isDefending = true;
		} else if(changeState <= 0 && isDefending == true){
			changeState = 5f;
			isDefending = false;
		} else {
			changeState -= Time.deltaTime;
		}

		if(isDefending == true){
			enemy.canBeDeltDamage = false;
			anim.SetBool("IsDefending", true);
			enemy.agent.speed = enemy.speed + speedBoost;
		} else if(isDefending == false){
			enemy.canBeDeltDamage = true;
			anim.SetBool("IsDefending", false);
			enemy.agent.speed = enemy.speed;
		}
	}
}

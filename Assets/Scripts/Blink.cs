using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {

	public float timeBtwBlinks;
	public float minTime;
	public float maxTime;

	private Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
		timeBtwBlinks = Random.Range(minTime, maxTime);
	}

	void Update(){

		if(timeBtwBlinks <= 0){
			anim.SetTrigger("BlinkTrigger");
			timeBtwBlinks = Random.Range(minTime, maxTime);
		} else {
			timeBtwBlinks -= Time.deltaTime;
		}
	}
}

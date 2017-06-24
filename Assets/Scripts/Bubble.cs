using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	private Animator anim;

	public float timeBeforeDestroy;

	void Start(){

		anim = GetComponent<Animator>();
		anim.SetTrigger("StartTrigger");
	}

	void Update(){

		if(timeBeforeDestroy <= 0){
			StartCoroutine(DestroyAnim());
		} else {
			timeBeforeDestroy -= Time.deltaTime;
		}
	}

	IEnumerator DestroyAnim(){

		anim.SetTrigger("EndTrigger");
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}

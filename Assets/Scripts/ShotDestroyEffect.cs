using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDestroyEffect : MonoBehaviour {

	public GameObject destroyEffect;


	void OnCollisonEnter(Collider other){
		if(other.tag == "Player" || other.tag == "Ground"){
			Instantiate(destroyEffect, transform.position, transform.rotation);
		}
	}
}

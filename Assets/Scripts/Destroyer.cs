﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	public float timeBeforeDestroy;


	void Update(){

		Destroy(gameObject, timeBeforeDestroy);
	}
}

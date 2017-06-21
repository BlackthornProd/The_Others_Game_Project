using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

	[Header ("References")]
	public GameObject fpsCam;
	public Player player;
	public Image crosshairImage;
	public Animator anim;

	public GameObject shotGraphics;
	public GameObject impactEffect;

	[Header ("Gun Stats")]
	public int shotCost = 4;
	public int damage = 1;
	public LayerMask enemyLayer;


	void Update(){

		if(Input.GetButtonDown("Fire1") && player.will > 0){
			player.will -= shotCost;
			Shoot();
		} 

		RaycastHit crosshairInfo;
		if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out crosshairInfo, 1000f, enemyLayer)){
			anim.SetBool("IsPositive", true);
		} else {
			anim.SetBool("IsPositive", false);
		}
	}

	void Shoot(){

		RaycastHit hitInfo;
		if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, 1000f)){ // this will return true if the ray hits something

			// References of the scripts that the ray could hit
			Enemy enemyHit = hitInfo.transform.GetComponent<Enemy>();
			DreamKFly dreamKillerHit = hitInfo.transform.GetComponent<DreamKFly>();
			Shot shotHit = hitInfo.transform.GetComponent<Shot>();
			PowerCarrier powerCarrierHit = hitInfo.transform.GetComponent<PowerCarrier>();
			FirstBoss firstBossHit = hitInfo.transform.GetComponent<FirstBoss>();
			Spawn spawnHit = hitInfo.transform.GetComponent<Spawn>();
			SpawnShot spawnShotHit = hitInfo.transform.GetComponent<SpawnShot>();

			if(enemyHit != null){ 
				enemyHit.TakeDamage(damage);
			} 
			if(dreamKillerHit != null){
				dreamKillerHit.TakeDamage(damage);
			}
			if(shotHit != null){
				shotHit.TakeDamage(damage);
			}
			if(powerCarrierHit != null){
				powerCarrierHit.TakeDamage();
			}
			if(firstBossHit != null){
				firstBossHit.TakeDamage(damage);
			}
			if(spawnHit != null){
				spawnHit.TakeDamage();
			}
			if(spawnShotHit != null){
				spawnShotHit.TakeDamage();
			}
		}

		//Instantiate(shotGraphics, fpsCam.transform.position, fpsCam.transform.rotation);
		//Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
		

	}
	
}

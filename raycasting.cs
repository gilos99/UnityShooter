using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class raycasting : MonoBehaviour {

	public static void RaycastShoot(float legHP,float bodyHP,float headHP,Camera mainCamera,GameObject shaves,GameObject blood)
	{

		RaycastHit hit;
		if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward,out hit)) {
			if (hit.transform.gameObject.tag=="sciana") {
				Instantiate (shaves,hit.point,Quaternion.identity);
			}


			GameObject hitObj = hit.transform.parent.parent.gameObject;
			Animator hitAnim = hitObj.GetComponent<Animator> ();
			enemyScript enemy = hitObj.GetComponent<enemyScript> ();
			CharacterController zombieC = hitObj.GetComponent<CharacterController> ();
			if (hit.rigidbody!=null) {

			}
			if (hit.transform.gameObject.tag=="HeadHitBox") {
				Instantiate (blood,hit.point,Quaternion.identity);
				if (enemy.hp>headHP) {
					enemy.DamageTaken (headHP);
				}
				else if (enemy.hp<=headHP) {
					if (!enemy.deadAnim) {
						hitAnim.SetTrigger ("dead");
						enemy.deadAnim = true;
					}
					zombieC.enabled = false;
					Destroy (hit.transform.parent.gameObject);
					enemy.DamageTaken (headHP);
					enemy.speed = 0;
					enemy.pacz = false;
					Destroy (hit.transform.parent.parent.gameObject,20f);

					hitAnim.SetBool ("attack2",false);
				}
			}
			if (hit.transform.gameObject.tag=="BodyHitBox") {
				Instantiate (blood,hit.point,Quaternion.identity);
				if (enemy.hp>bodyHP) {
					enemy.DamageTaken (bodyHP);
				}
				else if (enemy.hp<=bodyHP) {
					if (!enemy.deadAnim) {
						hitAnim.SetTrigger ("dead");
						enemy.deadAnim = true;
					}
					zombieC.enabled = false;
					Destroy (hit.transform.parent.gameObject);	
					enemy.DamageTaken (bodyHP);
					enemy.speed = 0;
					enemy.pacz = false;
					Destroy (hit.transform.parent.parent.gameObject,20f);

					hitAnim.SetBool ("attack2",false);
				}
			}
			if (hit.transform.gameObject.tag=="legsBox") {
				Instantiate (blood,hit.point,Quaternion.identity);
				if (enemy.hp>legHP) {
					enemy.DamageTaken (legHP);
				}
				else if (enemy.hp<=legHP) {
					if (!enemy.deadAnim) {
						hitAnim.SetTrigger ("dead");
						enemy.deadAnim = true;
					}
					zombieC.enabled = false;
					Destroy (hit.transform.parent.gameObject);
					enemy.DamageTaken (legHP);
					enemy.speed = 0;
					Destroy (hit.transform.parent.parent.gameObject,20f);

					hitAnim.SetBool ("attack2",false);
				}
			}
		}
	}
}

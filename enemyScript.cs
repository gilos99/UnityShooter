using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public float hp=100f;
	public Transform me;
	public float dystans;
	public GameObject player;
	public Animator animator;
	public bool pacz=true;
	public bool deadAnim = false;

	void Start () {
		animator = GetComponent<Animator> ();
		me = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		animator.SetTrigger ("go");

	}


	void Update () {
		
		dystans = Vector3.Distance (me.position,player.transform.position);
		if (pacz) {
			me.LookAt (new Vector3(player.transform.position.x,me.position.y,player.transform.position.z));
		}

		if (dystans>3&&hp>0) {
				animator.SetTrigger ("gonew");
			}
		if (dystans>2&&hp>0) {
				me.Translate (Vector3.forward*speed*Time.deltaTime);

			} else if(dystans<=2.5f ){
				animator.SetTrigger ("attack");	
			}


	}
	public void DamageTaken(float _hp)
	{
		hp -= _hp;
	}



}

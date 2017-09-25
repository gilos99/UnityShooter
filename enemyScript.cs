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
	public float wysokosc;
	CharacterController zombieC;
	akScript ak;

	void Start () {
		animator = GetComponent<Animator> ();
		me = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		animator.SetTrigger ("go");
		zombieC = GetComponent<CharacterController> ();
		ak = FindObjectOfType<akScript> ();
	}


	void Update () {
		if (!zombieC.isGrounded) {
			wysokosc += Physics.gravity.y * Time.deltaTime;	
		}
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
			Attacking ();
			animator.SetTrigger ("attack");
			}
		Vector3 ruch = new Vector3 (0,wysokosc,0);
		ruch = transform.rotation * ruch;
		zombieC.Move (ruch * Time.deltaTime);

	}
	public void DamageTaken(float _hp)
	{
		hp -= _hp;
	}
	public void Attacking()
	{
		

	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public float hp=100f;
	Transform me;
	public float dystans;
	public GameObject player;
	void Start () {
		me = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		dystans = Vector3.Distance (me.position,player.transform.position);
		me.LookAt (new Vector3(player.transform.position.x,me.position.y,player.transform.position.z));
		if (dystans>=2) {
			me.Translate (Vector3.forward*speed*Time.deltaTime);
		}
	}
	public void DamageTaken(float _hp)
	{
		hp -= _hp;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	// Use this for initialization
	public float hp=100f;
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void DamageTaken(float _hp)
	{
		hp -= _hp;
	}

}

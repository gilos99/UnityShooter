using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class akScript : MonoBehaviour {

	Transform me;
	public Transform celownik;
	public Transform positionAk;
	public float machanie;
	public bool machanieBool=true;
	public float maxMachanie;
	public float machanieAmount=0.02f;
	void Start () {
		me = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		poruszanie ();
		me.position = new Vector3 (positionAk.position.x,positionAk.position.y+machanie,positionAk.position.z);
		me.LookAt (new Vector3(celownik.position.x,celownik.position.y+machanie,celownik.position.z));

	}
	void poruszanie()
	{
		var up = Input.GetKey (KeyCode.W);
		var down = Input.GetKey (KeyCode.S);
		var left = Input.GetKey (KeyCode.A);
		var right = Input.GetKey (KeyCode.D);
		var shotUp = Input.GetMouseButtonUp (0);
		var sprint = Input.GetKey (KeyCode.LeftShift);
		if (machanie>maxMachanie) {
			machanieBool = false;
		}
		else if (machanie<-maxMachanie) {
			machanieBool = true;
		}
		if (machanieBool&&maxMachanie!=0) {
			machanie += machanieAmount;
		}
		else if (!machanieBool&&maxMachanie!=0) {
			machanie -= machanieAmount;
		}
		if (machanie>0&&maxMachanie==0) {
			machanie -= machanieAmount;
		}
		if ((up||left||right)&&!sprint) {
			maxMachanie = 0.05f;
			machanieAmount = 0.007f;
		}
		else if ((up||left||right)&&sprint) {
			maxMachanie = 0.07f;
			machanieAmount = 0.01f;
		}
		else if (down) {
			maxMachanie = 0.03f;
			machanieAmount = 0.003f;
		}
		else if (!down&&!up&&!left&&!right) {
			maxMachanie = 0f;
		}
		if (shotUp) {
			machanie = 0.09f;

		}

	}
}

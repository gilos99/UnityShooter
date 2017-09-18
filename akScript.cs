using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class akScript : MonoBehaviour {

	Transform me;
	public Transform gracz;
	public playerController graczC;
	public Transform celownik;
	public Transform positionAk;
	public float machanie;
	public bool machanieBool=true;
	public float maxMachanie;
	public float machanieAmount=0.02f;
	public int amunicja;
	public int magazynek;
	public int maxAmunicja=30;
	public Text textAmunicja;
	public Text textMagazynek;
	public GameObject flara;
	public Camera mCamera;
	public enemyScript wrog;

	void Start () {
		me = GetComponent<Transform> ();
		magazynek = 3 * maxAmunicja;
		amunicja = maxAmunicja;
		wrog = FindObjectOfType<enemyScript> ();
		graczC = FindObjectOfType<playerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		poruszanie ();
		me.position = new Vector3 (positionAk.position.x,positionAk.position.y+machanie,positionAk.position.z);
		me.LookAt (new Vector3(celownik.position.x,celownik.position.y+machanie,celownik.position.z));
		textAmunicja.text = amunicja.ToString();
		textMagazynek.text = magazynek.ToString ();
	}
	void poruszanie()
	{
		var up = Input.GetKey (KeyCode.W);
		var down = Input.GetKey (KeyCode.S);
		var left = Input.GetKey (KeyCode.A);
		var right = Input.GetKey (KeyCode.D);
		var shotUp = Input.GetMouseButtonUp (0);
		var sprint = Input.GetKey (KeyCode.LeftShift);
		var przeladowanie = Input.GetKeyUp (KeyCode.R);
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
			machanieAmount = 0.012f;
		}
		else if (down) {
			maxMachanie = 0.03f;
			machanieAmount = 0.003f;
		}
		else if (!down&&!up&&!left&&!right) {
			maxMachanie = 0f;
		}
		if (shotUp&&amunicja>0) {
			
			Shoot ();
		}
		if (przeladowanie) {
			Reload (amunicja,magazynek);
		}
		if ((!shotUp)|| (amunicja<=0)) {
			flara.SetActive (false);
		}

	}
	void Shoot(){
		
			machanie = 0.09f;
			amunicja--;

		flara.SetActive (true);

		RaycastHit hit;
		if (Physics.Raycast(mCamera.transform.position,mCamera.transform.forward,out hit)) {
			Debug.Log (hit.transform.gameObject.tag);
			if (hit.transform.gameObject.tag=="HeadHitBox") {
				if (wrog.hp>80) {
					wrog.DamageTaken (80);
				}
				else if (wrog.hp<=80) {
					Destroy (hit.transform.parent.gameObject);
					wrog.hp = 100f;
				}
			}
			if (hit.transform.gameObject.tag=="BodyHitBox") {
				if (wrog.hp>20) {
					wrog.DamageTaken (20);
				}
				else if (wrog.hp<=20) {
					Destroy (hit.transform.parent.gameObject);
					wrog.hp = 100f;
				}
			}
		}
	}
	void Reload(int _ammo,int _magazynek){
		var dodatkoweAmmo = maxAmunicja - _ammo;

		if (dodatkoweAmmo<=_magazynek) {
			amunicja += dodatkoweAmmo;
			magazynek -= dodatkoweAmmo;
		}
		else if (dodatkoweAmmo>magazynek) {
			amunicja += _magazynek;
			magazynek -= _magazynek;
		}
	}
}

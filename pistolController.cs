using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pistolController : MonoBehaviour {

	Transform me;
	public Transform gracz;
	public playerController graczC;
	public Transform celownik;
	public Transform pistolPosition;
	public float machanie;
	public bool machanieBool=true;
	public float maxMachanie;
	public float machanieAmount=0.02f;
	public int amunicja;
	public int magazynek;
	public int maxAmunicja=17;
	public Text textAmunicja;
	public Text textMagazynek;
	public GameObject flara;
	public Camera mCamera;
	public enemyScript wrog;
	public float amountOdrzut;
	public Animator pistolAnimator;
	public float timer;
	public GameObject blood;
	public GameObject shaves;

	void Start () {
		me = GetComponent<Transform> ();
		magazynek =  60;
		amunicja = 5*maxAmunicja;
		wrog = FindObjectOfType<enemyScript> ();
		graczC = FindObjectOfType<playerController> ();
		pistolAnimator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (timer>0) {
			timer -= 1*Time.deltaTime;
		}
		if (timer<0) {
			timer = 0;
		}
		poruszanie ();
		me.position = new Vector3 (pistolPosition.position.x,pistolPosition.position.y+machanie,pistolPosition.position.z);

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
		var przeladowanieUp = Input.GetKeyUp (KeyCode.R);
		var przeladowanieDown = Input.GetKeyDown (KeyCode.R);
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
			maxMachanie = 0.02f;
			machanieAmount = 0.002f;
			amountOdrzut = 1f;
		}
		else if ((up||left||right)&&sprint) {
			maxMachanie = 0.025f;
			machanieAmount = 0.0025f;
			amountOdrzut = 1.2f;
		}
		else if (down) {
			maxMachanie = 0.01f;
			machanieAmount = 0.001f;
			amountOdrzut = 0.5f;
		}
		else if (!down&&!up&&!left&&!right) {
			maxMachanie = 0;
			amountOdrzut = 0.5f;
		}
		if (shotUp&&amunicja>0&&timer<=0) {

			Shoot ();
		}

		if (przeladowanieDown&&amunicja!=maxAmunicja&&magazynek!=0) {
			pistolAnimator.SetTrigger ("reload");
		}
		if (przeladowanieUp&&amunicja!=maxAmunicja&&magazynek!=0) {
			Reload (amunicja,magazynek);
			machanie = 0.01f;
			machanieAmount = 0.001f;
		}
		if ((!shotUp)|| (amunicja<=0)) {
			flara.SetActive (false);
		}

	}
	void Shoot(){
		pistolAnimator.SetTrigger ("shoot");
		graczC.odrzutY = amountOdrzut;

		amunicja--;

		machanie = 0.03f;	

		flara.SetActive (true);
	
		raycasting.RaycastShoot (5f, 10f, 30f, mCamera,shaves,blood);
	}
	void Reload(int _ammo,int _magazynek){
		var dodatkoweAmmo = maxAmunicja - _ammo;
		timer = 1f;
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

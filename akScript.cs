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
	public float amountOdrzut;
	public float coIle;
	public Animator animAk;
	public bool animacjaIsPlaying;
	public float timer;

	void Start () {
		me = GetComponent<Transform> ();
		magazynek = 3 * maxAmunicja;
		amunicja = maxAmunicja;
		wrog = FindObjectOfType<enemyScript> ();
		graczC = FindObjectOfType<playerController> ();
		animAk = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer>0) {
			timer -= Time.deltaTime*10;
			animacjaIsPlaying = true;
		}
		if (timer<=0) {
			animacjaIsPlaying = false;
			timer = 0;
		}

		if (coIle>0) {
			coIle -= 1f;
		}
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
		var shot = Input.GetMouseButton (0);
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
			maxMachanie = 0.05f;
			machanieAmount = 0.007f;
			amountOdrzut = 1.5f;
		}
		else if ((up||left||right)&&sprint) {
			maxMachanie = 0.07f;
			machanieAmount = 0.007f;
			amountOdrzut = 2f;
		}
		else if (down) {
			maxMachanie = 0.03f;
			machanieAmount = 0.003f;
			amountOdrzut = 1f;
		}
		else if (!down&&!up&&!left&&!right) {
			maxMachanie = 0f;
			amountOdrzut = 0.7f;
		}
		if (shotUp&&amunicja>0&&coIle==0&&!animacjaIsPlaying) {
			
			Shoot ();
		}
		if (shot&&amunicja>0&&coIle==0&&!animacjaIsPlaying) {

			Shoot ();
		}
		if (przeladowanieDown&&amunicja!=maxAmunicja&&magazynek!=0) {
			animAk.SetTrigger ("reload");
		}
		if (przeladowanieUp&&amunicja!=maxAmunicja&&magazynek!=0) {
			Reload (amunicja,magazynek);
		}
		if ((!shotUp&&!shot)|| (amunicja<=0)) {
			flara.SetActive (false);
		}

	}
	void Shoot(){
		graczC.odrzutY = amountOdrzut;

			amunicja--;
	
		machanie = 0.09f;	

		flara.SetActive (true);
		coIle = 7f;
		RaycastHit hit;
		if (Physics.Raycast(mCamera.transform.position,mCamera.transform.forward,out hit)) {
			Debug.Log (hit.transform.gameObject.tag);
			GameObject hitObj = hit.transform.parent.gameObject;
			Animator hitAnim = hitObj.GetComponent<Animator> ();
			enemyScript enemy = hitObj.GetComponent<enemyScript> ();
			if (hit.transform.gameObject.tag=="HeadHitBox") {
				
				if (enemy.hp>60) {
					enemy.DamageTaken (60);
				}
				else if (enemy.hp<=60) {
					if (!enemy.deadAnim) {
						hitAnim.SetTrigger ("dead");
						enemy.deadAnim = true;
					}

					enemy.speed = 0;
					enemy.pacz = false;
					Destroy (hit.transform.parent.gameObject,2f);
				
				}
			}
			if (hit.transform.gameObject.tag=="BodyHitBox") {
				if (enemy.hp>20) {
					enemy.DamageTaken (20);
				}
				else if (enemy.hp<=20) {
					if (!enemy.deadAnim) {
						hitAnim.SetTrigger ("dead");
						enemy.deadAnim = true;
					}
					enemy.speed = 0;
					enemy.pacz = false;
					Destroy (hit.transform.parent.gameObject,2f);

				}
			}
			if (hit.transform.gameObject.tag=="legsBox") {
				if (enemy.hp>10) {
					enemy.DamageTaken (10);
				}
				else if (enemy.hp<=10) {
					if (!enemy.deadAnim) {
						hitAnim.SetTrigger ("dead");
						enemy.deadAnim = true;
					};
					enemy.speed = 0;
					Destroy (hit.transform.parent.gameObject,2f);

				}
			}
		}
	}
	void Reload(int _ammo,int _magazynek){
		var dodatkoweAmmo = maxAmunicja - _ammo;
		timer = 12f;
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

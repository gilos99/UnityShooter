using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
	public Camera MainCamera;
	public Transform me;
	public CharacterController playerCC;
	public float aktualnaWysokosc=0f;
	public float wysokoscSkoku=4f;
	public float speed=9f;
	public float czuloscMyszki;
	float myszkaGoraDol;
	float myszkaPrawoLewo;
	public float maxGoraDol;
	 float przodTyl;
	 float lewoPrawo;
	public float predkoscBiegania = 7f;
	
	
	// Use this for initialization
	void Start () {
		playerCC = GetComponent<CharacterController> ();
		me = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		mouse ();
		keyboard ();
	}
	void keyboard()
	{
		przodTyl = Input.GetAxis ("Vertical")*speed;
		lewoPrawo = Input.GetAxis ("Horizontal") * speed;
		if (playerCC.isGrounded&&Input.GetKey(KeyCode.Space)) {
			aktualnaWysokosc = wysokoscSkoku;
		}
		else if (!playerCC.isGrounded) {
			aktualnaWysokosc += Physics.gravity.y * Time.deltaTime;	
		}

		if (Input.GetKey(KeyCode.S)) 
			{
			predkoscBiegania=0;
			if (!Input.GetKey(KeyCode.LeftShift)) {
				speed = 5f;

			}
		}
		else if (!Input.GetKey(KeyCode.S)) {
				predkoscBiegania=7f;
			if (!Input.GetKey(KeyCode.LeftShift)) {
				speed = 9f;

			}
			}

		if(Input.GetKeyDown("left shift")) {
			speed+=predkoscBiegania;
		} else if(Input.GetKeyUp("left shift")) {
			speed-=predkoscBiegania;
		}
		Vector3 ruch = new Vector3 (lewoPrawo,aktualnaWysokosc,przodTyl);
		ruch = transform.rotation * ruch;
		playerCC.Move (ruch*Time.deltaTime);

	}
	void mouse()
	{
		myszkaPrawoLewo = Input.GetAxis ("Mouse X") * czuloscMyszki;
		transform.Rotate (0,myszkaPrawoLewo,0);
		myszkaGoraDol -= Input.GetAxis ("Mouse Y")*czuloscMyszki;
		myszkaGoraDol = Mathf.Clamp (myszkaGoraDol,-maxGoraDol,maxGoraDol);
		MainCamera.transform.localRotation = Quaternion.Euler (myszkaGoraDol,0,0);
	}
}

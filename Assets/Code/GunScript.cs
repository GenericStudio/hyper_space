using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BulletProjections))]
[RequireComponent (typeof(Rigidbody))]
public class GunScript : MonoBehaviour
{
		private Hub hub;
		public Material bulletMaterial;
		public float FireDelay = 5f;
		public int bulletSpeed = 1;
		public float bulletSize = 2;
		public int bulletSpread = 5;
		public GameObject Bullet;
		private float TimeAtLastFire = 0f;
		private PlayerController player;
		public int RapidFireCost = 5;
		public int ChargeCharge = 0;
	private GameObject ChargeShot;
	private bool[] weaponSystems = new bool[2]{true,true};

		// Update is called once per frame
		void Start ()
		{
				player = GameObject.Find ("Player").GetComponent<PlayerController> ();
				hub = GameObject.Find ("hub").GetComponent<Hub> ();


		}

		public void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Keypad1)) {
						weaponSystems [1] = !weaponSystems [1];
				}
				if (Input.GetKeyDown (KeyCode.Keypad0)) {
						weaponSystems [0] = !weaponSystems [0];
				}


				if (weaponSystems [0]) {
						if (Input.GetMouseButton (0)) {
								if (TimeAtLastFire < (Time.timeSinceLevelLoad - FireDelay) && player.energy > RapidFireCost) {
										player.energy -= RapidFireCost;
										TimeAtLastFire = Time.timeSinceLevelLoad;
										for (int i = 0; i < transform.childCount; i++) {
						if (transform.GetChild (i).name == "MuzzleSmall"&&transform.GetChild (i).gameObject.activeSelf) {
														GameObject cube = Instantiate (Bullet, transform.GetChild (i).transform.position, Quaternion.identity) as GameObject;
														if (hub.latice.Active) {
																hub.latice.AddObjectToLatice (cube);
														}
														cube.transform.localScale = new Vector3 (bulletSize, bulletSize, bulletSize);
														cube.name = "bullet";
														cube.AddComponent<BulletProjections>();
														cube.rigidbody.velocity = rigidbody.velocity + transform.forward * bulletSpeed + Random.onUnitSphere * bulletSpread;
														Physics.IgnoreCollision (cube.collider, gameObject.collider);
												}
										}
										audio.Play ();
								}
						}
				}
	//	if(weaponSystems[1]){
	//		if (Input.GetMouseButton (0)) {
	//			if(ChargeShot == null){
	//				for (int i = 0; i < transform.childCount; i++) {
	//					if (transform.GetChild (i).name == "MuzzleCharge") {
	//						ChargeShot=Instantiate (Bullet, transform.GetChild (i).transform.position, transform.GetChild (i).transform.rotation) as GameObject;
	//						ChargeShot.transform.parent = transform.GetChild(i).transform;
	//						ChargeShot.collider.enabled=false;
	//
	//
	//			}
	//				}
	//			}
	//			if(ChargeCharge<100 && player.energy>1){
	//				ChargeCharge++;
	//				player.energy--;
	//				ChargeShot.transform.localScale = new Vector3(ChargeCharge/15,ChargeCharge/15,ChargeCharge/15);
	//				ChargeShot.rigidbody.mass=ChargeCharge;
	//				ChargeShot.transform.position = ChargeShot.transform.parent.position+ (ChargeShot.transform.parent.forward * ChargeCharge)/5;
	//
	//			}else{
	//				ChargeShot.transform.position = ChargeShot.transform.parent.position+ (ChargeShot.transform.parent.forward * ChargeCharge)/5;
	//			}
	//
	//			
	//		}else if(ChargeShot !=null && ChargeCharge>0 && !Input.GetMouseButton(0)){
	//			if (hub.latice.Active) {
	//				hub.latice.AddObjectToLatice (ChargeShot);
	//			}
	//			//ChargeShot.AddComponent<BulletProjections>();
	//			ChargeShot.rigidbody.velocity = rigidbody.velocity + transform.forward * bulletSpeed + Random.onUnitSphere * bulletSpread;
	//			ChargeShot.collider.enabled=true;
	//		
	//			Physics.IgnoreCollision (ChargeShot.collider, gameObject.collider);
	//			ChargeShot.GetComponent<BulletScript>().damage=ChargeCharge/5;
	//			ChargeCharge=0;
	//			ChargeShot.transform.parent=null;
	//			ChargeShot = null;
	//
	//		}
	//	}
	}
}

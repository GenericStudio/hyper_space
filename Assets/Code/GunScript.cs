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
	public GameObject Missle;

		private float TimeAtLastFire = 0f;
		private PlayerController player;
		public int RapidFireCost = 5;
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
		if (TimeAtLastFire < (Time.timeSinceLevelLoad - FireDelay) && player.energy > RapidFireCost) {

				if (weaponSystems [0]) {
				if (Input.GetAxis("Fire1")<-0.25f) {
					player.energy -= RapidFireCost;
					TimeAtLastFire = Time.timeSinceLevelLoad;
					for (int i = 0; i < transform.childCount; i++) {
					if (transform.GetChild (i).name == "MuzzleSmall"&&transform.GetChild (i).gameObject.activeSelf) {
						GameObject cube = Instantiate (Bullet, transform.GetChild (i).transform.position, transform.rotation) as GameObject;
						if (hub.latice.Active) {
							//	hub.latice.AddObjectToLatice (cube);
						}
						cube.transform.localScale = new Vector3 (bulletSize/10, bulletSize/10, bulletSize*10);
						cube.name = "bullet";
						cube.rigidbody.velocity = rigidbody.velocity + transform.forward * bulletSpeed + Random.onUnitSphere * bulletSpread;
						Physics.IgnoreCollision (cube.collider, gameObject.collider);
					}
				}
				
			}

			}

			}
		if (TimeAtLastFire < (Time.timeSinceLevelLoad - FireDelay*4) && player.energy > RapidFireCost) {

			if (Input.GetAxis("Fire1")>0.25f) {



				player.energy -= RapidFireCost;
				TimeAtLastFire = Time.timeSinceLevelLoad;
				for (int i = 0; i < transform.childCount; i++) {
					if (transform.GetChild (i).name == "MuzzleLarge"&&transform.GetChild (i).gameObject.activeSelf) {
						GameObject cube = Instantiate (Missle, transform.GetChild (i).transform.position, Quaternion.identity) as GameObject;

						BulletScript bullet_props = GetComponent<BulletScript>();
						cube.transform.localScale = new Vector3 (bulletSize*10, bulletSize*10, bulletSize*10);
						cube.name = "GravityCenter";
						cube.renderer.material.color = Color.red;
						//cube.AddComponent <Attractor>();
						
						cube.rigidbody.velocity = rigidbody.velocity + transform.forward * bulletSpeed + Random.onUnitSphere * bulletSpread*2;
						cube.rigidbody.drag=0.5f;
						cube.rigidbody.mass*=10;
						Physics.IgnoreCollision (cube.collider, gameObject.collider);
					}

			}
				}
		}
		}

}

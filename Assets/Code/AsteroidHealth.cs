using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidHealth : MonoBehaviour {


	public int health = 1;
	public GameObject impact;
	public GameObject destruction;
	public GameObject asteroid;
	public Hub hub;

	// Use this for initialization
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
	}

	public void OnCollisionEnter(Collision collision) {
		if(collision.relativeVelocity.magnitude> 50 && GetComponent<AudioSource>()){
		audio.pitch = (100/transform.localScale.magnitude)+Random.Range(-.05f,0.05f);
		audio.Play();
		}
	}
	public void HIT(int damage,GameObject collider){
			health-= damage;
		if(health<0){ 

			List<List<CloneProperties>> clones = GetComponent<Projection_AllVisibleZones>().Clones;
			Vector3 pos =transform.position;
			float distance = Vector3.Distance(collider.transform.position,transform.position);
			foreach(List<CloneProperties> clone in clones){
				foreach(CloneProperties cl in clone){
					float anfle = Vector3.Distance(collider.transform.position,cl.transform.position);
					if(anfle<distance){
						distance = anfle;
					pos = cl.self.transform.position;
				}
				}
			}
			GameObject explosion = Instantiate(destruction,pos,transform.rotation) as GameObject;
			explosion.particleSystem.startSize=transform.lossyScale.magnitude;
			explosion.transform.GetChild(0).particleSystem.startSize=transform.lossyScale.magnitude;
			int Value = (int)(rigidbody.mass/2f);
			if(rigidbody.mass>20){
				for(int i = 0 ; i <2; i++){
					GameObject ass1 = Instantiate(hub.asteroid, transform.position+Random.insideUnitSphere*transform.localScale.x*0.5f ,transform.rotation) as GameObject;
					ass1.GetComponent<AsteroidHealth>().health=Value/2;
					ass1.rigidbody.mass = Value;
					ass1.transform.localScale = new Vector3(transform.localScale.x*.5f,transform.localScale.x*.5f,transform.localScale.x*.5f);
					ass1.rigidbody.velocity = rigidbody.velocity;
					ass1.rigidbody.inertiaTensorRotation=rigidbody.inertiaTensorRotation;
				}
			}
			GameObject.Destroy(this.gameObject);
			
			
		}
	}
}

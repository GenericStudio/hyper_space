using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour {


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
		if(collision.relativeVelocity.magnitude> 25 && GetComponent<AudioSource>()){
		audio.pitch = 1-(1/transform.localScale.magnitude);
		audio.Play();
		}
		if(collision.gameObject.GetComponent<BulletScript>()){

			health-= collision.gameObject.GetComponent<BulletScript>().damage;
			Instantiate(impact,collision.contacts[0].point,transform.rotation);

		
			if(GetComponent<ParticleSystem>())
				GetComponentInChildren<ParticleSystem>().Play();

		}
		if(health<0){ 
			//List<List<CloneProperties>> clones = GetComponent<Projection_AllVisibleZones>().Clones;
			Vector3 pos =transform.position;
			//float angle = Vector3.Angle(hub.player.transform.forward,(hub.player.transform.position-transform.position).normalized);
			//foreach(List<CloneProperties> clone in clones){
			//	foreach(CloneProperties cl in clone){
				//	float anfle = Vector3.Angle(hub.player.transform.forward,(hub.player.transform.position-cl.self.transform.position).normalized);
			//	if(anfle<angle){
				//	anfle = angle;
				//		pos = cl.self.transform.position;
			//	}
			//	}
			//}//
 			GameObject explosion = Instantiate(destruction,pos,transform.rotation) as GameObject;
			explosion.particleSystem.startSize=transform.lossyScale.magnitude;
			explosion.transform.GetChild(0).particleSystem.startSize=transform.lossyScale.magnitude;

			if(rigidbody.mass>100){
			for(int i = 0 ; i <3; i++){
				GameObject ass1 = Instantiate(hub.asteroid, transform.position+Random.insideUnitSphere*transform.localScale.x*0.5f ,transform.rotation) as GameObject;
				ass1.GetComponent<EnemyHealth>().health=(int)rigidbody.mass/10;
				
				ass1.transform.localScale = new Vector3(transform.localScale.x*.3f,transform.localScale.x*.3f,transform.localScale.x*.3f);
				ass1.rigidbody.AddForce(Random.onUnitSphere * (int)rigidbody.mass*100);
			}
			}
			GameObject.Destroy(this.gameObject);


		}
	}
}

using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {


	public int health = 1;
	public GameObject impact;
	public GameObject destruction;
	public GameObject asteroid;

	// Use this for initialization
	void Start () {
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
		if(health==0){ 
 			GameObject explosion = Instantiate(destruction,transform.position,transform.rotation) as GameObject;
			explosion.particleSystem.startSize=transform.lossyScale.magnitude;
			explosion.transform.GetChild(0).particleSystem.startSize=transform.lossyScale.magnitude;
		//	if(transform.lossyScale.x >50){
		//		GameObject ass1 = Instantiate(asteroid, transform.position ,transform.rotation) as GameObject;
	//			ass1.transform.localScale = new Vector3(transform.localScale.x*.3f,transform.localScale.x*.3f,transform.localScale.x*.3f);
	//			ass1.SetActive(true);
		//		GameObject ass2 =Instantiate(asteroid, transform.position,transform.rotation) as GameObject;
		//		ass2.transform.localScale = new Vector3(transform.localScale.x*.3f,transform.localScale.x*.3f,transform.localScale.x*.3f);
		//		ass2.SetActive(true);
				
		//	}
			GameObject.Destroy(this.gameObject);
			if(particleSystem!=null) particleSystem.Emit(5000);

		}
	}
}

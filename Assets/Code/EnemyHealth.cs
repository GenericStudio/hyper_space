using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {


	public int health = 1;
	// Use this for initialization
	void Start () {
		health *= (int)transform.lossyScale.magnitude;
	}

	public void OnCollisionEnter(Collision collision) {
		if(collision.relativeVelocity.magnitude> 25 && GetComponent<AudioSource>()){
		audio.pitch = 1-(1/transform.localScale.magnitude);
		audio.Play();
		}
		if(collision.gameObject.GetComponent<BulletScript>()){
			health-= collision.gameObject.GetComponent<BulletScript>().damage;
		
			if(GetComponent<ParticleSystem>())
				GetComponentInChildren<ParticleSystem>().Play();

		}
		if(health<1){ 
			GameObject.Destroy(this.gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private bool alive = true;
	private Hub hub;
	public int damage = 1;
	public GameObject HitAnim;
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
		if (hub.latice.Active) {
		}
		Destroy (gameObject, 60f);
		audio.Play();
	}
	

	void OnCollisionEnter(Collision c){
		if(c.collider.gameObject.GetComponent<AsteroidHealth>()!=null){
			c.collider.gameObject.GetComponent<AsteroidHealth>().HIT (damage,GetComponent<SingleProjection>().Clone_Props.self);
		Instantiate(HitAnim,GetComponent<SingleProjection>().Clone_Props.self.transform.position,transform.rotation);

		Destroy (gameObject);
		}
	}
	void OnDestroy(){

	}





}

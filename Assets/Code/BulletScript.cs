using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private bool alive = true;
	private Hub hub;
	public int damage = 1;
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
		Destroy (gameObject, 15f);
	}
	

	void OnCollisionEnter(Collision c){
		if(!(c.contacts[0].otherCollider.gameObject.GetComponent<BulletScript>()))
		Destroy (gameObject, 1f);
	}
	void OnDestroy(){

	}

}

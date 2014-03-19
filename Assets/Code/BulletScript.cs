using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private bool alive = true;
	private Hub hub;
	public int damage = 1;
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
		Destroy (gameObject, 10f);
	}
	
	void Update(){

	}
	void OnCollisionEnter(){

		Destroy (gameObject, 1f);
	}
	void OnDestroy(){

	}

}

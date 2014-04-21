using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public  class AsteroidScript :MonoBehaviour {
	
	private Hub hub;
	private List<GameObject> asteroids = new List<GameObject>();


	public void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();

		rigidbody.mass = (int)transform.localScale.magnitude;

	}

	public void OnDestroy(){

	}
	public void OnApplicationQuit(){
		Destroy(this.gameObject);
	}



		




	
}


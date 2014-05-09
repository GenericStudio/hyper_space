using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public  class AsteroidScript :MonoBehaviour {
	
	private Hub hub;
	private List<GameObject> asteroids = new List<GameObject>();


	public void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();
		if (hub.latice.Active) {
			hub.latice.AddObjectToLatice (this.gameObject);
		}
	}

	public void OnDestroy(){

	}
	public void OnApplicationQuit(){
		Destroy(this.gameObject);
	}



		




	
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public  class AsteroidScript :MonoBehaviour {
	

	private Hub hub;
	private List<GameObject> asteroids = new List<GameObject>();

	public void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();
		GetComponent<EnemyHealth>().health = (int)transform.localScale.magnitude*5;
		rigidbody.mass = GetComponent<EnemyHealth>().health;
		asteroids = hub.latice.LaticeObjectManager.Where(x=>x.GetComponent<AsteroidScript>()!=null).ToList();

	}

	public void FixedUpdate(){
		Vector3 gravity = Vector3.zero;
		var self_point = hub.LaticeBox.transform.InverseTransformPoint(transform.position - rigidbody.velocity);
		foreach(GameObject ass in asteroids){
			Vector3 difference = new Vector3();
			var target_point =  hub.LaticeBox.transform.InverseTransformPoint(ass.transform.position-ass.rigidbody.velocity);
	
			difference = self_point-target_point;

			Vector3 direction = (difference);
			if(difference.x>0.5) direction.x = -direction.x;
			if(difference.y>0.5) direction.y = -direction.y;
			if(difference.z>0.5) direction.z = -direction.z;


			gravity-=direction *ass.rigidbody.mass*9.81f* (1/((ass.rigidbody.velocity - rigidbody.velocity).magnitude+1)) ;


		}
		rigidbody.AddForce(gravity);
	}

		




	
}


using UnityEngine;
using System.Collections;

public class AvoidPlayer : MonoBehaviour {
	private Hub hub;

	// Use this for initialization
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
		

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 difference = new Vector3();
		var player_point =  hub.LaticeBox.transform.InverseTransformPoint(hub.player.transform.position+hub.player.rigidbody.velocity);
		var self_point = hub.LaticeBox.transform.InverseTransformPoint(transform.position + rigidbody.velocity);
		difference = self_point-player_point;

		Vector3 direction = (difference.normalized);
		if(difference.x>0.5) direction.x = -direction.x;
		if(difference.y>0.5) direction.y = -direction.y;
		if(difference.z>0.5) direction.z = -direction.z;
		float force = (difference.magnitude)*rigidbody.mass * 900.81f*Time.deltaTime;

		//print ("D"+direction+":  F"+force);
		rigidbody.AddForce(force*direction);
		Debug.DrawLine(transform.position,transform.position + (direction*force));
		

	}
}

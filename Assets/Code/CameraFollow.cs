using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {
	private Hub hub;
	public Transform target;
	public Vector3 offset;
	public float slipperiness = 0.5f;
	public List<Quaternion> rotation_history = new List<Quaternion>();
	// Use this for initialization
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
		target = hub.player.transform;
		transform.position = target.transform.position  - offset;
		transform.LookAt(target.position);
	}
	
	// Update is called once per frame
	void Update () {

		if(transform && target && target.transform){
			transform.position = target.transform.position +target.up * offset.y + target.right*offset.x + target.forward*offset.z;
			transform.rotation =  Quaternion.Lerp(transform.rotation,target.transform.rotation,slipperiness);
		}
	}
}

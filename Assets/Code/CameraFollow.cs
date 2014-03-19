using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	private Hub hub;
	public Transform target;
	public Vector3 offset;
	public float slipperiness = 0.5f;
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
			transform.rotation =  Quaternion.Slerp(transform.rotation,Quaternion.LookRotation((target.position + target.forward*100 ) - transform.position,target.up),slipperiness);
		}
	}
}

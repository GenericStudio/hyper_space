using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TurretScript : MonoBehaviour {
		
	private Hub hub;
		public GameObject goTarget;
		public float  maxDegreesPerSecond = 30.0f;
		private Quaternion qTo;
	public int turretRange;
	public int turretFieldOfView;
	private LineRenderer line ;
	public GameObject Muzzle;
		
		public void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
		line = GetComponent<LineRenderer>();
			qTo = goTarget.transform.localRotation;
		goTarget = GameObject.Find("Player").gameObject;

		}
		
		public void Update () {
			Vector3 v3T = goTarget.transform.position - transform.position;
			Vector3 tagetLock = transform.forward*turretRange;


		if(v3T.magnitude<turretRange && Vector3.Angle(transform.forward,v3T)<turretFieldOfView){
				
			tagetLock = goTarget.transform.position;
			qTo = Quaternion.LookRotation(v3T, Vector3.up);       
			transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);


		}
		else{
			tagetLock = transform.forward*turretRange;
			qTo = Quaternion.LookRotation(transform.forward, Vector3.up);       
			transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);
		}
			
		line.SetPosition(0,Muzzle.transform.position);
		line.SetPosition(1,tagetLock);
		}
		
}


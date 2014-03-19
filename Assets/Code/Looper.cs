using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Looper : MonoBehaviour {

	//private int border;
	private LaticeController latice;
	public Vector3 LoopCounter;
	private Hub hub;
	private Transform center;
	private bool LaticeWasActive = false;
	private bool looping = false;
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
		latice = hub.latice;

	
	}
	
	// Update is called once per frame
	void Update () { 
		if(latice.Active) {
			if(!LaticeWasActive){
				center = latice.LaticeBox.transform;
				LaticeWasActive=true;
				LoopCounter = new Vector3();
			}
		
		float right =center.transform.InverseTransformPoint(transform.position).x ;
		float up =center.transform.InverseTransformPoint(transform.position).y;
		float forward =center.transform.InverseTransformPoint(transform.position).z;
			if (right >.5) {
				right--;      
				LoopCounter.x++;
				looping = true;
			}
			else if (right < -.5) {
				right++;      
				LoopCounter.x--;
				looping = true;
			}
			if (up >.5) {
				up--;      
				LoopCounter.y++;
				looping = true;
			}
			else if (up < -.5) {
				up++;      
				LoopCounter.y--;
				looping = true;
				
			}
			if (forward >.5) {
				forward--;      
			LoopCounter.z++;
			looping = true;
		}
		else if (forward < -.5) {
				forward++;      
				LoopCounter.z--;
				looping = true;
		}
		if(looping){
			Vector3 worldPos = center.transform.localToWorldMatrix.MultiplyPoint(new Vector3(right,up,forward));
			transform.position =worldPos ;
				looping = false;
		}
		}else{
			if(LaticeWasActive){
				LaticeWasActive=false;
			}
		}

	}
}

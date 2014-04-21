using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ZoneController : MonoBehaviour {
	public List<List<Quaternion>> MasterZoneList = new List<List<Quaternion>>();
	public List<int> MasterFreshness = new List<int>();
	public LaticeController latice;
	public int maxZones = 100;
	private Hub hub;
	public void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();
		MasterZoneList = new List<List<Quaternion>>();
		for(int i = 0; i<= latice._max_Iterations;i++){
			MasterZoneList.Add(new List<Quaternion>());
			MasterFreshness.Add (0);
		}

		for(int x = -latice._max_Iterations; x <= latice._max_Iterations; x++){
			for(int y = -latice._max_Iterations; y <= latice._max_Iterations; y++){
				for(int z = -latice._max_Iterations; z <= latice._max_Iterations; z++){
					if(Mathf.Max (Mathf.Abs (x),Mathf.Abs (y),Mathf.Abs (z)) > 0){
						MasterZoneList[Mathf.Max (Mathf.Abs (x),Mathf.Abs (y),Mathf.Abs (z))].Add (new Quaternion(x,y,z,(Mathf.Max (Mathf.Abs (x),Mathf.Abs (y),Mathf.Abs (z)))));
					}
				}
			}
		}

			for(int i = 1 ; i < MasterZoneList.Count;i++){
				
					MasterFreshness[i]=Time.frameCount;
					MasterZoneList[i] = MasterZoneList[i].OrderBy(x=> Vector3.Angle(new Vector3(x.x,x.y,x.z), hub.LaticeBox.transform.InverseTransformDirection(transform.forward))).ToList();
				}
			

	}
	public void Update(){
		if(latice.Active){
			for(int i = 1 ; i < MasterZoneList.Count;i++){
				if( i < 3 || Time.frameCount%(i*2) == 0){
					MasterFreshness[i]=Time.frameCount;
					MasterZoneList[i] = MasterZoneList[i].OrderBy(x=> Vector3.Angle(new Vector3(x.x,x.y,x.z), hub.LaticeBox.transform.InverseTransformDirection(transform.forward))).ToList();
				}
		}
		}
	}
}



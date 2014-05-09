using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Projection_AllVisibleZones :MonoBehaviour
{
		private Hub hub;
		public List<List<CloneProperties>> Clones = new List<List<CloneProperties>> ();
		private ZoneController zoneController;
		public LaticeController latice;
		public List<List<Quaternion>> turnZones = new List<List<Quaternion>>();
		public List<int> turnZonesFreshness;
		public List<Quaternion> visableZones;
	public GameObject target_retical;
		
		public void Start ()
		{
				hub = GameObject.Find ("hub").GetComponent<Hub> ();
				latice = hub.latice;
				turnZones = new List<List<Quaternion>> ();
				zoneController = hub.zoneController;
		if(target_retical!=null){
			GameObject reticle = (GameObject)Instantiate(target_retical,transform.position,transform.rotation);
			
			reticle.transform.parent=this.transform;
		}
		}

		public void Update ()
		{
			if(latice.Active){
				
				
			for (int i = 0; i <= latice.Actual_Iterations; i++) {
					if (turnZones.Count <= i) {
						turnZonesFreshness.Add (0);
						turnZones.Add (new List<Quaternion> ());
					}
					int zones = 18 + i*5;
					if(i>4){
						zones -= (latice.Actual_Iterations-i)*7;
				}
				if(turnZonesFreshness[i] < zoneController.MasterFreshness[i]){
					turnZonesFreshness[i] = zoneController.MasterFreshness[i];
					turnZones [i] = zoneController.MasterZoneList [i].Take (Mathf.Min (zones,zoneController.MasterZoneList [i].Count)).ToList ();
				}
				
			}
			for (int i = latice._max_Iterations; i > latice.Actual_Iterations; i--) {
				if (turnZones.Count > i) {
					turnZones.RemoveAt(i);
				}

				
			}
			for(int i = latice.Actual_Iterations+1; i < latice._max_Iterations;i++){
				if(Clones.Count > i && Clones[i].Count>0){
					while (Clones[i].Count>0) {
						//print ("Kill Clone 0");
						
						CloneProperties dis_clone = Clones[i].ElementAt(Clones[i].Count-1);
						Clones[i].RemoveAt (Clones[i].Count-1);
						Destroy (dis_clone.self.gameObject);
						
					} 
					Clones.RemoveAt(i);
					i--;
				}
				


		}
				for (int i = 0; i < turnZones.Count; i++) {
						if (i > Clones.Count-1) {
							Clones.Add (new List<CloneProperties> ());
						}
						while (Clones[i].Count<turnZones[i].Count) {
							//	print ("Making Clone");
								GameObject Clone = new GameObject ();
								Clone.AddComponent<MeshRenderer> ();
								Clone.renderer.material = renderer.material;
					Clone.renderer.material.color = Color.Lerp (renderer.material.color,new Color(0,0,0.01f*i),((float)i)/turnZones.Count*0.8f);

								Clone.AddComponent<MeshFilter> ();
								CloneProperties CloneObj =  Clone.AddComponent<CloneProperties>();
								Clone.GetComponent<MeshFilter> ().sharedMesh =GetComponent<MeshFilter> ().mesh;
								Clone.name = this.gameObject.name + "_Projection:" + i;
								CloneObj.Original = transform;
								CloneObj.self = Clone;
								Clone.transform.localScale = transform.localScale;
								Clones [i].Add (CloneObj);	
						}
				}
			for (int i = turnZones.Count; i < Clones.Count; i++) {
				if(Clones[i].Count>0){
					while (Clones[i].Count>0) {
						//print ("Kill Clone 1");
						
						CloneProperties dis_clone = Clones[i].ElementAt(Clones[i].Count-1);
						Clones[i].RemoveAt (Clones[i].Count-1);
						Destroy (dis_clone.self.gameObject);
						
					}   
					Clones.RemoveAt(i);
					i--;
				}


			}


			}else{
			for (int i = Clones.Count-1; i >turnZones.Count-1 ; i--) {
					while (Clones[i].Count>0) {
						print ("Kill Clone");
						CloneProperties dis_clone = Clones[i].ElementAt(Clones[i].Count-1);
						Clones[i].RemoveAt (Clones[i].Count-1);
						Destroy (dis_clone.self.gameObject);
					}
					if(Clones[i].Count==0)
						Clones.RemoveAt(Clones.Count-1);
				}
			}
			for (int i = 0; i < Clones.Count; i++) {
				for (int j = 0; j < Clones[i].Count; j++) {
					if(Clones[i][j]!=null){
						try{
							Clones[i][j].offset = new Vector3 (turnZones [i] [j].x, turnZones [i] [j].y, turnZones [i] [j].z);
						Clones [i] [j].self.transform.position = latice.LaticeBox.transform.localToWorldMatrix.MultiplyPoint (Clones[i][j].offset + (latice.LaticeBox.transform.InverseTransformPoint (transform.position)));
							Clones [i] [j].self.transform.rotation = Clones [i] [j].Original.transform.rotation;
						}catch(System.Exception e){
							//print (i + "  " + Clones.Count + "  " + j + "  " + turnZones.Count );	
						}
					}
				}
			}
		}

		public void OnDisable ()
		{
				for (int i = Clones.Count-1; i >=0; i--) {
						for (int j = Clones[i].Count-1; j >=0; j--) {
								Destroy (Clones [i] [j].self.gameObject);
								Clones [i].RemoveAt (j);
						}
				}
		}

		public void OnDestroy ()
		{

	
				for (int i = Clones.Count-1; i >=0; i--) {
						for (int j = Clones[i].Count-1; j >=0; j--) {
								Destroy (Clones [i] [j].self.gameObject);
								Clones [i].RemoveAt (j);
						}
				}
		
		}
	
		
		
		
}
	


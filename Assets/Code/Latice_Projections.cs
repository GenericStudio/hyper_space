using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Latice_Projections :MonoBehaviour
{
		private Hub hub;
		public List<List<CloneProperties>> Clones = new List<List<CloneProperties>> ();
		private ZoneController zoneController;
		public LaticeController latice;
		public List<List<Quaternion>> turnZones;
		public List<Quaternion> visableZones;
	public int laticeIterations = 3;
		
		public void Start ()
		{
				hub = GameObject.Find ("hub").GetComponent<Hub> ();
				latice = hub.latice;
				turnZones = new List<List<Quaternion>> ();
				zoneController = hub.zoneController;
		}

		public void Update ()
		{
		if(latice.Active){

			for (int i = 0; i < laticeIterations; i++) {
						if (turnZones.Count <= i) {
								turnZones.Add (new List<Quaternion> ());
						}
			
				int zones =40;

					
				 
						
				turnZones [i] = zoneController.MasterZoneList [i].Take (Mathf.Min (zones,zoneController.MasterZoneList [i].Count)).ToList ();
						
				}
			for (int i = 0; i < laticeIterations; i++) {

						if (i >= Clones.Count) {
							//	print ("Creating Clone index i = " + i);
								Clones.Add (new List<CloneProperties> ());
						}
						while (Clones[i].Count<turnZones[i].Count) {
							//	print ("Creating Clone  count = " + Clones [i].Count);
								GameObject Clone = new GameObject ();
								Clone.AddComponent<MeshRenderer> ();
								Clone.renderer.material = renderer.sharedMaterial;
								Clone.AddComponent<MeshFilter> ();
					CloneProperties CloneObj = Clone.AddComponent<CloneProperties>();
								
								Clone.GetComponent<MeshFilter> ().sharedMesh =GetComponent<MeshFilter> ().mesh;
								Clone.name = this.gameObject.name + "_Projection:" + i;
								CloneObj.Original = transform;
								CloneObj.self = Clone;
								Clone.transform.localScale = transform.localScale;
								Clones [i].Add (CloneObj);	
						}
				}
			for (int i = 0; i < laticeIterations; i++) {
						while (Clones[i].Count>turnZones[i].Count) {
					CloneProperties dis_clone = Clones[i].ElementAt(Clones[i].Count-1);
								Clones[i].RemoveAt (Clones[i].Count-1);
					Destroy (dis_clone.self.gameObject);
								
						}
				}

			for (int i = 0; i < laticeIterations; i++) {
						for (int j = 0; j < Clones[i].Count; j++) {
					if(Time.frameCount%(i*2)==0 || j<6){
								Vector3 Offset = new Vector3 (turnZones [i] [j].x, turnZones [i] [j].y, turnZones [i] [j].z);
								Clones [i] [j].self.transform.position = latice.LaticeBox.transform.localToWorldMatrix.MultiplyPoint (Offset + latice.LaticeBox.transform.InverseTransformPoint (transform.position));
								Clones [i] [j].self.transform.rotation = Clones [i] [j].Original.transform.rotation;
						Clones[i][j].self.transform.localScale = Clones[i][j].Original.transform.localScale;
					}
						
				}
			}
		}else{
			for (int i = Clones.Count-1; i >=0; i--) {
				for (int j = Clones[i].Count-1; j >=0; j--) {
					Destroy (Clones [i] [j].self.gameObject);
					Clones [i].RemoveAt (j);
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
	


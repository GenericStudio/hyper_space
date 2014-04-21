using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BulletProjections :MonoBehaviour
{
	private Hub hub;
	public CloneProperties Clone_Props;
	public Vector3 startZone;
	public int Iterations;
	public int maxZones = 1;
	public Looper Looper;
	
	void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();

		Looper =  GetComponent<Looper>();
		

		startZone = hub.player.GetComponent<Looper>().LoopCounter;
		GameObject Projection = new GameObject ();
		Clone_Props = Projection.AddComponent<CloneProperties>();
		Projection.AddComponent <MeshRenderer> ();
		Projection.renderer.material = renderer.material;
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		Projection.AddComponent<MeshFilter> ().mesh = mesh;
		renderer.enabled=false;;
		Projection.transform.localScale = transform.localScale;
		Projection.name = this.gameObject.name + "_Projection";
		Clone_Props.Original = transform;
		Clone_Props.self = Projection;
		renderer.enabled=false; 
		Vector3 loop = Looper.LoopCounter;
		if(Clone_Props!=null){
			Vector3 Offset = loop  - (hub.player.GetComponent<Looper>().LoopCounter-startZone);
			Clone_Props.self.transform.position = hub.LaticeBox.transform.localToWorldMatrix.MultiplyPoint (Offset + hub.LaticeBox.transform.InverseTransformPoint (transform.position));
		}
	}
	public void Update ()
	{
		
		
		Vector3 loop = Looper.LoopCounter;
		if(Clone_Props!=null){
			Vector3 Offset = loop  - (hub.player.GetComponent<Looper>().LoopCounter-startZone);
			Clone_Props.self.transform.position = hub.LaticeBox.transform.localToWorldMatrix.MultiplyPoint (Offset + hub.LaticeBox.transform.InverseTransformPoint (transform.position));
		}
	}
	
	public void OnDestroy ()
	{
		if(Clone_Props!=null)
			Destroy (Clone_Props.self.gameObject);
	}
}
	


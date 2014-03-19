using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BulletProjections :MonoBehaviour
{
	private Hub hub;
	public CloneProperties Clone_Props;
	public LaticeController latice;
	public ZoneController zoneController;
	public Vector3 startZone;
	public int Iterations;
	public int maxZones = 1;
	public Looper Looper;
	
	void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();
		latice = hub.latice;
		Looper =  GetComponent<Looper>();
		
		zoneController = hub.player.GetComponent<ZoneController>();
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
		
		
	}
	public void Update ()
	{
		
		
		Vector3 loop = Looper.LoopCounter;
		if(renderer.enabled){
			if(Mathf.Abs(loop.x)>=0.5f||Mathf.Abs(loop.y)>=0.5f||Mathf.Abs(loop.z)>=0.5f){
				renderer.enabled=false;
			}
		}
		if(Clone_Props!=null){
			Vector3 Offset = loop  - (hub.player.GetComponent<Looper>().LoopCounter-startZone);
			Clone_Props.self.transform.position = latice.LaticeBox.transform.localToWorldMatrix.MultiplyPoint (Offset + latice.LaticeBox.transform.InverseTransformPoint (transform.position));
		}
	}
	
	public void OnDestroy ()
	{
		if(Clone_Props!=null)
			Destroy (Clone_Props.self.gameObject);
	}
}
	


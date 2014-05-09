using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class LaticeController : MonoBehaviour {
	  

	private Hub hub;
	public GameObject player;
	public ZoneController zoneController;

	public int ArenaSize = 0;
	public int Actual_Iterations= 5;
	public int _max_Iterations = 10;

	public bool Active = false;
	public GameObject LaticeBox;
	public HashSet<GameObject> LaticeObjectManager;
	public HashSet<GameObject> OutsideLaticeObjectManager;
	Vector3 FrameVelocity = Vector3.zero;
	private List<float> smooth_fps = new List<float>(){25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f,25f};




	// Use this for initialization
	void Start () {

		//.position=transform.position+ ArenaSize*.4f* hub.cam.transform.forward;
		
		hub = GameObject.Find("hub").GetComponent<Hub>();
	
		player = hub.player;
		LaticeBox = hub.LaticeBox;
		LaticeBox.transform.localScale = new Vector3(ArenaSize,ArenaSize,ArenaSize);
		MeshFilter BaseMeshFilter = LaticeBox.GetComponent<MeshFilter>() as MeshFilter;
		Mesh mesh = BaseMeshFilter.mesh;
		
		//reverse triangle winding
		int[] triangles= mesh.triangles;
		
		int numpolies = triangles.Length / 3;
		for(int t=0;t < numpolies;t++)
		{
			int tribuffer = triangles[t*3];
			triangles[t*3]=triangles[(t*3)+2];
			triangles[(t*3)+2]=tribuffer;
		}
		
		//readjust uv map for inner sphere projection
		Vector2[] uvs = mesh.uv;
		for(int uvnum = 0;uvnum < uvs.Length;uvnum++)
		{
			uvs[uvnum]=new Vector2(1-uvs[uvnum].x,uvs[uvnum].y);
		}
		
		//readjust normals for inner sphere projection 
		Vector3[] norms = mesh.normals;       
		for(int normalsnum = 0;normalsnum < norms.Length;normalsnum++)
		{
			norms[normalsnum]=-norms[normalsnum];
		}
		
		//copy local built in arrays back to the mesh
		mesh.uv = uvs;
		mesh.triangles=triangles;
		mesh.normals=norms;    
	
		if(Active) LaunchLatice();
	//latice = hub.latice;
	
	
	
}
public void LaunchLatice(){

		Active=true;
		player.GetComponent<Looper>().LoopCounter=new Vector3();
		LaticeBox.renderer.material.SetFloat ("_Cutoff", 1);
		LaticeObjectManager = new HashSet<GameObject>();
		OutsideLaticeObjectManager = new HashSet<GameObject>();
	
	
		LaticeBox.transform.position=player.transform.position;
		LaticeBox.transform.rotation = player.transform.rotation;

		Collider[] objects = Physics.OverlapSphere(LaticeBox.transform.position,10000000);
		FrameVelocity = player.rigidbody.velocity;


		for(int i = objects.Length-1;i>-1;i--){
			GameObject obj = objects[i].gameObject;
			if(obj.GetComponent<Tags>()!=null && obj.GetComponent<Tags>().Looping){
			Vector3 pos = LaticeBox.transform.InverseTransformPoint(obj.transform.position);

			if(Mathf.Abs (pos.x)<.5&&Mathf.Abs(pos.y)<.5&&Mathf.Abs (pos.z)<.5){
				AddObjectToLatice(obj.gameObject);
					if(obj.rigidbody!=null)
						obj.rigidbody.velocity -=  FrameVelocity;
			}else{
					OutsideLaticeObjectManager.Add (obj);
					obj.gameObject.SetActive(false); 
				
			}
		}
		}
	
		player.rigidbody.velocity=Vector3.zero;




	}
	public void AddObjectToLatice(GameObject obj){
		if(Active){
			LaticeObjectManager.Add (obj);
		}
	}
	public void DestroyLatice(){
		Active = false;
		for(int i = LaticeObjectManager.Count-1 ; i >-1 ;i--){
			if(LaticeObjectManager.ElementAt(i) != null){
				if(LaticeObjectManager.ElementAt(i).rigidbody!=null) 
					LaticeObjectManager.ElementAt(i).rigidbody.velocity+=FrameVelocity;
				LaticeObjectManager.Remove(LaticeObjectManager.ElementAt(i));
			}
		}

		FrameVelocity=Vector3.zero;
		for(int i = OutsideLaticeObjectManager.Count-1 ; i >-1 ;i--){
			if(LaticeObjectManager.ElementAt(i) != null){
				LaticeObjectManager.ElementAt(i).SetActive(true);
				OutsideLaticeObjectManager.Remove(LaticeObjectManager.ElementAt(i));
			}
		}


		LaticeBox.SetActive(false);


	}



	void EarlyUpdate(){


		for (int i = LaticeObjectManager.Count-1; i > 0; i--){
			if(LaticeObjectManager.ElementAt(i)==null || LaticeObjectManager.ElementAt(i).rigidbody==null) LaticeObjectManager.Remove(LaticeObjectManager.ElementAt(i));
		}

		/*if(!Active && Input.GetKey(KeyCode.LeftControl)){
			LaticeBox.SetActive(true);
			LaticeBox.transform.position   = player.transform.position;
			LaticeBox.transform.rotation   = player.transform.rotation;
			Time.timeScale=0.25f;
			LaticeBox.renderer.material.SetFloat ("_Cutoff", 0);
		}else if(Time.timeScale <1){
			Time.timeScale=1;
		}
		if(Input.GetKeyUp(KeyCode.LeftControl)){
			if(!Active){
				LaunchLatice();
				Active = true;
			}else{
				DestroyLatice();
				Active = false;
			}
		}
		
*/

	}
	public void Update(){
		hub.LaticeBox.transform.position=transform.position+ ArenaSize*.4f* hub.cam.transform.forward;
		smooth_fps[Time.frameCount %smooth_fps.Count] = Time.deltaTime;
		if(Time.frameCount%10 == 0){
			var fps = (int)smooth_fps.Count/smooth_fps.Sum();
			//print (fps);
			if(Actual_Iterations > _max_Iterations/2 && fps < 30) Actual_Iterations--;
			else if(Actual_Iterations < _max_Iterations && fps>60) Actual_Iterations++;
		}
	}

}
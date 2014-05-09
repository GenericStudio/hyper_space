using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
public class PlayerController : MonoBehaviour {



	float sensitivity = 80f;
	public float moveSpeed;
	private float EngineForce = 50f;
	public int energy = 0;
	public int maxEnergy = 100;
	public int energyRechargeRate = 1;
	private GUIText EnergyDisplay;
	private Vector3 desiredDirection;
	public ParticleSystem EngineEffect;


	private Hub hub;

	Vector3 velocity = new Vector3();

	void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();
		EnergyDisplay = GameObject.Find ("ShipEnergyMeter").GetComponent<GUIText>();

	}
	public struct placement{
		public GameObject dis_obj;
		public Vector3 oldPos;
	}
	void Update(){
		desiredDirection = Vector3.zero;
		if(energy<maxEnergy)
			energy+=energyRechargeRate;
		EngineForce=moveSpeed;
		if(Input.GetKey(KeyCode.LeftShift))
			EngineForce*=2;
		EnergyDisplay.text = "" + hub.latice.LaticeObjectManager.Count();
		if(Input.GetAxis("Mouse ScrollWheel") > 0 && hub.cam.fieldOfView > 25){
			hub.cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") *10;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0 && hub.cam.fieldOfView < 90){
			hub.cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") *10;
		}
		if(Input.GetAxis("Zoom")< -0.25f  ){
			if(hub.cam.fieldOfView >45)
				hub.cam.fieldOfView *=.9f;
		}
		else if(Input.GetAxis("Zoom") > 0.25f  ){
			if(hub.cam.fieldOfView <90){
			hub.cam.fieldOfView *=1.1f;
			}
		}
		else if( Mathf.Abs (hub.cam.fieldOfView-60)>5){
			hub.cam.fieldOfView = (hub.cam.fieldOfView + 60) /2;
		}
		if ( Input.GetAxis("Vertical")<-0.1f) {
			desiredDirection += transform.forward;
		}
		if ( Input.GetAxis("Vertical")>0.1f) {
			desiredDirection -= transform.forward;
		}
		if ( Input.GetAxis("Horizontal")<-0.1f) {
			desiredDirection -= 0.5f*transform.right;
		}
		if (Input.GetAxis("Horizontal")>0.1f) {
			desiredDirection += 0.5f*transform.right;
		}
		EngineEffect.startSpeed=Mathf.Max(2,desiredDirection.magnitude*10);
		/*
		if (Input.GetKey (KeyCode.Minus) && hub.LaticeBox.transform.lossyScale.x<20000) {
			List<placement> posMap = new List<placement>();
			foreach( GameObject obj in hub.latice.LaticeObjectManager){
				if(obj!=null)
					posMap.Add (new placement(){ oldPos = hub.LaticeBox.transform.InverseTransformPoint(obj.transform.position), dis_obj = obj  });
			}
			hub.LaticeBox.transform.localScale*=1.01f;
			foreach(placement obj in posMap){
				if(obj.dis_obj != null)
					obj.dis_obj.transform.position = hub.LaticeBox.transform.localToWorldMatrix.MultiplyPoint(obj.oldPos);
			}
		}
		if (Input.GetKey (KeyCode.Equals)&& hub.LaticeBox.transform.lossyScale.x>1000) {
			List<placement> posMap = new List<placement>();
			foreach( GameObject obj in hub.latice.LaticeObjectManager){
				if(obj!=null)
					posMap.Add (new placement(){ oldPos = hub.LaticeBox.transform.InverseTransformPoint(obj.transform.position), dis_obj = obj  });
			}
			hub.LaticeBox.transform.localScale*=0.99f;
			foreach(placement obj in posMap){
				if(obj.dis_obj != null)
					obj.dis_obj.transform.position = hub.LaticeBox.transform.localToWorldMatrix.MultiplyPoint(obj.oldPos);
			}
		}
		*/
		if (Input.GetKey (KeyCode.P) && hub.LaticeBox.transform.lossyScale.x<10000) {
			hub.LaticeBox.transform.localScale*=1.01f;
		}
		if (Input.GetKey (KeyCode.O)&& hub.LaticeBox.transform.lossyScale.x>100) {
			hub.LaticeBox.transform.localScale*=0.99f;
		}
		transform.rotation = Quaternion.Slerp(transform.rotation,hub.cam.transform.rotation,0.1f);
		if(Input.GetKey (KeyCode.LeftShift)||((rigidbody.velocity + desiredDirection).magnitude < moveSpeed*5||(rigidbody.velocity + desiredDirection).magnitude < rigidbody.velocity.magnitude ))
			rigidbody.AddForce(desiredDirection*EngineForce*2000*Time.deltaTime);
	}


		public void OnCollisionEnter(Collision col) {
		//rigidbody.velocity = -rigidbody.velocity/2;
		if(!GetComponentInChildren<Shield>().shieldUp){
			if(col.gameObject.GetComponent<Harmfull>()){
		
				
					//Destroy (gameObject,2f);
				}
			}
		}
		public void OnDestroy(){
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	




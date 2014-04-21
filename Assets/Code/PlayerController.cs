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
	public int antiGravityRadius = 100;
	public int antiGravityStrength = 100;
	public float moveSpeed;
	private float _moveSpeed = 50f;
	public int energy = 0;
	public int maxEnergy = 100;
	public int energyRechargeRate = 1;
	private GUIText EnergyDisplay;
	private Vector3 desiredDirection;


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
 
	

	//	float dt = sensitivity * Time.deltaTime;
	//	float dx = -Input.GetAxis("Mouse Y") * dt;
	//	float dy = Input.GetAxis("Mouse X") * dt;
	//	float dz = 0f;



	
		desiredDirection = Vector3.zero;
		if(energy<maxEnergy)
			energy+=energyRechargeRate;


		EnergyDisplay.text = "" + energy;
	//	if(Input.GetAxis("Mouse ScrollWheel") > 0 && hub.cam.fieldOfView >25){
		//	hub.cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") *10;
		//}
	//	if(Input.GetAxis("Mouse ScrollWheel") < 0 && hub.cam.fieldOfView <90){
	//		hub.cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") *10;
	//	}
		if (Input.GetKey (KeyCode.LeftShift)) {
			_moveSpeed = moveSpeed * 20;
		} else {
			_moveSpeed = moveSpeed*10;		
		}

		if (Input.GetKey (KeyCode.W)|| Input.GetAxis("Vertical")>0.1f) {
			desiredDirection += transform.forward;
		}
		if (Input.GetKey (KeyCode.S)|| Input.GetAxis("Vertical")<-0.1f) {
			desiredDirection -= transform.forward;
		}
		if (Input.GetKey (KeyCode.A)) {
			desiredDirection -= transform.right;
		}
		if (Input.GetKey (KeyCode.D)) {
			desiredDirection += transform.right;
		}
		if (Input.GetKey (KeyCode.Q)) {
		//	dz=1f;
		}
		if (Input.GetKey (KeyCode.E)) {
		//	dz=-1f;
		}


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
		if (Input.GetKey (KeyCode.P) && hub.LaticeBox.transform.lossyScale.x<10000) {
			hub.LaticeBox.transform.localScale*=1.01f;
		}
		if (Input.GetKey (KeyCode.O)&& hub.LaticeBox.transform.lossyScale.x>100) {
			hub.LaticeBox.transform.localScale*=0.99f;
		}


		//Vector3 rotationVector = new Vector3(dx,dy,dz);
		//rotationVector*=Time.deltaTime*10;
		//transform.Rotate(dx, dy, dz);

		transform.rotation = Quaternion.Slerp(transform.rotation,hub.cam.transform.rotation,0.15f);


		if((rigidbody.velocity + desiredDirection).magnitude < 5000||(rigidbody.velocity + desiredDirection).magnitude < rigidbody.velocity.magnitude )
			rigidbody.AddForce(desiredDirection*_moveSpeed*1000*Time.deltaTime);
	

	}


		public void OnCollisionEnter(Collision col) {
		rigidbody.velocity = -rigidbody.velocity/2;
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
	




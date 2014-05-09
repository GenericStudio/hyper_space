using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour {
	
	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	
	public float distanceMin = .5f;
	public float distanceMax = 15f;
	
	public float x = 0.0f;
	float y = 0.0f;
	
	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change 
	}
	
	void LateUpdate () {
		if (target) {
				float dt = 10 * Time.deltaTime;
				//float dx = -Input.GetAxis("Mouse Y") * dt;
				//float dy = Input.GetAxis("Mouse X") * dt;

			float dx = Input.GetAxis("Pitch");
			float dy = Input.GetAxis("Yaw");
			float dz = Input.GetAxis("Roll");



			                                                                   
	
			Vector3 rotationVector = new Vector3(dx,dy,dz);
			rotationVector*=Time.deltaTime;

			transform.Rotate(dx, dy, dz);
			Quaternion rotation =  transform.rotation;;
			
			//distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
			
			//RaycastHit hit;
			//if (Physics.Linecast (target.position, transform.position, out hit)) {
			//	distance -=  hit.distance;
			//}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;
			transform.position = position;
		}
	}
	
	public  float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F){
			angle += 360F;

		}
		if (angle > 360F){
			angle -= 360F;
		}
		return Mathf.Clamp(angle, min, max);
	}
	
	
}
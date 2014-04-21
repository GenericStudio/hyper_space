﻿using UnityEngine;
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
		
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
	
	void LateUpdate () {
		if (target) {
		float 	x_raw = Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
		
			y -= Input.GetAxis("Mouse Y") * ySpeed * distance*0.02f;
			//x+=x_raw;

//			print (y);
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			if(y>270 || y<90) x+=x_raw;
			else x-=x_raw;
			Quaternion rotation =  Quaternion.Euler(y,x,0);
			
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
			
			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) {
				distance -=  hit.distance;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;
			
			transform.rotation = Quaternion.Lerp(transform.rotation,rotation,0.999f);
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
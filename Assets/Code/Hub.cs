using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Hub : MonoBehaviour {
	public GameObject LaticeBox;
	public Camera cam;
	public GameObject player;
	public ZoneController zoneController;
	public LaticeController latice;
	public int GlobalGravity = 9;
	public void Start(){
		Screen.lockCursor = true;
	}
	void OnMouseDown() {
		Screen.lockCursor = true;
	}

}

using UnityEngine;
using System.Collections;

public class Harmfull : MonoBehaviour {
	private Hub hub;
	// Use this for initialization
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

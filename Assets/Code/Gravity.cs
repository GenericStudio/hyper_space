
using UnityEngine;
using System.Collections;
		                                                               
public class Gravity : MonoBehaviour
{
	private Hub hub;
		private int range;
			
		// Use this for initialization
		void Start ()
		{
		hub = GameObject.Find ("hub").GetComponent<Hub> ();
		}
			
		// Update is called once per frame
		void FixedUpdate ()
		{
				if(!hub.latice.Active){
					rigidbody.AddForce (hub.GlobalGravity * (-transform.position.normalized) * rigidbody.mass);
			}
		}
}
		


using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {
	private Hub hub;
	private GameObject Player ;
	public Link front;
	public Link back;
	public float spaceing;
	public float slipperiness = 0.5f;
	public float MagneticDelay = 0;
	private int prevCount;
	public float magnetRange;
	// Use this for initialization
	void Start () {
		hub = GameObject.Find ("hub").GetComponent<Hub>();
		Player = GameObject.Find("Player");
		renderer.material.color = new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f));
	

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!front){
		Collider[] proximal = Physics.OverlapSphere(transform.position,magnetRange);
		foreach(Collider hit in proximal){
					if(hit.gameObject != this.gameObject && hit.GetComponent<Link>()){
				float distanceToHit = Vector3.Distance(hit.transform.position,this.transform.position);
				if(front == null || distanceToHit < Vector3.Distance(front.transform.position,this.transform.position)){
					Link temp = hit.GetComponent<Link>();
					bool valid = true;
					while (temp!=null){
						if(temp == this){
							valid = false;
						}
						temp = temp.front;
					}
					if(valid){
				
						front = hit.GetComponent<Link>();
						front.back=this;
					}
				}
			}
		}
		}
		if(front){
			if(back == null){

			}else{
			float distToFront = Vector3.Distance(front.transform.position,transform.position);
			if(distToFront<magnetRange){
				transform.LookAt(front.transform.position);
				rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, (front.transform.position - (front.transform.forward*spaceing)-transform.position)+front.rigidbody.velocity,slipperiness);
				GetComponent<LineRenderer>().useWorldSpace=true;
				GetComponent<LineRenderer>().SetPosition(0,transform.position);
				GetComponent<LineRenderer>().SetPosition(1,front.transform.position);
				transform.LookAt(front.transform.position);
			}else{
				front.back=null;
				front = null;
			}
			}
			

			
			}
		else if(back){

			float distToPlayer = Vector3.Distance(Player.transform.position,transform.position);
			if(distToPlayer<magnetRange*5){
			transform.LookAt(Player.transform.position);

			GetComponent<LineRenderer>().useWorldSpace=false;
			GetComponent<LineRenderer>().SetPosition(0,Vector3.zero);
			GetComponent<LineRenderer>().SetPosition(1,Vector3.forward*5);
					
			rigidbody.velocity = Vector3.Lerp(rigidbody.velocity.normalized*30, transform.forward*30,0.9f);
				}else{
				rigidbody.velocity = Vector3.Lerp(rigidbody.velocity,Vector3.zero,0.2f);
			}
			}else{
				GetComponent<LineRenderer>().useWorldSpace=false;
				GetComponent<LineRenderer>().SetPosition(0,Vector3.zero);
				GetComponent<LineRenderer>().SetPosition(1,transform.forward);
			}
		}
		                                                              // rigidbody.AddForce(gameController.GlobalGravity*(-transform.position.normalized) * rigidbody.mass);
		                                                               
		                                                               
		                                                               
		                                                               
		                                                               
		                                                               }
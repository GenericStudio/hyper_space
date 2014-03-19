using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Shield: MonoBehaviour
{
	private Hub hub;
	private PlayerController player;
	public int ShieldEnergyCost = 5;
	public bool shieldUp = false;
	// Update is called once per frame
	void Start(){
		hub = GameObject.Find("hub").GetComponent<Hub>();
		player = GameObject.Find("Player").GetComponent<PlayerController>();

	
		
	}
	void Update ()
	{
		if(shieldUp)
			shieldUp = false;


		if (Input.GetKey(KeyCode.Space)) {
			if(player.energy>0)
				player.energy-=ShieldEnergyCost;
			if(player.energy>ShieldEnergyCost){
				player.energy-=ShieldEnergyCost;
				renderer.material.mainTextureOffset += new Vector2(-.01f,-.001f);
				renderer.enabled = true;
				shieldUp = true;

			
					List<Collider> col = Physics.OverlapSphere(transform.position,hub.latice.ArenaSize).ToList();
					Vector3 averageVelo =Vector3.zero;
					int count = 0;
					foreach(Collider co in col){
						if(co.rigidbody!=null && co.GetComponent<BulletScript>()==null){
							averageVelo +=co.rigidbody.velocity;
							count++;
						}
					}
					
					averageVelo = averageVelo/ count;
					
					
					
					if(count>1){
					player.rigidbody.velocity = Vector3.Lerp (player.rigidbody.velocity, averageVelo,0.07f);
				player.rigidbody.inertiaTensorRotation= new Quaternion();

				}else{
					player.rigidbody.velocity = Vector3.Lerp (player.rigidbody.velocity, Vector3.zero,0.1f);
				}

			}else{
				renderer.enabled = false;
				}


		}else{
			renderer.enabled = false;
		}
		
	}
}

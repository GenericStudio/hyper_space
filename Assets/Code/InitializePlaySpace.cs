using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class InitializePlaySpace:MonoBehaviour {
	public int AsteroidSize = 5;
	public int Num_Asteroids = 2;
	public List<GameObject> Asteroids; 
	public GameObject Asteroid;
	private Hub hub;
	public bool OrbitingThis = false;
	public bool square = true;

	
	// Use this for initialization
	void Start () {
		hub = GameObject.Find("hub").GetComponent<Hub>();




		Asteroids = new List<GameObject>(); 
		if(!square){
			for (int x = 0; x < Num_Asteroids; x++) {
				Asteroids.Add(SpawnAsteroid(x.ToString()));
			}
		}else{
			Num_Asteroids*=2;
			Num_Asteroids++;
			int arenaSize = hub.latice.ArenaSize;
			int spaceing = (int)(2*arenaSize/Num_Asteroids);
			for(int i = 3 ; i < Num_Asteroids-2;i++){
				for(int j = 3 ; j < Num_Asteroids-2;j++){
					for(int k = 3 ; k < Num_Asteroids-2;k++){
						if(i!=0 && k!=0&&j!=0){
						GameObject cube = Instantiate(Asteroid) as GameObject;
						int size = Random.Range((AsteroidSize/4),AsteroidSize);

						cube.transform.localScale = new Vector3(size,size,size);
						cube.transform.position = new Vector3(-arenaSize + spaceing*i,-arenaSize + spaceing*k,-arenaSize + spaceing*j);
						}
					}
				}
			}
		}
	}



	public GameObject SpawnAsteroid(string x){
		GameObject cube = Instantiate(Asteroid) as GameObject;
		int size = Random.Range((AsteroidSize/4+1),AsteroidSize);

		cube.transform.localScale = new Vector3(size,size,size);


		if(OrbitingThis){
			Vector2 circle = Random.insideUnitCircle.normalized * hub.latice.ArenaSize;
			cube.transform.position = transform.position + new Vector3(circle.y,circle.x,0);
			cube.transform.LookAt(transform.position);
		//	cube.rigidbody.velocity = transform.right* hub.GlobalGravity  * (1/cube.transform.position.magnitude);

		}else{
			cube.transform.position = transform.position + Random.onUnitSphere * hub.latice.ArenaSize/2;
			cube.transform.LookAt(transform.position);
		//	cube.rigidbody.velocity = Random.insideUnitSphere * Random.Range(0,100);



		}

		cube.name = "Asteroid:"+x;
		return cube;
		
	}
}


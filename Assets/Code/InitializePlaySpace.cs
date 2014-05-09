using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class InitializePlaySpace:MonoBehaviour {
	public int AsteroidSize = 5;
	public int Num_Asteroids = 2;
	public List<GameObject> Asteroids; 
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
			int spaceing = (int)(2*arenaSize/Num_Asteroids+1);
			for(int i = 3 ; i < Num_Asteroids-2;i++){
				for(int j = 3 ; j < Num_Asteroids-2;j++){
					for(int k = 3 ; k < Num_Asteroids-2;k++){
						if(i!=0 && k!=0&&j!=0){
						GameObject cube = Instantiate(hub.asteroid) as GameObject;
						cube.transform.localScale = new Vector3(AsteroidSize,AsteroidSize,AsteroidSize);
						//cube.rigidbody.mass=AsteroidSize*10;
							cube.rigidbody.mass=AsteroidSize;
							cube.name = "Asteroid"+i+j;

						cube.transform.position = new Vector3(-arenaSize + spaceing*i,-arenaSize + spaceing*k,-arenaSize + spaceing*j)*0.9f;
						
						}
					}
				}
			}
		}
	}
	public GameObject SpawnAsteroid(string x){
		GameObject cube = Instantiate(hub.asteroid) as GameObject;
		cube.rigidbody.mass=AsteroidSize;
		cube.rigidbody.velocity=Random.onUnitSphere*Random.Range(10,250);
		cube.transform.localScale = new Vector3(AsteroidSize,AsteroidSize,AsteroidSize);
		if(OrbitingThis){
			Vector2 circle = Random.insideUnitCircle.normalized * hub.latice.ArenaSize;
			cube.transform.position = transform.position + new Vector3(circle.y,circle.x,0);
			cube.transform.LookAt(transform.position);
		}else{
			cube.transform.position = transform.position + Random.onUnitSphere * hub.latice.ArenaSize/2;
			cube.transform.LookAt(transform.position);
		}
		cube.name = "Asteroid:"+x;
		return cube;
	}
}


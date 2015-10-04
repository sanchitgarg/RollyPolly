using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public GameObject brickwall;
	public float zSpeed;

	// Use this for initialization
	void Start () {
		spawnObstacle();
		zSpeed = -10.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnObstacle()
	{
		Vector3 position = new Vector3(0.0f, 3.0f, Random.Range(60.0F, 80.0F));
		Instantiate(brickwall, position, Quaternion.identity);
	}
}

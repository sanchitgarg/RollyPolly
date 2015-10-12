using UnityEngine;
using System.Collections;

public class PathScript: MonoBehaviour {

	public GameObject pathObject;
	GameManagerScript gameManager;
	public float zSpeed;
	bool generate;

	// Use this for initialization
	void Start () {
		//zSpeed = -100;
		var rb = GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (0, 0, zSpeed);
		generate = true;

		GameObject g = GameObject.Find ("GameManager");
		gameManager = g.GetComponent<GameManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision)
	{
		Collider c = collision.collider;

		if (c.CompareTag ("CreatePathColliderTag") && generate) {
			generate = false;
			Vector3 position = new Vector3(0,0, 80);
			Instantiate(pathObject, position, Quaternion.identity);
			gameManager.spawnObstacle();
		}

		else if (c.CompareTag ("DestroyPathColliderTag")) {
			Destroy(gameObject);
		}
	}
}

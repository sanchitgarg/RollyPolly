using UnityEngine;
using System.Collections;

public class ObstacleMotionScript : MonoBehaviour {

	GameManagerScript gameobj;
	// Use this for initialization
	void Start () {
		var rb = GetComponent<Rigidbody> ();
		GameObject g = GameObject.Find ("GameManager");
		gameobj = g.GetComponent<GameManagerScript>();
		rb.velocity = new Vector3 (0, 0, gameobj.zSpeed);
//		Debug.Log (gameobj.zSpeed);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		Collider c = collision.collider;
		
		if (c.CompareTag ("DestroyPathColliderTag") || c.CompareTag ("PlayerTag")) {
			//Debug.Log("Smashed");
			Destroy(gameObject);
		}
	}
}

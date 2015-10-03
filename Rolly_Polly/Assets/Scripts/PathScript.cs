using UnityEngine;
using System.Collections;

public class PathScript: MonoBehaviour {

	public GameObject pathObject;
	public float zSpeed;

	// Use this for initialization
	void Start () {
		//zSpeed = -100;
		var rb = GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (0, 0, zSpeed);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision)
	{
		Collider c = collision.collider;

		if (c.CompareTag ("CreatePathColliderTag")) {
			Vector3 position = new Vector3(0,0, 50);
			Instantiate(pathObject, position, Quaternion.identity);
		}

		else if (c.CompareTag ("DestroyPathColliderTag")) {
			Destroy(gameObject);
		}
	}
}

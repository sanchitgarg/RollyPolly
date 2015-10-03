using UnityEngine;
using System.Collections;

public class PlayerBallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		
		if (Input.GetKey ("left")) {
			Vector3 position = this.transform.position;
			position.x = position.x - 0.5f;
			this.transform.position = position;
		}
		
		
		if (Input.GetKey ("right")) {
			
			Vector3 position = this.transform.position;
			position.x = position.x + 0.5f;
			this.transform.position = position;
		}
		
		
		
		
		
		//GetComponent<Rigidbody>().position = new Vector3 (Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xmin, boundary.xmax), 0.0f, Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zmin, boundary.zmax)); 
		
	}
}

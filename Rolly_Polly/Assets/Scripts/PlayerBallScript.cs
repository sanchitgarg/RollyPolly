using UnityEngine;
using System.Collections;

public class PlayerBallScript : MonoBehaviour {

	public float thrust;
	bool canJump;

	GameManagerScript globalObj;
	AudioManagerScript AudioManager;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager"); 
		globalObj = g.GetComponent< GameManagerScript >(); 

		GameObject a = GameObject.Find ("AudioManager");
		AudioManager = a.GetComponent<AudioManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		
		if (Input.GetKey ("left")) {

			//var rb = gameObject.GetComponent<Rigidbody>();
			//rb.AddForce(-5, 0, 0);

			Vector3 position = this.transform.position;
			position.x = position.x - 0.1f;
			this.transform.position = position;
		}
		
		
		if (Input.GetKey ("right")) {
			
			//var rb = gameObject.GetComponent<Rigidbody>();
			//rb.AddForce(5, 0, 0);

			Vector3 position = this.transform.position;
			position.x = position.x + 0.1f;
			this.transform.position = position;
		}

		if(Input.GetKeyDown ("space") && canJump)
		{
			//Debug.Log("space down");
			canJump = false;

			var rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(new Vector3(0, thrust, 0));
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		Collider c = collision.collider;

		if (c.CompareTag ("CoinTag")) {
			globalObj.coinCount ++;
			AudioManager.playCoinSound();
		}
		else if (c.CompareTag ("WallLowerTag") || c.CompareTag ("WallLeftColliderTag") || c.CompareTag ("WallRightColliderTag")) {
			canJump = true;
		}
		else {
			globalObj.health = globalObj.health - 10;
			AudioManager.playObstacleCollideSound();
		}

		var rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = new Vector3 (0, 0, 0);
	}
}

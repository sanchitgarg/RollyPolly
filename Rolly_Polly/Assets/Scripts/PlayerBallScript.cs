using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum BallProperty
{
	Normal,
	Rubber,
	Steel,
	Bubble

};

public class PlayerBallScript : MonoBehaviour {

	public Slider HealthBarSlider;
	public Slider RubberBarSlider;
	public Slider SteelBarSlider;

	public Texture2D rubberball;
	public Texture2D steelball;
	public Texture2D bubbleball;

	public float thrust;
	bool canJump;
	float rotationsPerMinute = 40.0f;
	Vector3 movingup;
	BallProperty property;

	GameManagerScript globalObj;
	AudioManagerScript AudioManager;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager"); 
		globalObj = g.GetComponent< GameManagerScript >(); 

		GameObject a = GameObject.Find ("AudioManager");
		AudioManager = a.GetComponent<AudioManagerScript>();

		property = BallProperty.Normal;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(6.0f*rotationsPerMinute*Time.deltaTime,0.0f,0.0f);
		if (property == BallProperty.Rubber) {
			RubberBarSlider.value -= Time.deltaTime * 0.2f;
			if(RubberBarSlider.value <= 0)
			{
				GetComponent<Renderer>().material.mainTexture = null;
				property = BallProperty.Normal;
			}
		}
		if (property == BallProperty.Steel) {
			SteelBarSlider.value -= Time.deltaTime * 0.2f;
			if(SteelBarSlider.value <= 0)
			{
				GetComponent<Renderer>().material.mainTexture = null;
				property = BallProperty.Normal;
			}
		}
		if (property == BallProperty.Bubble) {
			movingup = this.transform.position;
			movingup.y += 0.2f;
			if(movingup.y>2.0f)
				movingup.y = 2.0f;
			this.transform.position = movingup;
		}
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

		if(Input.GetKeyDown ("space") && canJump && property == BallProperty.Rubber)
		{
			//Debug.Log("space down");
			canJump = false;

			var rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(new Vector3(0, thrust, 0));
		}
		if (Input.GetKey ("1")) {
			GetComponent<Renderer>().material.mainTexture = null;
			property = BallProperty.Normal;
		}
		if (Input.GetKey ("2")) {
			GetComponent<Renderer>().material.mainTexture = rubberball;
			property = BallProperty.Rubber;
		}
		if (Input.GetKey ("3")) {
			GetComponent<Renderer>().material.mainTexture = steelball;
			property = BallProperty.Steel;
		}
		if (Input.GetKey ("4")) {
			GetComponent<Renderer>().material.mainTexture = bubbleball;
			property = BallProperty.Bubble;
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
			if(property!=BallProperty.Steel)
			{
				globalObj.health = globalObj.health - 10;
				HealthBarSlider.value -= 0.1f;
			}

			AudioManager.playObstacleCollideSound();
		}

		var rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = new Vector3 (0, 0, 0);
	}
}

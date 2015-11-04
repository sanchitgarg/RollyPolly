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

	private Vector3 v3Pos;

	float keepBouncing;

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

		var rb = gameObject.GetComponent<Rigidbody>();
		rb.AddTorque (10.0f, 0.0f, 0.0f);

		if (property == BallProperty.Rubber) {
			RubberBarSlider.value -= Time.deltaTime * 0.05f;
			if(RubberBarSlider.value <= 0)
			{
				GetComponent<Renderer>().material.mainTexture = null;
				property = BallProperty.Normal;
			}
		}
		if (property == BallProperty.Steel) {
			SteelBarSlider.value -= Time.deltaTime * 0.05f;
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

			var rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(-4, 0, 0);
			//rb.transform.Rotate(6.0f*rotationsPerMinute*Time.deltaTime,0.0f,0.0f);
			//Vector3 position = this.transform.position;
			//position.x = position.x - 0.1f;
			//this.transform.position = position;
		}
		
		
		if (Input.GetKey ("right")) {
			
			var rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(4, 0, 0);
			//rb.transform.Rotate(6.0f*rotationsPerMinute*Time.deltaTime,0.0f,0.0f);
			//Vector3 position = this.transform.position;
			//position.x = position.x + 0.1f;
			//this.transform.position = position;
		}

		if((Input.GetKeyDown ("space")||Input.GetButtonDown("Rubber_jump")) && canJump && property == BallProperty.Rubber)
		{
			//Debug.Log("space down");
			canJump = false;

			var rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(new Vector3(0, thrust, 0));

			GetComponent<Collider>().material.bounciness = 1.0f;
			keepBouncing = 4.0f;
		}

		if (keepBouncing > 0 && canJump) {
			canJump = false;
			
			var rb = gameObject.GetComponent<Rigidbody>();
//			float b = keepBouncing;
			rb.AddForce(new Vector3(0, thrust / (6.0f - keepBouncing), 0));
			keepBouncing -= 1.0f;

			//GetComponent<Collider>().material.bounciness = 1000.0f;
		}
		if (Input.GetKey ("1")) {
			GetComponent<Renderer>().material.mainTexture = null;
			gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			GetComponent<Rigidbody>().mass = 1.0f;
			property = BallProperty.Normal;
		}
		if (Input.GetKey ("2")) {
			GetComponent<Renderer>().material.mainTexture = rubberball;
			gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
			GetComponent<Rigidbody>().mass = 1.0f;
			property = BallProperty.Rubber;
		}
		if (Input.GetKey ("3")) {
			GetComponent<Renderer>().material.mainTexture = steelball;
			gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			GetComponent<Rigidbody>().mass = 5.0f;
			property = BallProperty.Steel;
		}
//		if (Input.GetKey ("4")) {
//			GetComponent<Renderer>().material.mainTexture = bubbleball;
//			property = BallProperty.Bubble;
//		}
	}

	void OnMouseDown() {
		//	Debug.Log ("Mouse Clicked");
		v3Pos = Input.mousePosition;
	}

	//Implementing Mouse inputs
	// Reference : http://answers.unity3d.com/questions/452018/how-to-get-the-mouse-direction-while-left-click-is.html
	void OnMouseDrag()
	{
		var v3 = Input.mousePosition - v3Pos;
		v3.Normalize();
		var f = Vector3.Dot(v3, Vector3.up);

		f = Vector3.Dot(v3, Vector3.right);
		if (f > 0.005f) {
			//Debug.Log("Right");
			//Debug.Log(f);

			f = Mathf.Clamp(f, 0.5f, 5.0f) * 10.0f;
			var rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(f, 0, 0);

		}
		else if(f < -0.005f) {
			//Debug.Log("Left, ");
			//Debug.Log(f);
		
			f = Mathf.Clamp(f, -5.0f, -0.5f)  * 10.0f;
			var rb = gameObject.GetComponent<Rigidbody>();
			rb.AddForce(f, 0, 0);
		}

		v3Pos = Input.mousePosition;
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
		else if(c.CompareTag("HealthTag"))
		{
			//Debug.Log ("Health reset to 100");
			globalObj.health = 100;
			HealthBarSlider.value = 1.0f;
		}
		else if(c.CompareTag("RubberPowerTag"))
		{
			RubberBarSlider.value = 1.0f;
		}
		else if(c.CompareTag("SteelPowerTag"))
		{
			SteelBarSlider.value = 1.0f;
		}
		else {
			if(property!=BallProperty.Steel)
			{
				globalObj.health = globalObj.health - 10;
				HealthBarSlider.value -= 0.1f;
			}

			if(property == BallProperty.Steel && c.CompareTag("MagnetWallTag"))
			{
				SteelBarSlider.value = 0.0f;
			}
			else if(property == BallProperty.Rubber && c.CompareTag("TetrahedronTag"))
			{
				RubberBarSlider.value = 0.0f;
			}

			AudioManager.playObstacleCollideSound();
		}

		var rb = gameObject.GetComponent<Rigidbody>();
		rb.velocity = new Vector3 (0, 0, 0);
	}
}

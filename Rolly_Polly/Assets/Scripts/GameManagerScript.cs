using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public GameObject brickwall;
	public GameObject cone;
	public GameObject cylinder;
	public GameObject doubleCylinder;
	public GameObject magnet;
	public GameObject cube;
	public GameObject tetrahedron;
	public GameObject coin;

	public GameObject healthobject;
	public GameObject rubber_power_up_object;
	public GameObject steel_power_up_object;

	public float zSpeed;

	public int score;
	public int health;
	public int coinCount;


	// Use this for initialization
	void Start () {
		spawnObstacle();
		zSpeed = -10.0f;
		score = 0;
		coinCount = 0;
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0) {
			Application.LoadLevel("GameEndScene");

		}
		if (score % 500 == 0) {
			spawnbonuses();
		}
	}

	void FixedUpdate ()
	{
		score ++;

	}

	void spawnbonuses()
	{
		int randomNum = Random.Range (0,5);

		float x = Random.Range (-2.0f, 2.0f);
		Vector3 v = new Vector3(0.0f, 1.0f, 73.0f);
		v.x = x;
		switch (randomNum) {
		case 0:
		//Instantiate position
		

		//Health Drop
			Instantiate (healthobject, v,  Quaternion.Euler(0, 30, 0));
			break;

		case 1:Instantiate (rubber_power_up_object, v,  Quaternion.Euler(0, 30.0f, 0));
			break;
		case 2:Instantiate (steel_power_up_object, v,  Quaternion.Euler(0, 30, 0));
			break;

		default:break;

		}
	}

	public void spawnObstacle()
	{
//		
//		Instantiate(cone, position, Quaternion.identity);

		//Instanstiate random obstacles 3 per path.

		ArrayList positions = new ArrayList ();

		positions.Add(new Vector3(0.0f, 1.0f, 63.0f));
		positions.Add(new Vector3(0.0f, 1.0f, 70.0f));
		positions.Add(new Vector3(0.0f, 1.0f, 77.0f));


		//Adding obstacles
		for (int i=0; i<3; ++i) {
			int randomNum = Random.Range (0, 9);

			switch (randomNum) {
//				case 0:
//				{
//					float x = Random.Range (-2.0f, 2.0f);
//					Vector3 v = (Vector3)positions[i];
//					v.x = x;
//					Instantiate (cone, v, Quaternion.identity);
//				break;
//				}

			case 1:
			{
				float x = Random.Range (-2.0f, 2.0f);
				Vector3 v = (Vector3)positions[i];
				v.x = x;
				Instantiate (cube, v, Quaternion.identity);
				break;
			}
			case 2:
			{
				float x = Random.Range (-2.0f, 2.0f);
				Vector3 v = (Vector3)positions[i];
				v.x = x;
				Instantiate (cylinder, v, Quaternion.identity);
				break;
			}

			case 3:
			{
				float x = Random.Range (-2.0f, 2.0f);
				Vector3 v = (Vector3)positions[i];
				v.x = x;
				Instantiate (tetrahedron, v, Quaternion.identity);
				break;
			}

			case 4:
			{
				Vector3 v = (Vector3)positions[i];
				v.y = 0.0f;
				Instantiate (doubleCylinder, v, Quaternion.identity);
				break;
			}
			
			case 5:
			{
				Vector3 v = (Vector3)positions[i];
				Instantiate (magnet, v, Quaternion.identity);
				break;
			}

			default:
				break;
			}
		}


		//Adding Coins
//		int numCoins = Random.Range (0, 10);
//		ArrayList coinZ = new ArrayList ();
//		coinZ.Add(61.0f); 
//		coinZ.Add(65.0f); coinZ.Add(66.0f); coinZ.Add(67.0f); coinZ.Add(68.0f);
//		coinZ.Add(72.0f); coinZ.Add(73.0f); coinZ.Add(74.0f); coinZ.Add(75.0f);
//		coinZ.Add(79.0f); 
//
//		for (int i=0; i<numCoins; ++i) {
//			int coinPos = Random.Range (0, 9-i);
//
//			Vector3 v = new Vector3(Random.Range(-2.0f, 2.0f), 1.0f, (float)coinZ[coinPos]);
//			coinZ.Remove((float)coinZ[coinPos]);
//			Instantiate (coin, v, Quaternion.Euler(90, 0, 0));
////			Debug.Log(v);
//		}
	}
}

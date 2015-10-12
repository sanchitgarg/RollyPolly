using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUpdateScript : MonoBehaviour {

	
	GameManagerScript globalObj;
	Text healthText;
	
	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager"); 
		globalObj = g.GetComponent< GameManagerScript >(); 
		
		healthText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = "Health : " + globalObj.health.ToString();
	}
}

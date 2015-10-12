using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ScoreUpdateScript : MonoBehaviour {

	GameManagerScript globalObj;
	Text scoreText;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager"); 
		globalObj = g.GetComponent< GameManagerScript >(); 
		
		scoreText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score : " + globalObj.score.ToString();
	}
}
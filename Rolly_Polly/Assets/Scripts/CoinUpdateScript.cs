using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinUpdateScript : MonoBehaviour {

	GameManagerScript globalObj;
	Text coinText;
	
	// Use this for initialization
	void Start () {
		GameObject g = GameObject.Find ("GameManager"); 
		globalObj = g.GetComponent< GameManagerScript >(); 
		
		coinText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (globalObj.coin);
		coinText.text = "Coins : " + globalObj.coinCount.ToString();
	}
}
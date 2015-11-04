using UnityEngine;
using System.Collections;

public class AudioManagerScript : MonoBehaviour {

	public AudioClip explosion;
	public AudioClip coins;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playCoinSound()
	{
		//Debug.Log ("coin sound played");
		AudioSource.PlayClipAtPoint (coins, gameObject.transform.position);
	}

	public void playObstacleCollideSound()
	{
		//Debug.Log ("collision sound played");
		AudioSource.PlayClipAtPoint (explosion, gameObject.transform.position);
	}
}

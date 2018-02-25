using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
* Date: January 22, 2017
* Name: Olga Agafonova
* Note: I commented out particle emission because it was confusing to the user
*/
public class lightUp : MonoBehaviour
{

	public Material lightUpMaterial;
	public GameObject gameLogic;
	private Material defaultMaterial;

	// Use this for initialization
	void Start ()
	{
		defaultMaterial = this.GetComponent<MeshRenderer> ().material; //Save our initial material as the default

		try {
			//this.GetComponentInChildren<ParticleSystem> ().enableEmission = false; //Start without emitting particles
		} catch (Exception e) {
		}
		gameLogic = GameObject.Find ("GameLogic");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void patternLightUp (float duration)
	{ //The lightup behavior when displaying the pattern
		StartCoroutine (lightFor (duration));
	}


	public void gazeLightUp ()
	{
		this.GetComponent<MeshRenderer> ().material = lightUpMaterial; //Assign the hover material

		try {
			
			//this.GetComponentInChildren<ParticleSystem> ().enableEmission = true; //Turn on particle emmission

			this.GetComponent<GvrAudioSource> ().enabled = true;
			this.GetComponent<GvrAudioSource> ().Play ();

		} catch (Exception ex) {

			Debug.Log ("gazeLightUp exception: " + ex.ToString ());
		}

		gameLogic.GetComponent<GameLogic> ().playerSelection (this.gameObject);


	}

	public void playerSelection ()
	{
		gameLogic.GetComponent<GameLogic> ().playerSelection (this.gameObject);
		this.GetComponent<GvrAudioSource> ().Play ();
	}

	public void aestheticReset ()
	{
		this.GetComponent<MeshRenderer> ().material = defaultMaterial; //Revert to the default material

		try {
			//this.GetComponentInChildren<ParticleSystem> ().enableEmission = false; //Turn off particle emission 	
		} catch (Exception ex) {
			Debug.Log ("Error in aesthetic reset: " + ex.ToString ());
		} 
	}

	public void patternLightUp ()
	{ //Lightup behavior when the pattern shows.
		this.GetComponent<MeshRenderer> ().material = lightUpMaterial; //Assign the hover material

		try {
			//this.GetComponentInChildren<ParticleSystem> ().enableEmission = true; //Turn on particle emmission
		} catch (Exception ex) {
		 	
		} 
		this.GetComponent<GvrAudioSource> ().Play (); //Play the audio attached
	}


	IEnumerator lightFor (float duration)
	{ //Light us up for a duration.  Used during the pattern display
		patternLightUp ();
		yield return new WaitForSeconds (duration - .1f);
		aestheticReset ();
	}
}

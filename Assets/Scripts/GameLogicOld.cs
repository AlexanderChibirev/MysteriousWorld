using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameLogicOld : MonoBehaviour {

	public Camera currCamera;
	public Button startButton;
	public Button restartButton;
    public GameObject player;
    public GameObject startUI, restartUI;
	public GameObject startPoint, playPoint, restartPoint;
	public bool playerWon = false;

	void Start () {
		player.transform.position = startPoint.transform.position;
	}

	/*
	 * Update: January 22, 2017
	 * Description: I've disabled the color change because it was too distracting
	*/
	void Update () {

		if (Input.GetMouseButtonDown (0) && player.transform.position==playPoint.transform.position) {
			puzzleSuccess ();
		}

		startButton.GetComponent<Image> ().color = Color.blue;
		restartButton.GetComponent<Image> ().color = Color.blue;

		PointerEventData pointer = new PointerEventData (EventSystem.current);

		var raycastResults = new List<RaycastResult>();

		EventSystem.current.RaycastAll (pointer, raycastResults);

		if (raycastResults.Count > 0) {

			for (int i = 0; i < raycastResults.Count; i++) {
			   
				if (raycastResults [i].gameObject.CompareTag ("Start")) {

					//startButton.GetComponent<Image> ().color = Color.red;
				} 

				if (raycastResults [i].gameObject.CompareTag ("Restart")) {

					//restartButton.GetComponent<Image> ().color = Color.red;
				} 
			}
		} 
	}

	public void startPuzzle() { 
		toggleUI();
		iTween.MoveTo (player, 
			iTween.Hash (
				"position", playPoint.transform.position, 
				"time", 3, 
				"easetype", "linear"
			)
		);

	}

	public void resetPuzzle() {
		player.transform.position = startPoint.transform.position;
		toggleUI ();
	}


	public void puzzleSuccess() { //Do this when the player gets it right
		iTween.MoveTo (player, 
			iTween.Hash (
				"position", restartPoint.transform.position, 
				"time", 3, 
				"easetype", "linear"
			)
		);
	}
		
	public void toggleUI() {

		startUI.SetActive (!startUI.activeSelf);

		restartUI.SetActive (!restartUI.activeSelf);
	}
}

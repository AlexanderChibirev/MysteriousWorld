using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameLogic : MonoBehaviour
{
    public GameObject player;
    public GameObject eventSystem;
    public GameObject startUI, restartUI;
    public GameObject startPoint, restartPoint;
    public GameObject[] magicObjects1; //An array to hold our puzzle spheres
    public GameObject[] magicObjects2;
    public GameObject[] magicObjects3;
    public GameObject[] magicObjects4;
    public GameObject[] magicObjects5;
    public GameObject[] magicObjects6;
    public GameObject[] magicObjects7;
    public GameObject[] magicObjects8;
    public GameObject[] magicObjects9;


    public GameObject[] waypointsForPlayer;

    public int currentIndexForPlayPoints = 0;
    public int magicObjectLength = 5; //How many times we light up.  This is the difficulty factor.  The longer it is the more you have to memorize in-game.
    public float magicObjectSpeed = 1f; //How many seconds between puzzle display pulses
    private int[] magicObjectOrder; //For now let's have 5 orbs

    private int currentDisplayIndex = 0; //Temporary variable for storing the index when displaying the pattern
    public bool currentlyDisplayingPattern = true;
    public bool playerWon = false;

    private int currentSolveIndex = 0; //Temporary variable for storing the index that the player is solving for in the pattern.

    public GameObject failAudioHolder;
    public GameObject successAudioHolder;

    // Use this for initialization
    void Start()
    {
        magicObjectOrder = new int[magicObjectLength]; //Set the size of our array to the declared puzzle length
        generateMagicObjectSequence(); //Generate the puzzle sequence for this playthrough.  
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void playerSelection(GameObject magicObject)
    {

        if (playerWon != true)
        { //If the player hasn't won yet
            int selectedIndex = 0;
            //Get the index of the selected object
            for (int i = 0; i < getCurrentMagicObjects().Length; i++)
            { //Go through the puzzlespheres array
                if (getCurrentMagicObjects()[i] == magicObject)
                { //If the object we have matches this index, we're good
                    Debug.Log("Looks like we hit sphere: " + i);
                    selectedIndex = i;
                }
            }
            solutionCheck(selectedIndex);//Check if it's correct
        }
    }

    public void solutionCheck(int playerSelectionIndex)
    { //We check whether or not the passed index matches the solution index
        if (playerSelectionIndex == magicObjectOrder[currentSolveIndex])
        { //Check if the index of the object the player passed is the same as the current solve index in our solution array
            currentSolveIndex++;
            Debug.Log("Correct!  You've solved " + currentSolveIndex + " out of " + magicObjectLength);
            if (currentSolveIndex >= magicObjectLength)
            {
                magicObjectSuccess();
            }
        }
        else
        {
            magicObjectFailure(false);
        }

    }


    public void startMagicObject(bool withUpdate)
    { //Begin the puzzle sequence
      //Generate a random number one through five, save it in an array.  Do this n times.
      //Step through the array for displaying the puzzle, and checking puzzle failure or success.

        if (withUpdate)
        {
            updateDifficultyForGame();
        }
        startUI.SetActive(false);
        //eventSystem.SetActive(false); not a good idea to disable the event system because GazeInputModule will stop working
        iTween.MoveTo(player, waypointsForPlayer[currentIndexForPlayPoints].transform.position, 5f);

        CancelInvoke("displayPattern");
        InvokeRepeating("displayPattern", 3, magicObjectSpeed); //Start running through the displaypattern function
        currentSolveIndex = 0; //Set our puzzle index at 0
        magicObjectSetEnable(true, getCurrentMagicObjects()); 
    }

    private void updateDifficultyForGame()
    {
        switch (currentIndexForPlayPoints)
        {
            case 0:
                initFields(5, 1f);           
                break;
            case 1:
                initFields(5, 0.3f);             
                break;
            case 2:
                initFields(6, 0.5f);              
                break;
            case 3:
                initFields(6, 0.3f);            
                break;
            case 4:
                initFields(7, 0.8f);
                break;
            case 5:
                initFields(7, 0.5f);
                break;
            case 6:
                initFields(8, 0.9f);
                break;
            case 7:
                initFields(8, 0.5f);             
                break;
            case 8://added moved bloksss he he h
                initFields(10, 0.5f);
                break;
            default:
                break;
        }
        generateMagicObjectSequence();
    }

    private void initFields(int lenght, float speed) {
        magicObjectLength = lenght;
        magicObjectOrder = new int[magicObjectLength];
        magicObjectSpeed = speed;
    }

    private void magicObjectSetEnable(bool enabled, GameObject[] objects)
    {
        foreach (GameObject sphere in objects)
        {
            sphere.SetActive(enabled);
        }
    }

    void displayPattern()
    { //Invoked repeating.
        currentlyDisplayingPattern = true; //Let us know were displaying the pattern
        eventSystem.SetActive(false); //Disable gaze input events while we are displaying the pattern.

        if (currentlyDisplayingPattern == true)
        { //If we are not finished displaying the pattern
            if (currentDisplayIndex < magicObjectOrder.Length)
            { //If we haven't reached the end of the puzzle
                Debug.Log(magicObjectOrder[currentDisplayIndex] + " at index: " + currentDisplayIndex);
                getCurrentMagicObjects()[magicObjectOrder[currentDisplayIndex]].GetComponent<lightUp>().patternLightUp(magicObjectSpeed); //Light up the sphere at the proper index.  For now we keep it lit up the same amount of time as our interval, but could adjust this to be less.
                currentDisplayIndex++; //Move one further up.
            }
            else
            {
                Debug.Log("End of puzzle display");
                currentlyDisplayingPattern = false; //Let us know were done displaying the pattern
                currentDisplayIndex = 0;
                CancelInvoke(); //Stop the pattern display.  May be better to use coroutines for this but oh well
                eventSystem.SetActive(true); //Enable gaze input when we aren't displaying the pattern.
            }
        }
    }

    private GameObject[] getCurrentMagicObjects()
    {
        switch (currentIndexForPlayPoints)
        {
            case 0:
                return magicObjects1;
            case 1:
                return magicObjects2;
            case 2:
                return magicObjects3;
            case 3:
                return magicObjects4;
            case 4:
                return magicObjects5;
            case 5:
                return magicObjects6;
            case 6:
                return magicObjects7;
            case 7:
                return magicObjects8;
            case 8:
                return magicObjects9;
            default:
                return magicObjects1;
        }
    }

    public void generateMagicObjectSequence()
    {

        int tempReference;
        for (int i = 0; i < magicObjectLength; i++)
        { //Do this as many times as necessary for puzzle length
            tempReference = Random.Range(0, getCurrentMagicObjects().Length); //Generate a random reference number for our puzzle spheres
            magicObjectOrder[i] = tempReference; //Set the current index to our randomly generated reference number
        }
    }


    public void resetMagicObject()
    { //Reset the puzzle sequence
        iTween.MoveTo(player,
            iTween.Hash(
                "position", startPoint.transform.position,
                "time", 4,
                "easetype", "linear",
                "oncomplete", "resetGame",
                "oncompletetarget", this.gameObject
            )
        );

        restartUI.SetActive(false);
        //for ways
        currentIndexForPlayPoints = 0;
    }
    public void resetGame()
    {
        restartUI.SetActive(false);
        startUI.SetActive(true);
        playerWon = false;
        generateMagicObjectSequence(); //Generate the puzzle sequence for this playthrough.  
    }

    public void magicObjectFailure(bool withUpdate)
    { //Do this when the player gets it wrong
        Debug.Log("You've Failed, Resetting puzzle");

        currentSolveIndex = 0;

        failAudioHolder.GetComponent<GvrAudioSource>().enabled = true;
        failAudioHolder.GetComponent<GvrAudioSource>().Play();

        startMagicObject(withUpdate);

    }

    public void magicObjectSuccess()
    { //Do this when the player gets it right
        if (currentIndexForPlayPoints == waypointsForPlayer.Length - 1)
        {
            Debug.Log("Рестартим");
            iTween.MoveTo(player,
                iTween.Hash(
                    "position", restartPoint.transform.position,
                    "time", 2,
                    "easetype", "linear",
                    "oncomplete", "finishingFlourish",
                    "oncompletetarget", this.gameObject
                )
            );
        }
        else
        {
            Debug.Log("ДВИГАЕМ ДАЛЬШЕ");
            magicObjectSetEnable(false, getCurrentMagicObjects());
            currentIndexForPlayPoints++;
            magicObjectFailure(true);
        }
    }

    public void finishingFlourish()
    { //A nice audio flourish when the player wins

        successAudioHolder.GetComponent<GvrAudioSource>().enabled = true;
        successAudioHolder.GetComponent<GvrAudioSource>().Play();

        restartUI.SetActive(true);
        playerWon = true;

    }
}

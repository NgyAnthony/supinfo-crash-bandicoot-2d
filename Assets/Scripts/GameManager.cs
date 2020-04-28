using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//This class holds a static reference to itself to ensure that there will only be
	//one in existence. This is often referred to as a "singleton" design pattern. Other
	//scripts access this one through its public static methods
	static GameManager current;

	public float deathSequenceDuration = 1.5f;	//How long player death takes before restarting

	SceneFader sceneFader;						//The scene fader

	private int numberOfDeaths;							//Number of times player has died
	private float totalGameTime;						//Length of the total game time
	private bool isGameOver;							//Is the game currently over?
	public static Vector2 checkpointPos;
	private static Vector2 _originalPos;
	private static GameObject playerCrash;
	
	void Awake()
	{
		//If a Game Manager exists and this isn't it...
		if (current != null && current != this)
		{
			//...destroy this and exit. There can only be one Game Manager
			Destroy(gameObject);
			return;
		}

		//Set this as the current game manager
		current = this;

		//Persis this object between scene reloads
		DontDestroyOnLoad(gameObject);
		playerCrash = GameObject.Find("Crash");
		
		_originalPos = playerCrash.transform.position;
		checkpointPos = _originalPos;

	}

	void Update()
	{
		//If the game is over, exit
		if (isGameOver)
			return;

		//Update the total game time and tell the UI Manager to update
		totalGameTime += Time.deltaTime;
		UIManager.UpdateTimeUI(totalGameTime);
	}
	
	public static bool IsGameOver()
	{
		//If there is no current Game Manager, return false
		if (current == null)
			return false;

		//Return the state of the game
		return current.isGameOver;
	}

	public static void RegisterSceneFader(SceneFader fader)
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Record the scene fader reference
		current.sceneFader = fader;
	}

	public static void PlayerDied()
	{	
		
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//Increment the number of player deaths and tell the UIManager
		current.numberOfDeaths++;
		UIManager.UpdateDeathUI(current.numberOfDeaths);

		//If we have a scene fader, tell it to fade out and then in immediatly
		current.sceneFader.FadeSceneOut();
		current.sceneFader.FadeSceneIdle();
	}

	public static void PlayerLost()
	{	
		//If there is no current Game Manager, exit
		if (current == null)
			return;
		
		
		//Increment the number of player deaths and tell the UIManager
		current.numberOfDeaths++;
		UIManager.UpdateDeathUI(current.numberOfDeaths);
		
		//Display you lost text
		UIManager.DisplayYouLostText();
		
		//The game is now over
		current.isGameOver = true;
		current.sceneFader.FadeSceneOut();
	}
	
	public static void PlayerWon()
	{
		//If there is no current Game Manager, exit
		if (current == null)
			return;

		//The game is now over
		current.isGameOver = true;

		//Tell UI Manager to show the game over text and tell the Audio Manager to play
		//game over audio
		UIManager.DisplayGameOverText();
		//TODO AudioManager.PlayWonAudio();
	}

	public static void RestartScene()
	{
		//Play the scene restart audio
		AudioManager.PlaySceneRestartAudio();
		
		//Remove UI of replay question
		UIManager.CleanUIonReplay();
		
		//Game isn't over anymore
		current.isGameOver = false;
		
		//Reload the current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
		//Remove checkpoint
		checkpointPos = _originalPos;
	}
	
}

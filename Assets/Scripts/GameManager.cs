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

	int numberOfDeaths;							//Number of times player has died
	float totalGameTime;						//Length of the total game time
	bool isGameOver;							//Is the game currently over?
	

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

		//If we have a scene fader, tell it to fade the scene out
		if(current.sceneFader != null)
			current.sceneFader.FadeSceneOut();

		//Invoke the RestartScene() method after a delay TODO: replace by go to spawn or checkpoint
		current.Invoke("RestartScene", current.deathSequenceDuration);
	}

	public static void PlayerLost()
	{	
		
		//If there is no current Game Manager, exit
		if (current == null)
			return;
		
		//The game is now over
		current.isGameOver = true;
		
		//If we have a scene fader, tell it to fade the scene out
		if(current.sceneFader != null)
			current.sceneFader.FadeSceneOut();
		
		//Display you lost text
		UIManager.DisplayYouLostText();
		
		//Ask the player if he wants to play again, if so play the scene again. If not close the game.
		if (UIManager.PlayAgain())
		{
			current.Invoke("RestartScene", current.deathSequenceDuration);
		}
		else if (UIManager.StopPlaying())
		{
			Application.Quit();
		}
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

	void RestartScene()
	{
		//Play the scene restart audio
		// TODO AudioManager.PlaySceneRestartAudio();

		//Reload the current scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}
}

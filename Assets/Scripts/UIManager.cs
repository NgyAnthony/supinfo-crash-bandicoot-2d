// This script is a Manager that controls the UI HUD (deaths, time, and orbs) for the 
// project. All HUD UI commands are issued through the static methods of this class

using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //This class holds a static reference to itself to ensure that there will only be
    //one in existence. This is often referred to as a "singleton" design pattern. Other
    //scripts access this one through its public static methods
    static UIManager current;

    public TextMeshProUGUI wumpaText;		//Text element showing the number of wumpas
    public TextMeshProUGUI shieldText;      //Text element showing the number of shields
    public TextMeshProUGUI livesText;       //Text element showing the number of lives
    
    public TextMeshProUGUI timeText;		//Text element showing amount of time
    
    public TextMeshProUGUI deathText;		//Text element showing number or deaths
    public TextMeshProUGUI youLost;         //Text element showing the You Lost message
    public GameObject playAgainButton;
    public GameObject quitButton;
    public Image deathImage;

    public TextMeshProUGUI gameOverText;	//Text element showing the Game Over message

    private void Awake()
    {
        //If an UIManager exists and it is not this...
        if (current != null && current != this)
        {
            //...destroy this and exit. There can be only one UIManager
            Destroy(gameObject);
            return;
        }

        //This is the current UIManager and it should persist between scene loads
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void UpdateTimeUI(float time)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Take the time and convert it into the number of minutes and seconds
        int minutes = (int)(time / 60);
        float seconds = time % 60f;

        //Create the string in the appropriate format for the time
        current.timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    
    public static void WumpaUI(int wumpaCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;
        
        //update the wumpa count element
        current.wumpaText.text = wumpaCount.ToString();
    }

    public static void LivesUI(int LivesCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;
        
        //update the Lives count element
        current.livesText.text = LivesCount.ToString();
    }
    
    public static void ShieldUI(int shieldCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;
        
        //update the shield count element
        current.shieldText.text = shieldCount.ToString();
    }
    public static void UpdateDeathUI(int deathCount)
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //update the player death count element
        current.deathText.text = deathCount.ToString();
    }

    public static void DisplayYouLostText()
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Show the you lost text
        current.youLost.enabled = true;
        current.deathImage.enabled = true;
        current.playAgainButton.SetActive(true);
        current.quitButton.SetActive(true);
    }

    public static void CleanUIonReplay()
    {
        current.youLost.enabled = false;
        current.deathImage.enabled = false;
        current.playAgainButton.SetActive(false);
        current.quitButton.SetActive(false);
    }
    
    
    public void PlayAgain()
    {
        GameManager.RestartScene();
    }

    public void StopPlaying()
    {
        Application.Quit();
    }
    
    public static void DisplayGameOverText()
    {
        //If there is no current UIManager, exit
        if (current == null)
            return;

        //Show the game over text
        current.gameOverText.enabled = true;
    }
}

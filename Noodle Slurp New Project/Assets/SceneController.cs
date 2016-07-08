using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		BackHardwareKeyPressed();
	
	}

	public void BackHardwareKeyPressed()
	{
		if(Input.GetKeyDown(KeyCode.Escape))			//Back key is pressed
		{
			switch(Application.loadedLevelName+"")				//Current Level Name is checked 
			{
			case "Game Over" :	SceneManager.LoadScene("Menu"); break;			//Menu scene is loaded if the player is in Game Over scene
			default : break;
			}
		}

	}

	public void PlayGame()
	{
	//	Application.LoadLevel ("GamePlay");
		SceneManager.LoadScene("GamePlay");
	}

	public void LeaderBoard()
	{
		// Calls the leaderboard of Google API
	}
	
	public void MainMenu_HighScore()
	{
		// Hide all Menus
		// Show the High Score Menu
	}

	public void MainMenu_LogOut()
	{
		// Call Google Play APIs and Logout
	}
	public void MainMenu_Exit()
	{
		Application.Quit();
	}

	public void HighScore_Show()
	{	
		// Loads the high score from player prefs
		// saves the high score in the UI text
	}

	public void ExitMenu_Yes()
	{
		// Application.Quit is called and the game is closed
	}

	public void ExitMenu_No()
	{	
		// Hides the exit menu
		// Shows the Main_Menu
	}

	public void Menu()
	{	
	//	Application.LoadLevel ("Menu");
		SceneManager.LoadScene("Menu");
	}
}

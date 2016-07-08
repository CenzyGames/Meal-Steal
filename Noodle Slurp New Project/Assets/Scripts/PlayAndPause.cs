using UnityEngine;
using System.Collections;

public class PlayAndPause : MonoBehaviour {

	public bool enablePhoneBackKey = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(enablePhoneBackKey == true)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				PlayPauseGame();
			}
		}
	}

	void PlayGame()
	{
		Time.timeScale = 1;
	//	Debug.Log("Game is playing");
	}

	void PauseGame()
	{
		Time.timeScale = 0;
	//	Debug.Log("Game Paused");
	}

	public void PlayPauseGame()
	{
		if(Time.timeScale == 0)
		{
			PlayGame();
		}
		else
		{
			PauseGame();
		}

	}
}

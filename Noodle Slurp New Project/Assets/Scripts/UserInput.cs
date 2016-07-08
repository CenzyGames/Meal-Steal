using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInput : MonoBehaviour {

	static bool IsScreenPressed = false;

	float score  = 0;

//	GameObject Boy;
	GameObject Girl;
	GameObject Animations;

	GameObject ConfirmationPanel;
	// Use this for initialization

	void OnEnable()
	{
		if(Application.loadedLevelName + "" == "GamePlay")
		{
			Time.timeScale = 1;
		}
	}

	void Start () 
	{
	//	Boy = GameObject.Find ("Boy");
		Girl = GameObject.Find ("Girl");
		Animations = GameObject.Find ("Animations");
		ConfirmationPanel = GameObject.Find("ConfirmationPanel");
		ConfirmationPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update() 
	{
		UserInputStarted();
		UserInputEnded();
		BackHardwareKeyPressed();
			
		if (IsScreenPressed == true)  // Screnarios that should happen is the screen is pressed by the player.
		{					
			CalculateScore ();
			Debug.Log ("Screen pressed"); 
		//	if(GameObject.Find("Noodle") != null)
		//	GameObject.Find("Noodle").SetActive(true);
			Animations.GetComponent<AnimationController> ().BoyAnimation ();
			Animations.GetComponent<AnimationController> ().GirlAnimation ();

		} 
		else 
		if (IsScreenPressed == false)
		{
		//		if(GameObject.Find("Noodle") != null)
		//	GameObject.Find("Noodle").SetActive(false);
			Debug.Log ("Screen not pressed");
		}


		if(Girl.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Girl Game Over Animation"))
		{			
			PlayerPrefs.SetInt("Currentscore" , (int)score);	// SAVE THE HIGH SCORE 
			saveHighScore();
			Application.LoadLevel ("Game Over");
		}

	}
		
	void CalculateScore()
	{
		if(Application.loadedLevelName == "GamePlay")
		{
			if(GameObject.Find("Noodle") != null)
			if(GameObject.Find("Noodle").activeInHierarchy == true)
			score  += Time.deltaTime *10;
			if(GameObject.Find ("Score")!=null)
			GameObject.Find ("Score").GetComponent<Text> ().text = "" + (int)score + "'" ;
		}
	}

	void ResetScore()
	{
		score  = 0;
	}

	void UserInputStarted()						// If the screen is pressed by the player then 'IsScreenPressed = true'
	{
		if(Input.GetButtonDown("Fire1"))
		{
			IsScreenPressed = true;
			if (!GameObject.Find ("Girl").GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Angry")) {
				GameObject.Find ("Boy Hand 2").GetComponent<Animator> ().SetBool ("HandMove", true);
			}
		}
			
	}

	void UserInputEnded()					// If the screen is NOT pressed by the player then 'IsScreenPressed = false'
	{
		if(Input.GetButtonUp("Fire1"))
		{
			IsScreenPressed = false;
			if (!GameObject.Find ("Girl").GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Angry")) {
				GameObject.Find ("Boy Hand 2").GetComponent<Animator> ().SetBool ("HandMove", false);
			}
		}
	}

	public bool ScreenPressed()
	{
		return IsScreenPressed;
	}

	public void saveHighScore()
	{
		if(PlayerPrefs.GetInt("Highscore") < PlayerPrefs.GetInt("Currentscore"))
		{
			PlayerPrefs.SetInt("Highscore" , (int)score);
			LeaderboardController lead = new LeaderboardController();
			lead.SaveScore();
		}
	}


	public void BackHardwareKeyPressed()
	{
		if(Input.GetKeyDown(KeyCode.Escape))			//Back key is pressed
		{
			switch(Application.loadedLevelName+"")				//Current Level Name is checked 
			{
				case "GamePlay" : ConfirmationPanel.SetActive(true);  Time.timeScale = 0; break;			//ConfirmationPanel is shown to ask for confirmation
				case "Game Over" :	SceneManager.LoadScene("Menu"); break;			//Menu scene is loaded if the player is in Game Over scene
				default : break;
			}
		}

	}


	public void ConfirmationPanel_NO()
	{
		ConfirmationPanel.SetActive(false); 
		Time.timeScale = 1;
	}
}

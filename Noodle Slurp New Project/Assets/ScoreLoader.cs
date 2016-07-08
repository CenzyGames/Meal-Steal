using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLoader : MonoBehaviour {
	GameObject Score;
	GameObject HighScore;

	string subject ;
	string body;


	// Use this for initialization
	void Start () 
	{
		Score = GameObject.Find("Score");
		HighScore = GameObject.Find("High Score");

		LoadScores();

		subject = "Have you played Meal Steal on Android ?";
		body = subject +  "I just scored " + PlayerPrefs.GetInt("Currentscore") + " and my high score is " + PlayerPrefs.GetInt("Highscore") + ". Do check it out. Download Link - https://play.google.com/store/apps/details?id=com.cenzy.mealsteal" ;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void LoadScores()
	{
		Score.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Currentscore") + "" ;
		HighScore.GetComponent<Text>().text = "" +  PlayerPrefs.GetInt("Highscore") + "";
	}
		

	public void shareText(){
		//execute the below lines if being run on a Android device
		#if UNITY_ANDROID
		//Refernece of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		//Refernece of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		//call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//start the activity by sending the intent data
		currentActivity.Call ("startActivity", intentObject);
		#endif

	}

}

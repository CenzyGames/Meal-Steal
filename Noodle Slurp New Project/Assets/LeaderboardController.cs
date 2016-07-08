using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine.UI;  
using com.shephertz.app42.paas.sdk.csharp.social; 
using Facebook.Unity;
using Facebook.MiniJSON;

public class LeaderboardController : MonoBehaviour, App42CallBack
{
	ScoreBoardService scoreBoardService = null; // Initialising ScoreBoard Service.
	//Constants cons = new Constants ();
	public string success, columnName, errorLable;
	public int txt_max;
	public bool saveButton, leaderBoardButton;

	public string apiKey  ="265edcd0e1ec2faea55b5c931afb9640d195c364cc818ef8687031b0745e664a";// API key that you have receieved after the success of app creation from AppHQ
	public string secretKey ="d583e99bb17dc4c5ebe6e17f48ae18996c8ae6f52be3fb261f1a048a4f168152";// SECRET key that you have receieved after the success of app creation from AppHQ

	bool boardShow = false;
	string userName = "";

	String FBgameName = "Meal Steal";    

	public GameObject rank1;
	public GameObject rank2;
	public GameObject rank3;
	public GameObject allranks;
	public GameObject NoNet;

	static int status = 0;

	void Start ()
	{
		App42API.Initialize(apiKey ,secretKey);
		App42API.SetOfflineStorage (true, 20);
		App42Log.SetDebug (true);

//		rank1 = GameObject.Find ("1");
//		rank2 = GameObject.Find ("2");
//		rank3 = GameObject.Find ("3");
//		allranks = GameObject.Find ("Panel");
//		NoNet = GameObject.Find ("No Net Connection");
	}

	void Update() 
	{
		if (status < 70) 
		{
			if(rank1!=null)
			rank1.SetActive (false);
			
			if(rank2!=null)
			rank2.SetActive (false);
			
			if(rank3!=null)
			rank3.SetActive (false);
			
			if(allranks!=null)
			allranks.SetActive (false);
			
			if(NoNet!=null)
			NoNet.SetActive (true);
		} 
		else {
			if(rank1!=null)
			rank1.SetActive (true);

			if(rank2!=null)
			rank2.SetActive (true);

			if(rank3!=null)
			rank3.SetActive (true);

			if(allranks!=null)
			allranks.SetActive (true);

			if(NoNet!=null)
			NoNet.SetActive (false);
		}
	}
		
	public void LeaderBoardAnimation()
	{
		boardShow = !boardShow;
		if (boardShow) 
		{
			GameObject.Find ("Leaderboards Panel").GetComponent<Animator> ().SetBool ("BoardShow", true );
			GetTopScorers ();
		} 
		else 
		{
			GameObject.Find ("Leaderboards Panel").GetComponent<Animator> ().SetBool ("BoardShow", false );
		}
	}

	public void GetTopScorers()
	{
		leaderBoardButton = true;
		SocialService socialService = App42API.BuildSocialService();   
		var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
		scoreBoardService = App42API.BuildScoreBoardService();   //For FACEBOOK
		scoreBoardService.GetTopNRankers(FBgameName, 20, new LeaderboardController());   

	}

	public void OnSuccess (object response)
	{	
		Debug.Log ("Response - " + response.ToString ().Length);

		status = response.ToString ().Length;
		Debug.Log ("Response  s- " + status);

		//ForFacebook (response);
		ForNormal (response);
		  
	}



	public void ForNormal(object response)
	{
		var nxtLine = System.Environment.NewLine; //Use this whenever I need to print something On Next Line.
		if (response is App42OfflineResponse) {
			success = "Network UnAvailable : " + nxtLine +
				"----------------------------------------" + nxtLine + 
				"Information is Stored in cache," + nxtLine +
				"Will send to App42 when network is available.";
		} 

			Game gameResponseObj = (Game)response;
			//if (leaderBoardButton) 
		{
				leaderBoardButton = false;
				success = "";
				IList<Game.Score> topRankersList = gameResponseObj.GetScoreList ();
				if (topRankersList.Count > 0) {
					// Creating ScoreBoard Manually.
					for (int i = 0; i < gameResponseObj.GetScoreList().Count; i++) 
					{
						string scorerName = gameResponseObj.GetScoreList () [i].GetUserName ();
						double scorerValue = gameResponseObj.GetScoreList () [i].GetValue ();

					// TRIM SCorename
						
						scorerName = Regex.Replace(scorerName, "[0-9]", "");

						GameObject.Find("Name" + (i +1)).GetComponent<Text>().text = scorerName+"";
						GameObject.Find("SCORE" + (i + 1)).GetComponent<Text>().text = scorerValue+"";
					}
				}
			}


	}

	public void ForFacebook(object response)
	{
		Debug.Log("--------------------------------2");
		Game game = (Game) response;       
		App42Log.Console("gameName is " + game.GetName());   
		for(int i = 0;i < game.GetScoreList().Count; i++)  
		{  
			App42Log.Console("userName is : " + game.GetScoreList()[i].GetUserName());  
			App42Log.Console("score value is : " + game.GetScoreList()[i].GetValue());  
			App42Log.Console("scoreId is : " + game.GetScoreList()[i].GetScoreId());  
			App42Log.Console("Created On : " + game.GetScoreList()[i].GetCreatedOn());  
			App42Log.Console("Facebook id is : " + game.GetScoreList()[i].GetFacebookProfile().GetId());  
			App42Log.Console("Facebook name is : " + game.GetScoreList()[i].GetFacebookProfile().GetName());  
			App42Log.Console("Facebook picture is : " + game.GetScoreList()[i].GetFacebookProfile().GetPicture());  
		}  
	}

	public void OnException (Exception e)
	{
		var nxtLine = System.Environment.NewLine; //Use this whenever I need to print something On Next Line.

		App42Exception exception = (App42Exception)e;
		int appErrorCode = exception.GetAppErrorCode ();
		if (appErrorCode == 3002) {
			Debug.Log("Exception Occurred :" + nxtLine +
				"Game With The Name " + nxtLine + 
				" Does Not Exist.");
			// handle here , If Game Name Does Not Exist.
		} else if (appErrorCode == 3013) {
			Debug.Log("Exception Occurred :" + nxtLine +
				"Scores For The Game," + nxtLine + 
				"With The Name " + nxtLine + 
				" Does Not Exist.");
			// handle here , if no scores found for the given gameName.
		} else if (appErrorCode == 1401) {
			Debug.Log("Exception Occurred :" + nxtLine +
				"Client Is Not authorized" + nxtLine +
				"Please Verify Your" + nxtLine + 
				"API_KEY & SECRET_KEY" + nxtLine +
				"From AppHq.");
			// handle here for Client is not authorized
		} else if (appErrorCode == 1500) {
			Debug.Log( "Exception Occurred :" + nxtLine +
				"WE ARE SORRY !!" + nxtLine +
				"But Somthing Went Wrong.");
			// handle here for Internal Server Error
		} else {
			Debug.Log("Exception Occurred :" + exception);
		}
				Debug.Log ("Message : " + e);	

	}

	public void SaveScore()
	{
			FacebookLoginController FBLC = new FacebookLoginController();
			FBLC.CheckFacebookLogin();
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			if(aToken.UserId!=null)
			userName = PlayerPrefs.GetString("FirstName") + " " + aToken.UserId + "";		//
		//	userName = aToken.UserId + "";  // Name Of The USER Who Wants To Save Score. //FACEBOOK USERNAME OF THE PLAYER
			int score = PlayerPrefs.GetInt("Highscore");	
			scoreBoardService = App42API.BuildScoreBoardService (); // Initializing scoreBoardService.
			scoreBoardService.SaveUserScore (FBgameName, userName, score, this);
			saveButton = true;
			Debug.Log("Posted High score");
	//	}
	}



}
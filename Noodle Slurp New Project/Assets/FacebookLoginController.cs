using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Facebook.MiniJSON;


public class FacebookLoginController : MonoBehaviour {

	string contentTitle = "Meal Steal Only for Friends lovers";
	string contentDescription = "Enjoy the fun filled mischief in this game 'Meal Steal'. Help your Friend steal the meal";

	GameObject Login;
	GameObject Logout;

	static string FBUserFirstname = "Guest";
	string FBUserLastName = "";
	string Email = "";

	Texture2D FBprofilePic;
	Sprite FBProfilePicture;

	void Awake()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(SetInit, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
			FBUserFirstname = PlayerPrefs.GetString ("FirstName");
		}
	}

	// Use this for initialization
	void Start () 
	{
		Login = GameObject.Find("Login");
		Logout = GameObject.Find("Logout");
		if(GameObject.Find("Guest")!=null)
			GameObject.Find("Guest").GetComponent<Text>().text = "Player: " + FBUserFirstname;
		if(FB.IsLoggedIn == true)
		{
			if(Logout!=null)
				Logout.SetActive(true);
			if(Login!=null)
				Login.SetActive(false);
		}
		else
		{
			if(Logout!=null)
				Logout.SetActive(false);
			if(Login!=null)
				Login.SetActive(true);
		}
		FBUserFirstname = PlayerPrefs.GetString ("FirstName");
	}

	private void SetInit()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		if(!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void ConnectWithFacebook()
	{
		if(FB.IsLoggedIn)
		{
			var perms = new List<string>(){"public_profile", "email", "user_friends"};
			FB.LogInWithReadPermissions(perms, AuthCallback2);
			Debug.Log("Already Fb Logged In");
			LoginWithPublishPermission();
		}
		else
		{
			Fblogin();
			Debug.Log("Fb Logged In");

		}
	}

	public void LoginWithPublishPermission()
	{
		var perms = new List<string>() { "publish_actions" };
		FB.LogInWithPublishPermissions(perms, AuthCallback);
	}

	public void LogOutWithFacebook()
	{
		FB.LogOut();
		Debug.Log("Logged Out");
		Logout.SetActive(false); // Player not Logged in. Logout button should be displayed
		Login.SetActive(true);
		GameObject.Find("Guest").GetComponent<Text>().text = "Player: Guest";
	}

	void Fblogin()
	{
		var perms = new List<string>(){"public_profile", "email", "user_friends"};

		FB.LogInWithReadPermissions(perms, AuthCallback);
		var perms2 = new List<string>(){"publish_actions"};
		//	FB.LogInWithPublishPermissions(perms2, AuthCallback2);

	}
	private void AuthCallback2 (ILoginResult result) {
		if (FB.IsLoggedIn) {
			Logout.SetActive(true); // Player Logged in. Logout button should be displayed
			Login.SetActive(false);
			GetPlayerDetails();
		} else {
			Debug.Log("User cancelled login");

			if(Logout!=null)
				Logout.SetActive(false); // Player not Logged in. Logout button should be displayed
			if(Login!=null)
				Login.SetActive(true);
		}
	}

	private void AuthCallback (ILoginResult result) {

		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

			//aToken.ExpirationTime.AddDays(20);
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);

				if(Logout!=null)
					Logout.SetActive(true); // Player Logged in. Logout button should be displayed
				if(Login!=null)
					Login.SetActive(false);
				GetPlayerDetails();
			}
		} else {
			Debug.Log("User cancelled login");
			if(Logout!=null)
				Logout.SetActive(false); // Player not Logged in. Logout button should be displayed
			if(Login!=null)
				Login.SetActive(true);
		}
	}

	public void ShareLinkOnFB()
	{
		if(FB.IsLoggedIn)
		{
			Debug.Log("Sharing content ----");
			FB.ShareLink(
				new Uri("https://play.google.com/store/apps/details?id=com.cenzy.mealsteal"),
				contentTitle ,
				contentDescription ,
				callback: ShareCallback
			);
		}
		else
		{
			Debug.Log("Please login and share ");
		}

	}

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}


	public void PublishScoreOnFB()
	{
		int score = 10000;

		var scoreData = new Dictionary<string, string>() {{"score", score.ToString()}};

		FB.API ("/me/scores", HttpMethod.POST, APICallback, scoreData);
	}

	private void APICallback(IGraphResult result)
	{
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			Debug.Log("Share Score Error: "+ result.Error + "000 " +  result.ToString());
		} 
		else {
			// Share succeeded without postID
			Debug.Log("Share score success!");
		}
	}

	public void SendFBInvite()
	{
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			PlayerPrefs.SetString("UserId",aToken.UserId );

			FB.Mobile.AppInvite(new Uri("https://fb.me/225602071162228"), new Uri("http://www.cenzy.com/wp-content/uploads/2016/04/Invite2.png"),  HandleResult);

		}
		else
		{
			Debug.Log("Cant send invite - You are not logged in");
			Fblogin();
			//	SendFBInvite();
		}
	}


	protected void HandleResult(IResult result)
	{
		if(result.Cancelled)
		{
			Debug.Log("Invite cancelled");
		}
		else
			if(result.Error != null)
			{
				Debug.Log("Invite cancelled");

			}
			else
			{
				PlayerPrefs.SetInt("Currentscore" , 2 * PlayerPrefs.GetInt("Currentscore"));
				GameObject.Find("Score").GetComponent<Text>().text = PlayerPrefs.GetInt("Currentscore") + "";
				if(PlayerPrefs.GetInt("Currentscore")> PlayerPrefs.GetInt("Highscore"))
				{
					PlayerPrefs.SetInt("Highscore" , PlayerPrefs.GetInt("Currentscore"));
					GameObject.Find("High Score").GetComponent<Text>().text = PlayerPrefs.GetInt("Highscore") + "";
				}
				LeaderboardController lead = new LeaderboardController();
				lead.SaveScore();
				GameObject.Find("ChallengeFriendButton").SetActive(false);
				GameObject.Find("Double").SetActive(false);
				GameObject.Find("ChallengeFriendButtonFake").GetComponent<Animator>().enabled = false;
			}
	}

	void GetPlayerDetails()
	{
		if (FB.IsLoggedIn) 
		{
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

			PlayerPrefs.SetString("UserId",aToken.UserId );
			// Print current access token's User ID
			if(Application.loadedLevelName == "Menu")
			{
				GetFirstName();
				//GetUserID ();

				PushNotification Pushnoti = new PushNotification ();
				Pushnoti.InitiatePush ();

				//GetProfilePicture();
			}
			Debug.Log("Token - " + aToken );
			// Print current access token's granted permissions
		}
		else{
			Debug.Log("Shitn - "  );
		}
	}

	void GetUserID()
	{

	}

	void GetFirstName()
	{
		FB.API("/me?fields=first_name", HttpMethod.GET, delegate (IGraphResult result) {
			// Add error handling here
			if (result.ResultDictionary != null) {
				foreach (string key in result.ResultDictionary.Keys) {
					Debug.Log(key + " : " + result.ResultDictionary[key].ToString());
					if(key.ToString() == "first_name")
					{
						FBUserFirstname = result.ResultDictionary[key].ToString();
						GameObject.Find("Guest").GetComponent<Text>().text = "Player: " + FBUserFirstname;
						PlayerPrefs.SetString("FirstName",FBUserFirstname);
					}
					// first_name : Chris
					// id : 12345678901234567
				}
			}

		});

	}

	void GetProfilePicture()
	{
		FB.API("/me/picture?redirect=false", HttpMethod.GET, ProfilePhotoCallback);
	}

	private void ProfilePhotoCallback (IGraphResult result)
	{
		if (String.IsNullOrEmpty(result.Error) && !result.Cancelled) {
			IDictionary data = result.ResultDictionary["data"] as IDictionary;
			string photoURL = data["url"] as String;

			StartCoroutine(fetchProfilePic(photoURL));
		}
	}

	private IEnumerator fetchProfilePic (string url) {
		WWW www = new WWW(url);
		yield return www;
		this.FBprofilePic = www.texture;
		FBProfilePicture = Sprite.Create(FBprofilePic, new Rect(0,0,45,45) , new Vector2(0.3f,0.5f));
		//		GameObject.Find("Profile Pic").GetComponent<Image>().sprite = FBProfilePicture;

	}

	public void CheckFacebookLogin()
	{
		if(FB.IsLoggedIn)
		{
			return;
		}
		else
		{
			Fblogin();
		}
	}

}

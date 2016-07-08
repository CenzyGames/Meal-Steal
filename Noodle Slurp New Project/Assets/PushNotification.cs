using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;    
using com.shephertz.app42.paas.sdk.csharp.pushNotification;  
using com.shephertz.app42.paas.sdk.csharp.game;
using System;

public class PushNotification : MonoBehaviour, App42CallBack {

	public string apiKey  ="265edcd0e1ec2faea55b5c931afb9640d195c364cc818ef8687031b0745e664a";// API key that you have receieved after the success of app creation from AppHQ
	public string secretKey ="d583e99bb17dc4c5ebe6e17f48ae18996c8ae6f52be3fb261f1a048a4f168152";// SECRET key that you have receieved after the success of app creation from AppHQ

	string userName = "Guest";  
	string deviceToken = "device token"	;		//"AIzaSyCEIFzcuHvVqvhAl-jRNF8uMmLJ8n7cgHE";  
      //Print output in your editor console  

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	public void InitiatePush () 
	{
		App42API.Initialize(apiKey,secretKey);
		App42Log.SetDebug(true);  
		GetUsername ();
	}


	public void OnSuccess(object response)  
	{  
		PushNotification pushNotification = (PushNotification) response;    
		App42Log.Console("UserName is " + pushNotification.userName);  
		App42Log.Console("Type is " +  pushNotification.GetType());  
		App42Log.Console("DeviceToken is " +  pushNotification.deviceToken);     
	}  
	public void OnException(Exception e)  
	{  
		App42Log.Console("Exception : " + e);  
	} 

	void GetUsername()
	{
		//yield return new WaitForSeconds(0.5f);
	//	userName = "Android --";
		userName = PlayerPrefs.GetString("FirstName");
		deviceToken = PlayerPrefs.GetString("UserId");
		PushNotificationService pushNotificationService = App42API.BuildPushNotificationService();  
		pushNotificationService.StoreDeviceToken(userName,deviceToken,com.shephertz.app42.paas.sdk.csharp.pushNotification.DeviceType.ANDROID, new PushNotification()); 
	}


		    
}

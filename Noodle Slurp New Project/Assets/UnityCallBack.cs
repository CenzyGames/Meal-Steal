using com.shephertz.app42.paas.sdk.csharp.social; 
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Net;
using System;

public class UnityCallBack : App42CallBack  
{  
//	static String FBUserFirstname = "Guest";
//	static String FBUserID = "";
//	static String FBaccessToken = ""; 
//	static String FBUserFullname = "Guest";
	public void OnSuccess(object response)  
	{  
		Social social = (Social) response;      
		App42Log.Console("userName is" + social.GetUserName());   
		App42Log.Console("fb Access Token is" + social.GetFacebookAccessToken());  

		//To get the friend list from facebook
		for(int i =0; i < social.GetFriendList().Count;i++)  
		{  
			App42Log.Console("Installed is : " + social.GetFriendList()[i].GetInstalled());  
			App42Log.Console("Id is : " + social.GetFriendList()[i].GetId());  
			App42Log.Console("Name is : " + social.GetFriendList()[i].GetName());  
		}       

	}  
	public void OnException(Exception e)  
	{  
		App42Log.Console("Exception : " + e);  
	}  
		
}  
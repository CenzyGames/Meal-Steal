using com.shephertz.app42.paas.sdk.csharp.social; 
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Net;
using System;

public class UnityCallBack2 : App42CallBack  
{  

	public void OnSuccess(object response)  
	{  
		App42Log.Console("LOL");      

	}  
	public void OnException(Exception e)  
	{  
		App42Log.Console("Exception : " + e);  
	}  
}  
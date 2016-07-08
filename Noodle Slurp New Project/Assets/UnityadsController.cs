using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityadsController : MonoBehaviour {

	[SerializeField] string gameID = "1062338";
	public bool enableTestMode = true;	

	int adsCounter = 0;
	// Use this for initialization
	void Start () 
	{
		adsCounter = PlayerPrefs.GetInt ("adsCounter");
		Debug.Log (adsCounter + "--");
		Advertisement.Initialize (gameID, true);
		if (Application.loadedLevelName == "Game Over") 
		{
			if (adsCounter == 0) {
				PlayerPrefs.SetInt ("adsCounter", adsCounter);
			} 
			else
				if (adsCounter == 3)
				{
					Debug.Log ("Initialized");
					ShowAd ("1062338");
					adsCounter = 0;
				}
				else 
				{
					adsCounter = PlayerPrefs.GetInt ("adsCounter");


					PlayerPrefs.SetInt ("adsCounter", adsCounter);
				}
			adsCounter +=1;
			PlayerPrefs.SetInt ("adsCounter", adsCounter);
		}

	}

	public void ShowAd(string zone = "")
	{
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd ());
		#endif

		if (Advertisement.IsReady ("video")) 
		{		
			Advertisement.Show ("video", new ShowOptions {			
				resultCallback = result => {						
					Debug.Log (result.ToString ());					
				}													
			});	
		}

		//		if (string.Equals (zone, ""))
		//			zone = null;
		//
		//		ShowOptions options = new ShowOptions ();
		//		options.resultCallback = AdCallbackhandler;
		//
		//		if (Advertisement.isReady (zone))
		//			Advertisement.Show (zone, options);
	}

	void AdCallbackhandler (ShowResult result)
	{
		switch(result)
		{
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
			break;
		case ShowResult.Failed:
			Debug.Log("I swear this has never happened to me before");
			break;
		}
	}

	IEnumerator WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		//	yield return null;

		while (Advertisement.isShowing)
			yield return null;

		Time.timeScale = currentTimeScale;
	}
}

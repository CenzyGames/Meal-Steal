using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public GameObject SlurpSound;
	public GameObject TVSound;
	public GameObject ButtonClick;
	public GameObject GirlAngry;
	public GameObject GirlHuh;
	public GameObject GameOver;
	public GameObject Ambient;

	void Awake()
	{
		if (Application.loadedLevelName == "Game Over") 
		{
			if(GameObject.Find ("GirlAngry")!=null)
			GirlAngry.GetComponent<AudioSource> ().playOnAwake = true;
		}

		DontDestroyOnLoad (GirlAngry);
		DontDestroyOnLoad (TVSound);
	}

	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 1) 
		{
			SoundOn ();
			if(GameObject.Find ("Off")!=null)
			GameObject.Find ("Off").SetActive(true);
			if(GameObject.Find ("On")!=null)
			GameObject.Find ("On").SetActive(false);

		}
		else
			if (PlayerPrefs.GetInt ("SoundOn") == 0)
			{
				SoundOff ();
				if(GameObject.Find ("On")!=null)
				GameObject.Find ("On").SetActive(true);
				if(GameObject.Find ("Off")!=null)
				GameObject.Find ("Off").SetActive(false);
			}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SoundOn()
	{
		PlayerPrefs.SetInt ("SoundOn",1);

		if (SlurpSound != null) {
			SlurpSound.GetComponent<AudioSource> ().mute = true;
		}
		if (TVSound!= null) {
			TVSound.GetComponent<AudioSource> ().mute = true;
		}
		if (ButtonClick != null) {
			ButtonClick.GetComponent<AudioSource> ().mute = true;
		}

		if (GirlAngry != null) {
			GirlAngry.GetComponent<AudioSource> ().mute = true;
		}
		if (GirlHuh!= null) {
			GirlHuh.GetComponent<AudioSource> ().mute = true;
		}
		if (GameOver!= null) {
			GameOver.GetComponent<AudioSource> ().mute = true;
		}
		if (Ambient!= null) {
			Ambient.GetComponent<AudioSource> ().mute = true;
		}

	}

	public void SoundOff()
	{
		PlayerPrefs.SetInt ("SoundOn",0);
		if (SlurpSound != null) {
			SlurpSound.GetComponent<AudioSource> ().mute = false;
		}
		if (TVSound!= null) {
			TVSound.GetComponent<AudioSource> ().mute = false;
		}
		if (ButtonClick != null) {
			ButtonClick.GetComponent<AudioSource> ().mute = false;
		}

		if (GirlAngry != null) {
			GirlAngry.GetComponent<AudioSource> ().mute = false;
		}
		if (GirlHuh!= null) {
			GirlHuh.GetComponent<AudioSource> ().mute = false;
		}
		if (GameOver!= null) {
			GameOver.GetComponent<AudioSource> ().mute = false;
		}
		if (Ambient!= null) {
			Ambient.GetComponent<AudioSource> ().mute = false;
		}
	}


}

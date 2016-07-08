using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	GameObject Boy;
	GameObject Girl;
	GameObject Noodle;
	GameObject InputManager;

	int GenerateRandom;
	int GenerateRandomTime;

	int RandomBoy;
	int RandomGirl;

	static bool IsScreenPressed;

	bool boyHandMove = false;
	GameObject GirlSurprise;
	static bool gameover = false;

	// Use this for initialization
	void Start () 
	{
		Boy = GameObject.Find ("Boy");
		Girl = GameObject.Find ("Girl");
		Noodle = GameObject.Find ("Noodle");
		InputManager = GameObject.Find ("InputManager");
		Noodle.SetActive (false);
		StartCoroutine ("RandomTime");
		GameObject.Find ("Slurp Sound").GetComponent<AudioSource>().enabled = false;
		GirlSurprise = GameObject.Find ("Girl Surprise");
		GirlSurprise.SetActive (false);
		gameover = false;
	}

	IEnumerator RandomTime()
	{
		GenerateRandomTime = Random.Range (1,4);

		yield return new WaitForSeconds (GenerateRandomTime);
		GenerateRandom = Random.Range (1,3);
		if (GenerateRandom == 1) 
		{
			Boy_Idle1 ();
		} 
		else 
		{
			Boy_Idle2 ();
		}
			
		StartCoroutine ("RandomTime");
	}

	IEnumerator GirlLookdownSuspicious()
	{
		GenerateRandomTime = Random.Range (1,7);

		yield return new WaitForSeconds (GenerateRandomTime);
		if (InputManager.GetComponent<UserInput> ().ScreenPressed() == true) {
			RandomGirl = Random.Range (1, 3);
			switch (RandomGirl) {
			case 1:
				Girl_Suspecious ();
				break;
			case 2:
				Girl_LookDown ();
				break;
			default:
				Girl_LookDown ();
				break;
			}
		} else 
		{
			Girl_WatchingTV ();
		}


	}

	void FixedUpdate()
	{
		if(boyHandMove == true && gameover == false)
		{
			Boy_NoodleSlurpAnimation ();
		}
		else
		{
			Boy_DefaultAnimation ();
		}
		if (Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Surprise Sprite Animation")
			|| Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Surprise Sprite Animation 1"))
		{
			GirlSurprise.SetActive (true);
		} 
		else 
		{
			GirlSurprise.SetActive (false);	
		}
		if (gameover == true)
			Boy.GetComponent<Animator> ().Play ("Boy Slurp Animation");
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Fire1"))		//Runs only one time
		{
			//Boy_NoodleSlurpAnimation ();
			boyHandMove = true;
			StartCoroutine ("GirlLookdownSuspicious");
		
		}
		if(Input.GetButtonUp("Fire1"))		//Runs only one time
		{
			if (!Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Angry")) {
				//Boy_DefaultAnimation ();
			}
			boyHandMove = false;
			Girl_WatchingTV ();
		}

		if(Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Angry"))
		{
			GameObject.Find("Girl Angry").GetComponent<AudioSource>().mute = false;
			if(GameObject.Find("Main Camera").GetComponent<ZoomInandOut>()!=null )
				GameObject.Find("Main Camera").GetComponent<ZoomInandOut>().StartZoomInOut();
		}
		else
		{
			GameObject.Find("Girl Angry").GetComponent<AudioSource>().mute = true;
		}


	}

	public void Boy_Idle1()
	{
		if (gameover == false) {
			Boy.GetComponent<Animator> ().SetBool ("BoyTaunt", true);
			//Boy.GetComponent<Animator> ().Play("Boy Taunt");
			if (GameObject.Find ("Slurp Sound").GetComponent<AudioSource> ().isActiveAndEnabled == true)
				GameObject.Find ("Slurp Sound").GetComponent<AudioSource> ().enabled = false;
		}else 
		{
			Boy_NoodleSlurpAnimation ();
		}

	}

	public void Boy_Idle2()
	{
		if (gameover == false) {
		Boy.GetComponent<Animator> ().SetBool("BoyTaunt", false);
		//Boy.GetComponent<Animator> ().Play("Boy Idle Animation");
		if(GameObject.Find ("Slurp Sound").GetComponent<AudioSource>().isActiveAndEnabled == true)
		GameObject.Find ("Slurp Sound").GetComponent<AudioSource>().enabled = false;
			}else 
			{
				Boy_NoodleSlurpAnimation ();
			}
	}

	public void Boy_DefaultAnimation()
	{
		Debug.Log ("Default" + gameover);
		if (GameObject.Find("BAR").GetComponent<BarController>().gameover == false) {
			Boy.GetComponent<Animator> ().SetInteger ("BoyAnimation", 0);		// Hand move of the boy and then noodle slurp animation
			//Boy.GetComponent<Animator> ().Play("Boy Default");
			Noodle.SetActive (false);
			if (GameObject.Find ("Slurp Sound").GetComponent<AudioSource> ().isActiveAndEnabled == true)
				GameObject.Find ("Slurp Sound").GetComponent<AudioSource> ().enabled = false;
		} else 
		{
			Boy_NoodleSlurpAnimation ();
			Debug.Log ("Default else " + gameover);
		}
	}

	public void Boy_NoodleSlurpAnimation()
	{
		Debug.Log ("Noodle Slurp");
		Boy.GetComponent<Animator> ().SetInteger("BoyAnimation", 1);		// Hand move of the boy and then noodle slurp animation
		//Boy.GetComponent<Animator> ().Play("Boy Slurp Animation");
		if(GameObject.Find ("Slurp Sound").GetComponent<AudioSource>().isActiveAndEnabled == false)
		GameObject.Find ("Slurp Sound").GetComponent<AudioSource>().enabled = true;

	}

	public void Boy_ScrewedUp()
	{
		Boy.GetComponent<Animator> ().SetInteger("BoyAnimation", 4);		// Hand move of the boy and then noodle slurp animation
	//	Boy.GetComponent<Animator> ().Play("Boy ScrewedUp Animation");
		if(GameObject.Find ("Slurp Sound").GetComponent<AudioSource>().isActiveAndEnabled == true)
		GameObject.Find ("Slurp Sound").GetComponent<AudioSource>().enabled = false;
	}


	public void Girl_WatchingTV()
	{
		Girl.GetComponent<Animator> ().SetInteger("GirlAnimation", 0);
		//Girl.GetComponent<Animator> ().Play("Girl Default Animation");
		Debug.Log ("Girl is watching Tv");
	}


	public void Girl_Suspecious()
	{
		Girl.GetComponent<Animator> ().SetInteger("GirlAnimation", 1);
		//Girl.GetComponent<Animator> ().Play("Girl Suspicious");
		GameObject.Find ("Girl Suspicious").GetComponent<AudioSource> ().Play ();
	}


	public void Girl_LookDown()
	{
		//Girl.GetComponent<Animator> ().Play("Girl Look Down");
		Girl.GetComponent<Animator> ().SetInteger("GirlAnimation", 2);
	}

	public void Girl_Angry()
	{
		Girl.GetComponent<Animator> ().SetBool("GirlCaughtBoy", true);
		Girl.GetComponent<Animator> ().Play("Girl Angry");
		GameObject.Find ("Girl Angry").GetComponent<AudioSource> ().Play ();
		
	}

	public void BoyAnimation()
	{
		if (Time.timeScale == 1)
		if (Boy.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Boy Slurp Animation")) {
			Noodle.SetActive (true);
		} else if (Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Angry")) {
			Noodle.SetActive (false);
		} 
	}

	public void GirlAnimation()
	{
		if(Time.timeScale == 1 && InputManager.GetComponent<UserInput>().ScreenPressed() == true)
		if (Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Suspicious 2 Animation") ) 
		{
			Girl_Angry ();
			Boy_ScrewedUp ();
		}
		else
			if (Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Look Down 2 Animation")) 
			{
				if(Noodle.activeInHierarchy == true)
				{
					Girl_Angry ();
					Boy_ScrewedUp ();
				}
			}

		if(Time.timeScale == 1 && InputManager.GetComponent<UserInput>().ScreenPressed() == false)
		if (Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Suspicious 2 Animation") )
		{
			Girl_WatchingTV ();
		}
		else if (Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Look Down 2 Animation") ) 
			{
				Girl_WatchingTV ();
			}
			
		if (Time.timeScale == 1 && Boy.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Boy Slurp Animation")) 
		{
			if (Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Detection Animation 0")
			    || Girl.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Detection Animation")) 
			{
				if (Noodle.activeInHierarchy == true) {
					Girl_Angry ();
					Noodle.SetActive (false);
					Boy_ScrewedUp ();
				}
			}
		}
	}



}

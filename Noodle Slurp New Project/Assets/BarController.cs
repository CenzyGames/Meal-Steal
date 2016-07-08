using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarController : MonoBehaviour {

	bool IsScreenPressed;
	GameObject BarRight;
	GameObject BarLeft;
	GameObject MessageText;
	public string [] messages;
	public Sprite [] images;
	int count = 0;
	GameObject Noodle;
	GameObject Hungry;
	GameObject Score;
	bool ShowNoodle = false;
	public bool gameover ;

	GameObject MsgForeGround;

	void Awake()
	{
		Noodle = GameObject.Find ("Noodle");
		Hungry = GameObject.Find ("Hungry");
		Hungry.SetActive (false);
		Score = GameObject.Find ("Score");
		gameover = false;
	}

	// Use this for initialization
	void Start () 
	{
		BarRight = GameObject.Find ("Progress Bar front Right");
		BarLeft = GameObject.Find ("Progress Bar front Left");
		MessageText = GameObject.Find ("MessageText");
		MessageText.SetActive (false);
		MsgForeGround = GameObject.Find ("Msg Foreground");
		count = 0;
		ShowNoodle = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameObject.Find ("Girl").GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Girl Angry")) 
		{
			UserInputStarted ();
			UserInputEnded ();
			BarRightIncrease ();
			BarLeftIncrease ();
		}

		if (IsScreenPressed == true)  // Screnarios that should happen is the screen is pressed by the player.
		{					
			if(GameObject.Find("Noodle") != null)
				GameObject.Find("Noodle").SetActive(true);
		} 
		else 
			if (IsScreenPressed == false)
			{
				if(GameObject.Find("Noodle") != null)
					GameObject.Find("Noodle").SetActive(false);
			}

		if (ShowNoodle) 
		{
			Noodle.SetActive (true);
		}
	}

	void UserInputStarted()						// If the screen is pressed by the player then 'IsScreenPressed = true'
	{
		if(Input.GetButtonDown("Fire1"))
		{
			IsScreenPressed = true;
			BarLeft.gameObject.transform.localScale = new Vector3 ( 0, BarLeft.gameObject.transform.localScale.y,0);
		}
	}

	void UserInputEnded()					// If the screen is NOT pressed by the player then 'IsScreenPressed = false'
	{
		if(Input.GetButtonUp("Fire1"))
		{
			IsScreenPressed = false;
		}
	}
		
	void BarRightIncrease()
	{
		if (IsScreenPressed &&  BarRight.gameObject.transform.localScale.x < 1) 
		{
			if (BarRight.gameObject.transform.localScale.x > 0.98f) 
			{
				if (count < messages.Length) 
				{
					BarRight.gameObject.transform.localScale = new Vector3 (0.02f + Time.deltaTime / 4, BarRight.gameObject.transform.localScale.y, 0);
					PrintMessage (messages [count] + "");

					count+=1;
					MsgForeGround.GetComponent<SpriteRenderer> ().sprite = images [count];
				}
			}
			
			BarRight.gameObject.transform.localScale = new Vector3 ( BarRight.gameObject.transform.localScale.x + Time.deltaTime/4, BarRight.gameObject.transform.localScale.y,0);

		}
		else
			if (!IsScreenPressed &&  BarRight.gameObject.transform.localScale.x > 0) 
			{
				BarRight.gameObject.transform.localScale = new Vector3 ( BarRight.gameObject.transform.localScale.x - Time.deltaTime/2, BarRight.gameObject.transform.localScale.y,0);
			}
	}

	void BarLeftIncrease()
	{
		if (!IsScreenPressed &&  BarLeft.gameObject.transform.localScale.x < 1) 
		{
			BarLeft.gameObject.transform.localScale = new Vector3 ( BarLeft.gameObject.transform.localScale.x + Time.deltaTime/4, BarLeft.gameObject.transform.localScale.y,0);

		}
			else
				if (!IsScreenPressed &&  BarLeft.gameObject.transform.localScale.x > 1) 
				{
					LeftBarMaximum ();
				}
	}

	void PrintMessage(string message)
	{
		MessageText.SetActive (true);
		MessageText.GetComponent<Text> ().text = message;

	}


	void LeftBarMaximum()
	{
		// Boy Slurp animation on
	
		ShowNoodle = true;
		gameover = true;
		Debug.Log ("GAME OVER " + gameover);
		GameObject.Find("Boy").GetComponent<Animator>().Play ("Boy Slurp Animation");
		Score.SetActive (false);
		Hungry.SetActive (true);
		StartCoroutine ("PlayGirlAngryAnimation");
		GameObject.Find ("Girl Angry").GetComponent<AudioSource>().Play();
	
		// No tap and other things to work
		// Noodle Activates
	}

	IEnumerator PlayGirlAngryAnimation()
	{
		yield return new WaitForSeconds (1);
		GameObject.Find ("Girl").GetComponent<Animator> ().Play ("Girl Angry");

	}

}

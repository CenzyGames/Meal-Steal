using UnityEngine;
using System.Collections;

public class Repeat : MonoBehaviour {

	public float xLimit;
	public float newXPoistionX;
	public float newXPoistionY;
	// Use this for initialization
	void Start () 
	{
		switch(this.gameObject.name)
		{
			case "Yellow": StartCoroutine("YellowRepeat"); break;
			case "Pink" : StartCoroutine("PinkRepeat");break;
			case "Blue" : StartCoroutine("BlueRepeat");break;
			default: break;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	IEnumerator YellowRepeat()
	{
		if(Time.timeScale==1)
		{
			if(transform.position.x < xLimit)
			{
				transform.position = new Vector3(newXPoistionX, -1.8f,0);;
			}
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine("YellowRepeat");
	}

	IEnumerator PinkRepeat()
	{
		if(Time.timeScale==1)
		{
			if(transform.position.x < xLimit)
			{
				transform.position = new Vector3(newXPoistionX, newXPoistionY,0);
			}
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine("PinkRepeat");
	}

	IEnumerator BlueRepeat()
	{
		if(Time.timeScale==1)
		{
			if(transform.position.x < xLimit)
			{
				transform.position = new Vector3(newXPoistionX, newXPoistionY,0);
			}
		}
		yield return new WaitForSeconds (0.01f);
		StartCoroutine("BlueRepeat");
	}
	
}

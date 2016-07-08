using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {

	float alphaValue = 1;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		if (gameObject.activeInHierarchy == true) 
		{
			if (alphaValue > 0.2f)
			{
				StartCoroutine("StartFade");
			}
			gameObject.GetComponent<Text> ().color = new Color(gameObject.GetComponent<Text> ().color.r,gameObject.GetComponent<Text> ().color.g, gameObject.GetComponent<Text> ().color.b, alphaValue);
		}

		if(gameObject.GetComponent<Text> ().color.a < 0.2f)
		{
			alphaValue = 1.0f;
			gameObject.GetComponent<Text> ().color = new Color(gameObject.GetComponent<Text> ().color.r,gameObject.GetComponent<Text> ().color.g, gameObject.GetComponent<Text> ().color.b, alphaValue);
			gameObject.SetActive (false); // Hides the gameObject after it is faded
		}
	}

	IEnumerator StartFade()
	{
		alphaValue -= alphaValue/60;
		yield return new WaitForSeconds(0.12f);
		StartCoroutine("StartFade");
	}
}

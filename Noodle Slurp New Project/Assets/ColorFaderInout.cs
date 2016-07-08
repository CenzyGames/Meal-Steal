using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorFaderInout : MonoBehaviour {

//	public Text text;
//	float duration = 1f; //0.5 secs
//	float currentTime = 0f;
//	float alpha;
//	// Use this for initialization
//	void Start () 
//	{
//		StartCoroutine("FadeIn");
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
////		alpha = Mathf.Lerp(1f, 0f, currentTime/duration);
////		text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
////		if(currentTime < 1 &&  currentTime < 0.5f)
////		{
////			currentTime += 0.01f;
////		}
////		else if(currentTime > 0  &&  currentTime < 0.5f)
////		{
////			currentTime -= 0.01f;	
////		}
//		//Color.Lerp (text, Color.clear, fadeSpeed * Time.deltaTime);
//	}
//
//	IEnumerator FadeIn ()
//	{
//		while (text.material.color.a < 1)
//		{
//			text.material.color +=  new Color( 1, 1, 1, 0.1f * Time.deltaTime * 2);  
//		}  
//		yield return new WaitForSeconds(2);
//		StartCoroutine("FadeOut"); 
//	}
//		
//	IEnumerator FadeOut()
//	{
//		while (text.material.color.a > 0)
//		{
//			text.material.color -= new Color( 1, 1, 1, 0.1f * Time.deltaTime * 2);  
//		}
//			yield return new WaitForSeconds(2);   
//		StartCoroutine("FadeIn");
//	}

}

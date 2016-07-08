using UnityEngine;
using System.Collections;

public class ZoomInandOut : MonoBehaviour {

	public float distanceZ	=	0.0f;

	GameObject Girl;
	int count = 0;

	int zoomCount = 0;

	// Use this for initialization
	void Start () 
	{
		Girl = GameObject.Find("Girl");
		zoomCount = 0;
		//	StartCoroutine("CameraZoomIn");
	}

	// Update is called once per frame
	void Update () 
	{
		if(count>300 || zoomCount == 2)
		{
			StopCoroutine("CameraZoomIn");
			StopCoroutine("CameraZoomOut");
			GetComponent<Camera>().orthographicSize = 3.19f;
			count = 0;
		}
	}

	IEnumerator CameraZoomIn()
	{
		yield return new WaitForSeconds(0.0001f);
		GetComponent<Camera>().orthographicSize -= Time.deltaTime;
		//	if(GameObject.Find("Girl").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Girl Angry") == true || GameObject.Find("Girl").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Girl Game Over Animation") == true)
		if(GetComponent<Camera>().orthographicSize <2.0f)
		{
			StartCoroutine("CameraZoomOut");
			zoomCount = 1;
		}
		else
		{
			StartCoroutine("CameraZoomIn");
		}

	}

	IEnumerator CameraZoomOut()
	{
		yield return new WaitForSeconds(0.0001f);
		GetComponent<Camera>().orthographicSize += Time.deltaTime;
		//	if(GameObject.Find("Girl").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Girl Angry") == true || GameObject.Find("Girl").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Girl Game Over Animation") == true)
		if(GetComponent<Camera>().orthographicSize>3.19f)
		{
			StartCoroutine("CameraZoomIn");
			zoomCount = 2;
		}
		else{
			StartCoroutine("CameraZoomOut");
			count++;

		}

	}

	public void StartZoomInOut()
	{
		//if(Girl.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Girl Angry"))
		{
			StartCoroutine("CameraZoomIn");
		}
	}
}

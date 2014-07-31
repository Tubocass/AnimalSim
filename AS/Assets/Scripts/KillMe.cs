using UnityEngine;
using System.Collections;

public class KillMe : MonoBehaviour {

	public float time = 3f; 


	void Start () 
	{

		Destroy(this.gameObject,time);
	}

	void Update()
	{
		Color gui = GetComponent<GUITexture>().color;
		gui.a-=Time.deltaTime/2f;
		GetComponent<GUITexture>().color = gui;
	}
}

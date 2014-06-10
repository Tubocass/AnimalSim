using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true;
	}


	void Update() {
		if (Input.GetKeyDown("escape"))
			Screen.lockCursor = false;
	}
	

}

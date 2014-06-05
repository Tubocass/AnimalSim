using UnityEngine;
using System.Collections;

public class interact : MonoBehaviour {
	private  bool bSeen = false;
	
	// Update is called once per frame
	void Update () 
	{
		if (bSeen)
			renderer.material.color = Color.red;
		else renderer.material.color = Color.white;
	}

	public void isSeen()
	{

		bSeen = !bSeen;

	}
}

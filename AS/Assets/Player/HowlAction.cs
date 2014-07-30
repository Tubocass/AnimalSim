using UnityEngine;
using System.Collections;

public class HowlAction : MonoBehaviour

{
	public AudioClip source1;
	 

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

				if (Input.GetKeyDown (KeyCode.H))
						audio.PlayOneShot (source1);
				if (Input.GetKeyDown (KeyCode.P))
						print ("PISSSSSSSSSSSSS");
		}

}


using UnityEngine;
using System.Collections;

public class interact : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		renderer.material.color = Color.white;
	
	}
public void OnLookEnter(){
		renderer.material.color = Color.red;

	}
}

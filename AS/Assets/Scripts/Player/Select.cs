using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
public RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

		if(Physics.Raycast (ray, out hit, 10)){
			if(hit.collider.gameObject.GetComponent<interact>() != null){
				hit.collider.gameObject.GetComponent<interact>().OnLookEnter();
		}
	}
}
}
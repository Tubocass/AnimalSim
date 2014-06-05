using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
public RaycastHit hit;


	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

		if(Physics.Raycast (ray, out hit, 10)){
			if(hit.collider.gameObject != null)
			{
				//GetComponent<GUITexture>().texture = images.

				if(Input.GetButtonDown("Fire1"))
				{
					Debug.Log("Ping");
					hit.collider.gameObject.GetComponent<interact>().isSeen();
				}else Debug.Log("Gnip");
			}
	}
}
}
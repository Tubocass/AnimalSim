using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
public RaycastHit hit;
public GUITexture crosshair;
Icons icons;


void Start()
{
	icons = new Icons();
}

// Update is called once per frame
void Update () 
{
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

		if(Physics.Raycast (ray, out hit, 10))
		{
			if(hit.collider.gameObject != null)
			{
				if(hit.collider.gameObject == GameObject.FindGameObjectWithTag("Shit"))
				crosshair = icons.sniff;

				if(Input.GetButtonDown("Fire1"))
				{
					Debug.Log("Ping");
					hit.collider.gameObject.GetComponent<interact>().isSeen();
				}else Debug.Log("Gnip");
			}
		}
}
}
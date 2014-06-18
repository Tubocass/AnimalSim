using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
public RaycastHit hit;

Texture texture;
Icons icons;


void Start()
{
	icons = GetComponent<Icons>();
	
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
				{
					Debug.Log("Ping");
					GetComponent<GUITexture>().texture = icons.sniff;

					if(Input.GetButtonDown("Fire1"))
					{

						hit.collider.gameObject.GetComponent<interact>().isSeen();
					}
				}else GetComponent<GUITexture>().texture = icons.none;
			} 
		}
}
}
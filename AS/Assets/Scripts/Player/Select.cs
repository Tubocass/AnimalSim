using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour 
{
	public RaycastHit hit;

	Icons icons;
	public GameObject scratch;
	public AudioClip roar;


	void Start()
	{
		icons = GetComponent<Icons>();
		
	}

	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		GetComponent<GUITexture>().texture = icons.none;
		if(Physics.Raycast (ray, out hit, 5))
		{
			if(hit.collider.gameObject != null)
			{
				switch(hit.collider.gameObject.tag)
				{
					//Debug.Log("Ping");
					case "NPC":
					GetComponent<GUITexture>().texture = icons.scratch;
					if(Input.GetButtonDown("Fire1"))
					{
						Instantiate(scratch,new Vector3(0,0,1),Quaternion.identity);
						audio.PlayOneShot(roar);
						Destroy(hit.collider.gameObject,4);
						/*
						 * destroy object
						 * decrease hunger
						*/
					}
					break;
				}
			} 
		}else if(Physics.Raycast (ray, out hit, 10))
		{
			if(hit.collider.gameObject != null)
			{
				switch(hit.collider.gameObject.tag)
				{
					case  "Shit":
					GetComponent<GUITexture>().texture = icons.sniff;
					if(Input.GetButtonDown("Fire1"))
					{
							
						hit.collider.gameObject.GetComponent<interact>().isSeen();
					}
					break;
					case "Food":
					GetComponent<GUITexture>().texture = icons.eat;
					if(Input.GetButtonDown("Fire1"))
					{
						Destroy(hit.collider.gameObject,3);
						/*
						* destroy object
						* decrease hunger
						*/
					}
					break;
				}
			}
		}
	}
}

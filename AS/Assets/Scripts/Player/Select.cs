using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour 
{
	public RaycastHit hit;
	public GameObject scratchFab;
	public AudioClip roar;
//	public Texture none;
//	public Texture howl;
//	public Texture sniff;
//	public Texture bite;
//	public Texture grab;
//	public Texture eat;
//	public Texture pee;
//	public Texture scratch;
	[SerializeField]LayerMask mask;
	Icons icons;

	void Start()
	{
		icons = GetComponent<Icons>();	
	}

	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
		GetComponent<GUITexture>().texture = icons.none; 
		if(Physics.Raycast (ray, out hit, 10, mask))
		{
			if(!hit.collider.isTrigger && hit.collider.gameObject != null)
			{
				Debug.Log(hit.collider.gameObject.tag);
				switch(hit.collider.gameObject.tag)
				{
					case "NPC":
						GetComponent<GUITexture>().texture = icons.scratch;
						if(Input.GetButtonDown("Fire1"))
						{
							Instantiate(scratchFab,new Vector3(0,0,1),Quaternion.identity);
							//GetComponent<AudioSource>().PlayOneShot(roar);
							Destroy(hit.collider.gameObject,4);
							/*
							 * destroy object
							 * decrease hunger
							*/
						}
						break;
					case  "Shit":
						GetComponent<GUITexture>().texture = icons.sniff;
						if(Input.GetButtonDown("Fire1"))
						{
							hit.collider.gameObject.GetComponent<ShitController>().IsVisible();
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

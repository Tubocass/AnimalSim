using UnityEngine;
using System.Collections;

public class Herd : MonoBehaviour 
{
	public GameObject prefab;
	public int numberOfObjects = 6;
	public float spawnRadius = 5f;
	GameObject[] herd;
	Vector3 targetLocation;
	void Start()
	{
		targetLocation = ClosestFood(transform.position);
		herd = Spawn();

		/*foreach(GameObject member in herd)
		{
			AstarAI aStar = member.GetComponent<AstarAI>();
			aStar.targetLocation = targetLocation;
			aStar.StartPath();
		}*/
	}
	void Update()
	{

	}
	public Vector3 getTargetLocation()
	{
		return targetLocation;
	}
	Herd getHerd()
	{
		return this;
	}
	
	GameObject[] Spawn()
	{
		GameObject[] herd = new GameObject[numberOfObjects];
		for (int i = 0; i < numberOfObjects; i++) 
		{
			//float angle = (i * Mathf.PI * 2 / numberOfObjects);
			//Vector3 pos = transform.position + (new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius);
			
			Vector2 circle = Random.insideUnitCircle * spawnRadius;
			Vector3 pos = transform.position+ new Vector3 (circle.x, 0 ,circle.y);
			herd[i] = (GameObject)Instantiate(prefab, pos, Quaternion.identity);

		}
		return herd;
	}

	public Vector3 ClosestFood(Vector3 position)
	{
		GameObject[] foods;
		foods = GameObject.FindGameObjectsWithTag("Food");
		if(foods.Length>0)
		{
			GameObject closest;
			closest = foods[Random.Range(0,foods.Length)];//Not closet, just random
			float distance = 500;

			foreach (GameObject fo in foods) 
			{
				Vector3 diff = fo.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) 
				{
					closest = fo;
					distance = curDistance;
				}
			}
			return closest.transform.position;
		}else return position;
	}

}

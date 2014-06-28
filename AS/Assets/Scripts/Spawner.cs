using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{

	public GameObject prefab;
	public int numberOfObjects = 6;
	public float radius = 5f;

	void Start()
	{
		for (int i = 0; i < numberOfObjects; i++) 
		{
			//float angle = (i * Mathf.PI * 2 / numberOfObjects);
			//Vector3 pos = transform.position + (new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius);

			Vector2 circle = Random.insideUnitCircle * radius;
			Vector3 pos = transform.position+ new Vector3 (circle.x, 0 ,circle.y);
			Instantiate(prefab, pos, Quaternion.identity);
		}
	}
}

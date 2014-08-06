using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {


	void Update()
	{
		/*if (rigidbody.velocity.magnitude < .1) 
		{
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			collider.isTrigger = true;
		}*/
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag ("NPC"))
		Destroy(this.gameObject,5);
	}

	void OnTriggerEnter(Collider other)
	{


		if (other.gameObject.CompareTag ("NPC")) 
		{
			print("Bite Me");
			Destroy(this.gameObject,5);
		}
			 
	}
}

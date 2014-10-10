using UnityEngine;
using System.Collections;

public class Anim : MonoBehaviour {

	Animator animator;
	public float speed = 0f;
	CharacterController controller;
	Rigidbody rigid;

	void Start()
	{
		animator = GetComponent<Animator> ();
		rigid = rigidbody;
		controller = transform.parent.GetComponent<CharacterController> ();
		animator.SetFloat("Speed",0);

	}

	void FixedUpdate () 
	{
		if(controller!=null)
		{
			speed = controller.velocity.magnitude;
			animator.SetFloat("Speed",speed);

		}else if(rigid!=null)
		{
			speed = rigid.velocity.magnitude;
			animator.SetFloat("Speed",speed);
		}
		if (speed > .1) 
		{
			float idle = Random.value;
			animator.SetFloat("Idle",idle);
			
		}
				
	}
}

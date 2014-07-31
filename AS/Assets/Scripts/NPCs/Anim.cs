using UnityEngine;
using System.Collections;

public class Anim : MonoBehaviour {

	Animator animator;
	public float speed;

	void Start()
	{
		animator = GetComponent<Animator> ();
		animator.SetFloat("Speed",0);

	}

	void FixedUpdate () 
	{
		//if(rigidbody.velocity.magnitude<10)
		animator.SetFloat("Speed",GetComponent<CharacterController>().velocity.magnitude);
		speed = animator.GetFloat("Speed");
				
	}
}

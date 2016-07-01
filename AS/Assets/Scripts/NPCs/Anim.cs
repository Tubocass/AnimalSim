using UnityEngine;
using System.Collections;

public class Anim : MonoBehaviour {

	Animator animator;
	public float speed = 0f;
	CharacterController controller;
	Rigidbody rigid;
	int idleHash = Animator.StringToHash("Base Layer.Idle");
	int biteHash = Animator.StringToHash("Base Layer.Bite");

	void Start()
	{
		animator = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody>();
		controller = transform.parent.GetComponent<CharacterController> ();

	}

	public void OnBite()
	{
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash!=biteHash)
		{
			animator.SetBool("Bite",true);
		}
	}
	public void EndBite()
	{
		animator.SetBool("Bite",false);
	}

	void Update () 
	{
		if(controller!=null)
		{
			speed = controller.velocity.magnitude;

		}else if(rigid!=null)
		{
			speed = rigid.velocity.magnitude;
		}

		animator.SetFloat("Speed",speed);
		if (animator.IsInTransition(0)&& animator.GetNextAnimatorStateInfo(0).nameHash == idleHash) 
		{
			float idle = Random.value;
			animator.SetFloat("Idle",idle);
			
		}

				
	}
}

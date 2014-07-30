using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour {
	public CollisionFlags collision;
	public float moveSpeed = 4;
	public float jumpSpeed = 4;
	public float turnSpeed = 4;
	public float gravity = 10;
	public Vector3 moveDirection;
	public CharacterController herd;
	//public GameObject go;
//	public Transform nPC;

	private AnimationState idle;
	private AnimationState walk;
	private AnimationState run;
	// Use this for initialization
	void Start () {
		herd = GetComponent<CharacterController> ();
		moveDirection = Vector3.zero;
		print ("step1");
		animation.wrapMode = WrapMode.Loop;
		animation ["Big_Cat_Idle"].wrapMode = WrapMode.Once;
		animation ["Big_Cat_Idle"].layer = 1;

		idle = animation ["Big_Cat_Idle"];
		walk = animation ["Big_Cat_Walk"];
		run = animation ["Big_Cat_Walk"];



	}
	
	// Update is called once per frame
	void Update () {
		//AstarAI speedcheck = new AstarAI();

	//	nPC = GameObject.FindGameObjectWithTag(Tags.food).transform;
	//	go.transform.LookAt (nPC);
				animation.CrossFade ("Big_Cat_Walk");
		print ("alwayson");
				if (Input.GetAxis ("Horizontal") != 0) {
						transform.Rotate (0, Input.GetAxis ("Horizontal") / turnSpeed, 0);
			print ("ok");
				}
	//	if (speedcheck.speed > 0) {
	//					animation.Play ("Big_Cat_Walk");
		//		}
	//	if (speedcheck.speed == 0){
		//			animation.Play("Big_Cat_Idle");
	//	}
				


				moveDirection.y -= gravity * Time.deltaTime;

				collision = herd.Move (moveDirection * Time.deltaTime);

	if (Input.GetAxis("Vertical") != 0){
		animation.Play("Big_Cat_Walk");
			print ("ok2");
	}






}
}
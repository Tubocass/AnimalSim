using UnityEngine;
using System.Collections;

public class NPCActions : MonoBehaviour 
{
	AIPath AStar;
	Anim anim;
	SphereCollider col;
	//GameObject[] aVisibleFood = new GameObject[10];
	Queue qVisibleFood = new Queue(6);
	public Transform target;
	public float pushPower = 2.0F;
	public bool bHungry;
	public bool bFoodVisible;
	bool bSearching;
	float fieldOfViewAngle = 110;
	public float hunger;
	public enum State{Eating,Searching,Idle,Moving};
	public State state;
	Vector3 dLoaction;
	
	
	void Start()
	{
		state = State.Idle;
		AStar = GetComponent<AIPath>();
		col = GetComponent<SphereCollider>();
		anim = GetComponentInChildren<Anim>();
	}
	
	void Update()
	{
		if (hunger > 60) 
		{
			bHungry = true;
			state = State.Searching;
			
		}else bHungry = false;
		
		StartCoroutine("Behaviour",state);
		
	}
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
		if (hit.gameObject.CompareTag ("Food")) 
		{
			Rigidbody body = hit.collider.attachedRigidbody;
			
			Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
			body.velocity = pushDir * pushPower;
		}
	}
	void PickRandomLocation()
	{
		float dx = Random.Range (-10, 10);
		float dy = Random.Range (-10, 10);
		float dz = Random.Range (-10, 10);
		dLoaction = new Vector3 (dx, 0, dz);
		AStar.setTargetLocation (dLoaction);
	}
	public void Bite()
	{
		state = State.Eating;
		anim.OnBite();
		Debug.Log("Ass");
	}
	public void EndBite()
	{
		//StopCoroutine("Behaviour");
		state = State.Idle;
		anim.EndBite();
		Debug.Log("boobs");

		//StartCoroutine("Behaviour",state);
	}

	IEnumerator Behaviour(State state)
	{
		switch(state)
		{
		case State.Idle: 
			AStar.canSearch = false;
			AStar.canMove = false;
			PickRandomLocation();
			//hunger+=10;
			yield return new WaitForSeconds(5); 
			break;
		case State.Searching: 
			AStar.canSearch = true;
			AStar.canMove = true;
	
			/*if (qVisibleFood.Count>0)
			{
				Transform food = (Transform)qVisibleFood.Dequeue(); 
				if (GameObject.Find(food.gameObject.name)!=null)
				{
					AStar.setTarget(food);
					//AStar.SearchPath();
					state = State.Moving;
					break;
				}
			}*/
			yield return null;
			break;
		case State.Moving:
			AStar.canMove = true;
			AStar.canSearch = false;
			if(AStar.TargetReached)
				Bite ();
			//AStar.SearchPath();
			yield return null;
			break;
		case State.Eating:
			//anim.OnBite();
			yield return new WaitForSeconds(1);
			EndBite();
			yield return null;
			break;
		}
	}
	

	void OnTriggerStay (Collider other)
	{
		if(other.gameObject.tag == "Food")
		{
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
				{
					
					if(hit.collider.gameObject.tag == "Food")
					{
						Debug.DrawRay(transform.position+ transform.up, direction, Color.red);
						bFoodVisible = true;
						target = other.transform;
						Debug.Log("Found IT!");
						qVisibleFood.Enqueue(target);
					}
				}
			}
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.tag == "Food")
			bFoodVisible = false;
	}
}
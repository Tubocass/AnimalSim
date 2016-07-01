using UnityEngine;
using System.Collections;

public class NPCActions : MonoBehaviour 
{
	SphereCollider col;
	//GameObject[] aVisibleFood = new GameObject[10];
	//Queue qVisibleFood = new Queue(6);
	//public Transform target;
	//public bool bHungry;
	//public bool bFoodVisible;
	//bool bSearching;
	[SerializeField]float fieldOfViewAngle = 110;
	//public float hunger;
	//public enum State{Eating,Searching,Idle,Moving};
	//public State state;
	[SerializeField]Vector3 destination;
	[SerializeField] bool bFleeing;
	LayerMask playerMask, groundMask;
	Animator animator;
	bool bMoving;
	NavMeshAgent agent;
	int idleHash = Animator.StringToHash("Base Layer.Idle");
	int biteHash = Animator.StringToHash("Base Layer.Bite");
	
	
	void OnEnable()
	{
		agent = GetComponent<NavMeshAgent>();
		destination = PickRandomLocation(transform.position,100);
		animator = GetComponent<Animator> ();
		col = GetComponent<SphereCollider>();
		playerMask = 1<<LayerMask.NameToLayer("Player");
		groundMask = 1<<LayerMask.NameToLayer("Terrain");

	}
	
	void Update()
	{
		if(!bMoving)
		{
			destination = PickRandomLocation(transform.position,100);
			if(destination!=transform.position)
			{
				MoveTo(destination);
			}
		}
		if (animator.IsInTransition(0)&& animator.GetNextAnimatorStateInfo(0).nameHash == idleHash) 
		{
			float idle = Random.value;
			animator.SetFloat("Idle",idle);
			
		}
		
//		if (hunger > 60) 
//		{
//			bHungry = true;
//			state = State.Searching;
//			
//		}else bHungry = false;
		
	}

	public void MoveTo(Vector3 location)
	{
		//agent.ResetPath();
		bMoving = true;
		//currentVector = location;
		agent.SetDestination(location);
		StopCoroutine("MovingTo");
		StartCoroutine("MovingTo");

	}
	IEnumerator MovingTo()
	{
		while(bMoving)
		{
			animator.SetFloat("Speed", agent.velocity.magnitude);
			if(agent.remainingDistance<1)
			{
				animator.SetFloat("Speed", 0f);
				yield return new WaitForSeconds(1f);
				bMoving = false;
				Debug.Log("I arrived");
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	Vector3 PickRandomLocation(Vector3 origin, float range)
	{
		float xx = Random.Range(-range,range)+origin.x;
		float zz = Random.Range(-range,range)+origin.z;
		return GroundCheck(new Vector3(xx,500,zz));
//		RaycastHit hit = new RaycastHit();
//		Physics.Raycast(new Vector3(xx,500f,zz),Vector3.down,out hit,1000f,groundMask);
//		NavMeshPath path = new NavMeshPath();
//		agent.CalculatePath(hit.point,path);
//		if(path.status != NavMeshPathStatus.PathPartial)
//		{
//			return hit.point;
//		}else return origin;

	}

	Vector3 Flee(Vector3 enemyLoc)
	{
		Vector3 fleeTo, dirToFlee, dirToEnemy = enemyLoc - transform.position;
		do{
		 fleeTo = PickRandomLocation(transform.position, 250);
		 dirToFlee = fleeTo-transform.position;
		}while(Vector3.Dot(dirToFlee ,dirToEnemy)>0);//while the flee location is towards the enemy
		return fleeTo;
	}

	Vector3 GroundCheck(Vector3 origin)
	{
		RaycastHit hit = new RaycastHit();
		Physics.Raycast(origin,Vector3.down,out hit,1000f,groundMask);
		NavMeshPath path = new NavMeshPath();
		agent.CalculatePath(hit.point,path);
		if(path.status != NavMeshPathStatus.PathPartial)
		{
			return hit.point;
		}else return origin;
	}
	public void Bite()
	{

	}
	public void OnTargetReached()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				if(Physics.Raycast(transform.position, direction, out hit, col.radius))
				{
					if(hit.collider.gameObject.tag == "Player")
					{
						if(!bFleeing)
						{
							destination = Flee(other.transform.position);
							if(destination!=transform.position)
							{
								MoveTo(destination);
								bFleeing = true;
							}
							Debug.Log(destination.ToString());
						}

					}
				}
			}
		}
	}
	void OnTriggerStay (Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;
				if(Physics.Raycast(transform.position, direction, out hit, col.radius))
				{
					if(hit.collider.gameObject.tag == "Player")
					{
						Debug.DrawRay(transform.position, direction, Color.red);
						if(!bFleeing)
						{
							destination = Flee(other.transform.position);

							if(destination!=transform.position)
							{
								MoveTo(destination);
								bFleeing = true;
							}
							Debug.Log(destination.ToString());
						}
					}
				}
			}
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			bFleeing = false;
		}
	}
}
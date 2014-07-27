using UnityEngine;
using System.Collections;
using Pathfinding;

public class AstarAI : MonoBehaviour {
	
	public Transform target;
	public Vector3 targetPosition;
	
    private Seeker seeker;
    private CharacterController controller;
 
    //The calculated path
    public Path path;
    
    //The AI's speed per second
    public float speed = 500;
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
 
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

	public int counter = 120;
	
	
	public void Start () {
		target = ClosestFood().transform;
		targetPosition = target.transform.position;
		//Get a reference to the Seeker component we added earlier
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        
		
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
	}
	
    public void OnPathComplete (Path p) {
        //Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error) 
		{
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
		}else Debug.Log(p.error);
    }
 
    public void FixedUpdate () {
        if (path == null) {
            //We have no path to move after yet
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count) 	
		{
            Debug.Log ("End Of Path Reached");
			if (counter<=0)
			{
				targetPosition = ClosestFood().transform.position;
				seeker.StartPath (transform.position,targetPosition, OnPathComplete);
				counter = 120;
			}
		
			counter -=1;
            return;
        }
        
        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.Move (dir);
        
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }


    }
	private GameObject ClosestFood()
	{
		GameObject[] foods;
		foods = GameObject.FindGameObjectsWithTag("Food");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

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
		return closest;
	}
			

	
}

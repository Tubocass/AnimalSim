using UnityEngine;
using System.Collections;
using Pathfinding;

public class AstarAI : MonoBehaviour {
	
	public Transform target;
	public Vector3 targetLocation;
	
    private Seeker seeker;
    private CharacterController controller;
	public Herd herdController;
 
    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed = 100;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 1;
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

	public int counter = 120;
	Vector3 dir;
	
	
	public void Start () 
	{ 
		targetLocation = herdController.getTargetLocation();
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
		StartPath();

	}
	
    public void OnPathComplete (Path p) 
	{
        //Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error) 
		{
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
		}else Debug.Log(p.error);
    }
	public void StartPath()
	{
		seeker.StartPath (transform.position,targetLocation, OnPathComplete);
	}
 
    public void FixedUpdate () 
	{
        if (path == null) 
		{
            //We have no path to move after yet
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count) 	
		{
            Debug.Log ("End Of Path Reached");
			if (counter<=0)
			{
				targetLocation = herdController.ClosestFood(transform.position);
				if (Vector3.Distance (transform.position,targetLocation) >nextWaypointDistance)
				{
					seeker.StartPath (transform.position,targetLocation, OnPathComplete);
				}
				counter = 120;
			}
		
			counter -=1;
            return;
        }
        
        //Direction to the next waypoint
        dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.Move (dir);
        
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) 
		{
            currentWaypoint++;
            return;
        }
    }
	
}

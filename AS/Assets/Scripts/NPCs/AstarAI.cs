using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	public float step = 5;
	Vector3 newDir;
	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 1;
	public float stoppingDistance  = 2;
	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;
	private bool targetReached;
	public int counter = 120;

	void Awake() 
	{
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
	}
	/*
	public void Start () 
	{
		startHasRun = true;
		OnEnable ();
	}
	/** Run at start and when reenabled.
	 * Starts RepeatTrySearchPath.
	 * 
	 * \see Start

	protected virtual void OnEnable () {
		
		lastRepath = -9999;
		canSearchAgain = true;
		
		lastFoundWaypointPosition = GetFeetPosition ();
		
		if (startHasRun) {
			//Make sure we receive callbacks when paths complete
			seeker.pathCallback += OnPathComplete;
			
			StartCoroutine (RepeatTrySearchPath ());
		}
	}
	
	public void OnDisable () {
		// Abort calculation of path
		if (seeker != null && !seeker.IsDone()) seeker.GetCurrentPath().Error();
		
		// Release current path
		if (path != null) path.Release (this);
		path = null;
		
		//Make sure we receive callbacks when paths complete
		seeker.pathCallback -= OnPathComplete;
	}
	
	/** Tries to search for a path every #repathRate seconds.
	  * \see TrySearchPath

	protected IEnumerator RepeatTrySearchPath () {
		while (true) {
			float v = TrySearchPath ();
			yield return new WaitForSeconds (v);
		}
	}
	
	/** Tries to search for a path.
	 * Will search for a new path if there was a sufficient time since the last repath and both
	 * #canSearchAgain and #canSearch are true and there is a target.
	 * 
	 * \returns The time to wait until calling this function again (based on #repathRate) 

	public float TrySearchPath () {
		if (Time.time - lastRepath >= repathRate && canSearchAgain && canSearch && target != null) {
			SearchPath ();
			return repathRate;
		} else {
			//StartCoroutine (WaitForRepath ());
			float v = repathRate - (Time.time-lastRepath);
			return v < 0 ? 0 : v;
		}
	}
	
	/** Requests a path to the target 
	public virtual void SearchPath () {
		
		if (target == null) throw new System.InvalidOperationException ("Target is null");
		
		lastRepath = Time.time;
		//This is where we should search to
		Vector3 targetPosition = target.position;
		
		canSearchAgain = false;
		
		//Alternative way of requesting the path
		//ABPath p = ABPath.Construct (GetFeetPosition(),targetPoint,null);
		//seeker.StartPath (p);
		
		//We should search from the current position
		seeker.StartPath (GetFeetPosition(), targetPosition);
	}
*/
	public void OnPathComplete (Path p) 
	{
		//Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
		if (!p.error) 
		{
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
			targetReached = false;
		}else Debug.Log(p.error);
	}
	public void StartPath()
	{
		seeker.StartPath (transform.position,targetLocation, OnPathComplete);
	}
	public void OnTargetReached()
	{
		if (Vector3.Distance (transform.position,targetLocation) > stoppingDistance)
			StartPath();
	}
	public void FixedUpdate () 
	{
		targetLocation = target.position;
		Vector3 dir = calcV(transform.position);
		controller.Move (dir);
	}
	public Vector3 calcV(Vector3 currentPosition)
	{
		if (path == null || path.vectorPath == null || path.vectorPath.Count == 0) return Vector3.zero;
		List<Vector3> vPath = path.vectorPath;
		
		if (currentWaypoint >= vPath.Count) { currentWaypoint = vPath.Count-1; }
		
		if (currentWaypoint == vPath.Count) 	
		{
			Debug.Log ("End Of Path Reached");

			if (Vector3.Distance (currentPosition,targetLocation) < stoppingDistance)
				{
					targetReached = true;
					OnTargetReached();
				}
			//return Vector3.zero;
		}
		if (currentWaypoint < vPath.Count) 
		{

			//Check if we are close enough to the next waypoint
			//If we are, proceed to follow the next waypoint
			float dist = Vector3.Distance (currentPosition,vPath[currentWaypoint]);
			if (dist < nextWaypointDistance) 
			{
				currentWaypoint++;
			}

		}
		
		//Direction to the next waypoint
		//rotate
		Vector3 targetdir = vPath[currentWaypoint]-currentPosition;
		targetdir.y = 0;
		newDir = Vector3.RotateTowards(transform.forward, targetdir, 3, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
		//move
		Vector3 dir = (path.vectorPath[currentWaypoint]-currentPosition).normalized;
		dir *= speed * Time.fixedDeltaTime;
		return dir;

	}
	public void setTarget(Transform t)
	{
		this.target = t;
	}
	
}

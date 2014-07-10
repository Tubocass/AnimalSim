using UnityEngine;
using System.Collections;

public class NPCActions : MonoBehaviour 
{
	AstarAI star;
	enum curState{Idle, LookingForFood, MovingToFood, Eating , RunningAway, Sleeping, LookingForSleep, LookingToPotty, MovingToPotty}



	public void FixedUpdate()
	{
		if(Vector3.Distance (transform.position,star.targetLocation)<3)
			if(bFoodTargeted = true)
	}

	public void Behavior()
	{
		switch(curState)
		{
			case curState.LookingForFood:
		}

	}
}

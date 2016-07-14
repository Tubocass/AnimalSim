using UnityEngine;
using System.Collections;

public class NPCBase : MonoBehaviour 
{
	public float Health{get{return health;}}
	public float Stamina{get{return stamina;}}
	public float Hunger{get{return hunger;}}
	public float Thirst{get{return thirst;}}
	private float health, hunger, thirst, stamina;
	[SerializeField]protected float maxHealth, maxStamina;

	void OnEnable()
	{
		health = maxHealth;
		stamina = maxStamina;
	}

	public void SetHealth(float n)
	{
		health += n;
		Mathf.Clamp(health,0,maxHealth);
	}
	public void SetHunger(float n)
	{
		hunger += n;
		Mathf.Clamp(hunger,0,100);
	}
	public void SetThirst(float n)
	{
		thirst += n;
		Mathf.Clamp(thirst,0,100);
	}
	public void SetStamina(float n)
	{
		stamina += n;
		Mathf.Clamp(stamina,0,maxStamina);
	}

	
}

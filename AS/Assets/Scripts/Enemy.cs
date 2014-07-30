using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	private GameObject player;
	void  Start (){
		player = GameObject.FindGameObjectWithTag("Player");
	}
	void  OnTriggerEnter ( Collider other  ){
		if(other.gameObject == player){
			player.GetComponent<HealthScript>().health -= 10;
			print ("hey");
		}
	}
}
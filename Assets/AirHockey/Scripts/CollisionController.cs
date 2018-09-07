using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

	public GameController tableController;
	PlayerDataManagement playerData;
	// force for testing
	float force = 17;
	// force in AR environment
	public float trueForce = 0.5f;
	// Use this for initialization
	void Start () {
		playerData = GameObject.Find("PlayerData").GetComponent<PlayerDataManagement>();
	}


	void OnCollisionEnter(Collision other) {
	
		if(other.gameObject.name == "scoreZone_1"){
			Debug.Log("p1 win score");
			playerData.p1ScoreAdded();
			tableController.showScoreText("player 1");
			tableController.initialPuckPosition();
		}else if(other.gameObject.name == "scoreZone_2"){
			playerData.p2ScoreAdded();
			tableController.showScoreText("player 2");
			tableController.initialPuckPosition();
		}else if(other.gameObject.tag == "Paddle"){
			Debug.Log("hit paddle: "+ other.gameObject.name);
			if(other.gameObject.name == "paddle2"){
				other.gameObject.GetComponentInParent<AIScript>().toMove = false;
				Debug.Log("to move == false");
			}		
			GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
		    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			// get the angle between collision point and the puck
			Vector3 dir = other.contacts[0].point - transform.position;
			// Debug.Log("dir: "+ dir.normalized);
			//get the opposit of the direction and normalized it
			dir = -dir.normalized;
			GetComponentInParent<Rigidbody>().velocity = new Vector3(dir.x*trueForce, 0, dir.z*trueForce);
			// GetComponent<Rigidbody>().AddForce(dir*force);
			
		}
	}

	void OnCollisionExit(Collision other) {
		// Debug.Log("collision exit");
	}

}

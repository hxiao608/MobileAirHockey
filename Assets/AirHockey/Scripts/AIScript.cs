using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AIScript : MonoBehaviour {
	// test speedda	float maxMovementSpeed = 10.0f;
	
	public float maxMovementSpeedinAR = 6.0f;
	Rigidbody rb;
	Vector3 startingPosition;
	public GameObject puck;
	public Transform AIBoundaryHolder;
	Boundary AIBoundary;
	public Transform PuckBoundaryHolder;
	Boundary PuckBoundary;
	Vector3 targetPosition;
	bool firstTimeInOpponentsHalf = true;
	public bool isStartPos = true;
	PlayerDataManagement playerData;
	public bool toMove;
	float offsetFromTarget;
	// Use this for initialization
	void Start () {
		rb = this.GetComponentInChildren<Rigidbody>();
		playerData = GameObject.Find("PlayerData").GetComponent<PlayerDataManagement>();
		startingPosition = transform.localPosition;
		AIBoundary = new Boundary(AIBoundaryHolder.GetChild(0).localPosition.z,
									AIBoundaryHolder.GetChild(1).localPosition.z,
									AIBoundaryHolder.GetChild(2).localPosition.x,
									AIBoundaryHolder.GetChild(3).localPosition.x);

		PuckBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).localPosition.z,
									PuckBoundaryHolder.GetChild(1).localPosition.z,
									PuckBoundaryHolder.GetChild(2).localPosition.x,
									PuckBoundaryHolder.GetChild(3).localPosition.x);	
		Debug.Log("puck position: "+puck.transform.localPosition);
		Debug.Log("AI paddle position: " + transform.localPosition);
		Debug.Log ("AIboundary up:" + AIBoundary.Up+"\nAIboundary down:" + AIBoundary.Down+"\nAIboundary left: " + AIBoundary.Left+"\nAIboundary right: "+ AIBoundary.Right);
		Debug.Log ("Puckboundary up:" + PuckBoundary.Up+"\nPuckboundary down:" + PuckBoundary.Down+"\nPuckboundary left: " + PuckBoundary.Left+"\nPuckboundary right: "+ PuckBoundary.Right);
	}
	
	// Update is called once per frame

	// void FixedUpdate() {
	// 	float movementSpeed;
	// 	if(puck.transform.localPosition.z < PuckBoundary.Down){
	// 		// puck in player's
	// 		if(firstTimeInOpponentsHalf){
	// 			firstTimeInOpponentsHalf = false;
	// 			offsetFromTarget = Random.Range(-1f,1f);
	// 		}
	// 		movementSpeed = maxMovementSpeed * Random.Range(0.1f,0.3f);
	// 		targetPosition = new Vector3(Mathf.Clamp(puck.transform.localPosition.x + offsetFromTarget, AIBoundary.Left,AIBoundary.Right),startingPosition.y,startingPosition.z);
	// 	}else{
	// 		// puck in AI's side
	// 		firstTimeInOpponentsHalf = true;
	// 		movementSpeed = Random.Range(maxMovementSpeed * 0.4f, maxMovementSpeed);
	// 		targetPosition = new Vector3 (Mathf.Clamp(puck.transform.localPosition.x,AIBoundary.Left,AIBoundary.Right),startingPosition.y,
	// 									Mathf.Clamp(puck.transform.localPosition.z,AIBoundary.Down,AIBoundary.Up));
	// 	}
		
	// 		transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition,  movementSpeed * Time.fixedDeltaTime);
	// } 
	
	void FixedUpdate() {
		float movementSpeed;
		if(transform.localPosition == startingPosition){
			isStartPos = true;	
		}else{
			isStartPos = false;
		}

		if(puck.transform.localPosition.z < PuckBoundary.Down){
			// puck in player's side
			firstTimeInOpponentsHalf = true;
			movementSpeed = maxMovementSpeedinAR * Random.Range(0.1f,0.5f);
			targetPosition = startingPosition;
		}else{
			// puck in AI's side
			if(isStartPos && !toMove){
				toMove = true;
			}
			if(toMove){
				// AI paddle hit the puck || after AI paddle go back to startpos
				movementSpeed = Random.Range(maxMovementSpeedinAR * 0.5f, maxMovementSpeedinAR);
				targetPosition = new Vector3 (Mathf.Clamp(puck.transform.localPosition.x,AIBoundary.Left,AIBoundary.Right),startingPosition.y,
									Mathf.Clamp(puck.transform.localPosition.z,AIBoundary.Down,AIBoundary.Up));
			}else{
				// After hitting the puck, go back to starting position
				movementSpeed = maxMovementSpeedinAR * 0.3f;
				targetPosition = startingPosition;
			}
		}
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition,  movementSpeed * Time.fixedDeltaTime);
	}

	struct Boundary{
		public float Up,Down,Left,Right;

		public Boundary(float up, float down, float left, float right){
			Up = up; Down = down; Left = left; Right = right;
		}
	}
}

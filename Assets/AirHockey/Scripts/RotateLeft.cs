using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeft : MonoBehaviour {

	public bool rotateLeft = false;
	public GameObject table;

	GameController controller;

	// Use this for initialization

	void Start() {
		controller = table.GetComponent<GameController>();
	}
		
	void Update(){
		if(rotateLeft && !controller.fix)
			{
				table.transform.Rotate(new Vector3(0, 10, 0)*Time.deltaTime);
			}
	}
	

	public void OnPress()
    {
		rotateLeft = true;
    }

	public void OnRelease(){
		rotateLeft = false;
	}
}

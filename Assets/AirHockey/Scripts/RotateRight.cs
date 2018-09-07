using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRight : MonoBehaviour {

	public bool rotateRight = false;
	public GameObject table;
	GameController controller;
	// Use this for initialization
	
	void Start() {
		controller = table.GetComponent<GameController>();
	}

	void Update(){
		if(rotateRight && !controller.fix)
			{
				table.transform.Rotate(new Vector3(0, -10, 0)*Time.deltaTime);
			}
	}
	
	// Update is called once per frame

	public void OnPress()
    {
		rotateRight = true;
    }

	public void OnRelease(){
		rotateRight = false;
	}
}

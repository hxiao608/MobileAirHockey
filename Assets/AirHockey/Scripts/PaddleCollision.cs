using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollision : MonoBehaviour {

	// Use this for initialization
	void FixedUpdate() {
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}

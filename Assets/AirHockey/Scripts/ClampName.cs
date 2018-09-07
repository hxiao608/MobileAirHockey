using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampName : MonoBehaviour {

	public Text nameLable;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		Vector3 namepos = Camera.main.WorldToScreenPoint(this.transform.position);
		nameLable.transform.position = namepos;
	}
}

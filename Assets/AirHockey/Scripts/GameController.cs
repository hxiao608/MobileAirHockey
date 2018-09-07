using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	float speed = 0.2f;
	//speed for test;
	float testSpeed = 10.0f;
	Vector3 p1startpos;
	Vector3 p2startpos;
	Vector3 puckposition;

	Rigidbody rb;
	public GameObject SizeUpBtn;
	public GameObject SizeDownBtn;
	public GameObject RotateLeftBtn;
	public GameObject RotateRightBtn;
	public GameObject FixBtn;
	public GameObject RestartBtn;
	public Text positionText;
	public Text ScoreText;

	public Text ScoreRecord;
	public Transform BoundaryHolder;

	public GameObject player1;
	public GameObject player2;
	public GameObject puck;

	public bool fix = false;
	bool gameEnd = false; 
	PlayerDataManagement playerData;
	Boundary playerBoundary;
	struct Boundary{
			public float Up,Down,Left,Right;

			public Boundary(float up, float down, float left, float right){
				Up = up; Down = down; Left = left; Right = right;
			}
	}
	// Use this for initialization
	void Start () {
		rb = player1.GetComponent<Rigidbody>();
		playerData = GameObject.Find("PlayerData").GetComponent<PlayerDataManagement>();
		playerBoundary =  new Boundary(BoundaryHolder.GetChild(0).localPosition.z,
									BoundaryHolder.GetChild(1).localPosition.z,
									BoundaryHolder.GetChild(2).localPosition.x,
									BoundaryHolder.GetChild(3).localPosition.x);
		// Debug.Log ("boundary up:" + playerBoundary.Up+"\nboundary down:" + playerBoundary.Down+"\nboundary left: " + playerBoundary.Left+"\nboundary right: "+ playerBoundary.Right);
		// Debug.Log("x: "+player1.transform.eulerAngles.x+ "y: "+player1.transform.eulerAngles.y+ "z: "+player1.transform.eulerAngles.z);
		positionText.enabled = true;
		SizeUpBtn.SetActive(true);
		SizeDownBtn.SetActive(true);
		RotateLeftBtn.SetActive(true);
		RotateRightBtn.SetActive(true);
		RestartBtn.SetActive(false);
		FixBtn.SetActive(true);
		// alignToPlane(player1);
		// alignToPlane(player2);
		// // alignToPlane(puck);
		// Debug.Log("puck position: "+ puckposition);
		ScoreRecord.enabled = true;
		showScore();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!gameEnd && fix){
			// for speed test
			// if(Input.GetKey(KeyCode.RightArrow)){
			// 	player1.transform.Translate(new Vector3(testSpeed * Time.deltaTime,0,0));
			// }
			// if(Input.GetKey(KeyCode.LeftArrow)){
			// 	player1.transform.Translate(new Vector3(-testSpeed * Time.deltaTime,0,0));
			// }
			// if(Input.GetKey(KeyCode.DownArrow)){
			// 	player1.transform.Translate(new Vector3(0,0,-testSpeed * Time.deltaTime));
			// }
			// if(Input.GetKey(KeyCode.UpArrow)){
			// 	player1.transform.Translate(new Vector3(0,0,testSpeed * Time.deltaTime));
			// }
			player1.transform.Translate(CrossPlatformInputManager.GetAxis("Horizontal")*speed*Time.deltaTime,0,0);
			player1.transform.Translate(0,0,CrossPlatformInputManager.GetAxis("Vertical")*speed*Time.deltaTime);
		}

		Vector3 clampedPosition = player1.transform.localPosition;
		// Debug.Log("clampedPosition: " + clampedPosition);
		clampedPosition.x = Mathf.Clamp(player1.transform.localPosition.x, playerBoundary.Left, playerBoundary.Right);
		clampedPosition.z = Mathf.Clamp(player1.transform.localPosition.z, playerBoundary.Down, playerBoundary.Up);
		player1.transform.localPosition = clampedPosition;
	
		positionText.text = "boundary up:" + playerBoundary.Up+"\nboundary down:" + playerBoundary.Down+"\nbounsdary left: " + playerBoundary.Left+"\nboundary right: "+ playerBoundary.Right+ "\nplayer 1_x: "+ player1.transform.localPosition.x.ToString() +"\nplayer 1_y: "+ player1.transform.localPosition.y.ToString() + "\nplayer 1_z: "+player1.transform.localPosition.z.ToString()+"\npuck position" + puck.transform.localPosition + "\ntable position: " + transform.position.ToString()+"\ntable rotation: "+transform.rotation.ToString();
		}

	public void onSizeUpClick(){
		if(!fix){
			transform.localScale += new Vector3(0.01f,0.01f,0.01f);
			speed += 0.05f;
			player2.GetComponent<AIScript>().maxMovementSpeedinAR += 0.5f;
				puck.GetComponent<CollisionController>().trueForce += 0.1f;
		}
	}

	public void onSizeDownClick(){	
		if(!fix){
			transform.localScale -= new Vector3(0.01f,0.01f,0.01f);
			speed -= 0.05f;
			player2.GetComponent<AIScript>().maxMovementSpeedinAR -= 0.5f;
			puck.GetComponent<CollisionController>().trueForce -= 0.1f;
		}
	}

	public void onFixBtnClick(){
		fix = true;
		puckposition = puck.transform.position;
		p1startpos = player1.transform.position;
		p2startpos = player2.transform.position;
	}

	public void alignToPlane(GameObject ob){
			
			RaycastHit hit;
			if(Physics.Raycast(ob.transform.position,Vector3.down,out hit)){
				Vector3 finalposition = hit.point;
				ob.transform.position = finalposition;
			}
		}

	public void initialPuckPosition(){
		puck.transform.position = puckposition;
		puck.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
		player1.transform.position = p1startpos;
		player2.transform.position = p2startpos;
		Debug.Log("initialized puck position: "+ puck.transform.position);
	}

	public void showScoreText(string winner){
		// determine whether one player win the game
		if(playerData.p1Score == 5){
			ScoreText.enabled = true;
			ScoreText.text = "player 1 win the game!";
			showScore();
			// show restart game button
			RestartBtn.SetActive(true);
			gameEnd = true;
		}else if(playerData.p2Score == 5 ){
			ScoreText.enabled = true;
			showScore();
			ScoreText.text ="player 2 win the game!";
			// show restart game button
			RestartBtn.SetActive(true);
			gameEnd = true;
		}else{
			ScoreText.enabled =true;
			ScoreText.text = winner+" get 1 score!";
			StartCoroutine(WaitAndDisappear(1.0f));
		}
	}
	
	IEnumerator WaitAndDisappear (float waitTime){
		showScore();
		gameEnd = true;
		yield return new WaitForSeconds (waitTime);
		ScoreText.enabled = false;
		gameEnd = false;
	}


	void showScore (){
		ScoreRecord.text = playerData.p1Score + " : " + playerData.p2Score;
	}

	public void onRestartButtonClicked(){
		playerData.initialData();
		fix = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
	}

}

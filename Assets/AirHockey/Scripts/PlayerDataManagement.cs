using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManagement : MonoBehaviour {
	public string playerChoice;
	public int p1Score;
	public int p2Score;

	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	public void onSinglePlayerBtnClicked(){
		playerChoice = "single";
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void onTwoPlayerBtnClicked(){
		playerChoice = "double";
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void p1ScoreAdded(){
		p1Score = p1Score+1;
	}
	public void p2ScoreAdded(){
		p2Score = p2Score+1;
	}

	public void initialData(){
		p1Score = 0;
		p2Score = 0;
	}
}

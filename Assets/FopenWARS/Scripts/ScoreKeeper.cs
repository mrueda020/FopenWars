using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreKeeper : MonoBehaviour {
	public static int score = 0;
	private Text myText;
	//public GameObject Score;
	void Start(){
		myText = GetComponent<Text>();
		Reset ();
	}


	public void Score(int points){
		Debug.Log("Score Points");
		score += points;
		myText.text = "Score: " + score.ToString ();
	}



	public static void Reset(){
		score = 0;
		//myText.text = "Score: " + score.ToString ();
	}


}

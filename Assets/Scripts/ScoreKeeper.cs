using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score=0;
	private Text myText;

	void Start()
	{
		myText = GetComponent<Text> ();
	}

	public void AddScore(int points)
	{
		score += points;
		myText.text = score.ToString ();
	}

	public static int GetScore()
	{
		return score;
	}

	public static void Reset()
	{
		score = 0;
	}
}

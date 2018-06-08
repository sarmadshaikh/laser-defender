using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	public void ShowMessage () {
		gameObject.SetActive (true);
		Invoke ("HideMessage", 2.0f);
	}

	public void HideMessage()
	{
		gameObject.SetActive (false);
	}
}

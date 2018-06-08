using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startClip, gameClip, endClip;
	private AudioSource source;

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			source = GetComponent <AudioSource> ();
			source.clip = startClip;
			source.loop = true;
			source.Play ();
		}
		
	}

	void OnLevelWasLoaded(int level)
	{
		source.Stop ();

		if (level == 0)
			source.clip = startClip;
		else if (level == 1)
			source.clip = gameClip;
		else if (level == 2)
			source.clip = endClip;

		source.Play ();
	}
}

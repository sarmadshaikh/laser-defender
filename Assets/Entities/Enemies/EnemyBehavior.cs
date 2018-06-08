using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public float health = 100f, projectileSpeed;
	public GameObject projectile;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 10;
	//AudioSource audioSource;
	public AudioClip audioClip;

	private ScoreKeeper scoreKeeper;

	void Start()
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		//audioSource = GetComponent <AudioSource> ();
	}

	void Update ()
	{
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability)
			Fire ();
	}

	void Fire ()
	{
		Vector3 offset = new Vector3 (0.0f, -0.75f, 0.0f);
		GameObject laser = Instantiate (projectile, transform.position + offset, Quaternion.identity) as GameObject;
		laser.GetComponent <Rigidbody2D> ().velocity = new Vector2 (0f, -projectileSpeed);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Laser laser = collider.gameObject.GetComponent<Laser> ();
		if (laser) {
			health -= laser.GetDamage ();
			laser.Hit ();
			if (health <= 0) {
				Die ();
			}
			Debug.Log ("Hit by Laser");
		}
	}

	void Die()
	{
		AudioSource.PlayClipAtPoint (audioClip, transform.position);
		Destroy (gameObject);
		scoreKeeper.AddScore (scoreValue);
	}
}

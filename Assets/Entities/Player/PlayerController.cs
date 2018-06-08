using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//private Rigidbody2D rb;
	public float speed;
	public float firingRate;
	public GameObject laserPrefab;
	public float health = 100f;

	private float minX, maxX, minY, maxY;

	// Use this for initialization
	void Start () {
		//rb = this.gameObject.GetComponent <Rigidbody2D> ();
		//rb.velocity = new Vector2 (0.0f, 0.0f);

		//Gettin mininmum and maximum World Space Coordinates from the Camera
		Vector2 min, max;

		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		minX = min.x + 0.5f;
		maxX = max.x - 0.5f;
		minY = min.y + 0.5f;
		maxY = max.y - 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//rb.velocity -= new Vector2(Time.deltaTime * speed, 0.0f);

			// Vector3.left is a unit vector pointing in negative x-direction
			transform.position += Vector3.left * speed * Time.deltaTime; 
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			//rb.velocity += new Vector2(Time.deltaTime * speed, 0.0f);

			// Vector3.right is a unit vector pointing in positive x-direction
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			//rb.velocity += new Vector2(0.0f, Time.deltaTime * speed);

			// Vector3.up is a unit vector pointing in positive y-direction
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			//rb.velocity -= new Vector2(0.0f, Time.deltaTime * speed);

			// Vector3.down is a unit vector pointing in negative y-direction
			transform.position += Vector3.down * speed * Time.deltaTime;
		}

		// Restricting the movement of the spaceship using manually calculated boundaries
		//transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), Mathf.Clamp(transform.position.y, -5.5f, 5.5f));

		// Restricting the movement of the spaceship using values calculated from Camera Viewport
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ();
		}
	}

	public void Fire()
	{
		GameObject laser = Instantiate (laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent <Rigidbody2D> ().velocity = new Vector2 (0f, 30f);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Laser laser = coll.gameObject.GetComponent <Laser> ();
		health -= laser.GetDamage ();
		if (health <= 0) {
			Destroy (gameObject);
			LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
			levelManager.LoadLevel ("Win Screen");
			/*
			GameOver gameover = FindObjectOfType<GameOver> ();
			gameover.ShowMessage ();
			*/
		}
		laser.Hit ();
	}
}

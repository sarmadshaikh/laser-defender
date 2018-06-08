using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	private bool movingRight = false;
	public float speed = 5f;

	private float xmax, xmin;

	// Use this for initialization
	void Start () {

		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector2 (1, 0));
		xmax = rightEdge.x;
		xmin = leftEdge.x;
		SpawnUntilFull ();
	}

	void SpawnEnemies()
	{
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;		
		}
	}

	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition ();
		if (freePosition != null) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
			Invoke ("SpawnUntilFull", 0.5f);
		}
	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector2 (width, height));
	}

	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (width / 2);
		float leftEdgeOfFormation = transform.position.x - (width / 2);
		if (rightEdgeOfFormation >= xmax) {
			movingRight = false;
		} else if (leftEdgeOfFormation <= xmin) {
			movingRight = true;
		}

		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}

	bool AllMembersDead ()
	{
		foreach (Transform childObjectTransformPosition in transform){
			if (childObjectTransformPosition.childCount > 0)
				return false;
		}
		return true;
	}

	Transform NextFreePosition()
	{
		foreach (Transform childObjectTransformPosition in transform) {
			if (childObjectTransformPosition.childCount == 0)
				return childObjectTransformPosition;
		}

		return null;
	}
}
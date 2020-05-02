using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemiesspawn : MonoBehaviour {
	public GameObject enemyprefab;
	public float width = 10f;
	public float height = 5f;
	private bool movingRight = true;
	public float velocidad=1;
	private float xmax, xmin;
	public float spawndelay = 0.5f; 
	// Use this for initialization

	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge= Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightEdge= Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xmax = rightEdge.x;
		xmin = leftEdge.x;
		SpawnFULL();
		//transform.Rotate (90, 0, 0);
	}


		
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			//transform.Rotate (90, 0, 0);
			transform.position += Vector3.right * velocidad * Time.deltaTime;
		} else {
			//transform.Rotate (90, 0, 0);
			transform.position += Vector3.left * velocidad * Time.deltaTime;
		}

		float rightformation = transform.position.x + (0.45f * width);
		float leftformation = transform.position.x - (0.45f * width);
		if (leftformation < xmin) {
			movingRight = true;
		} else if (rightformation > xmax) {
			movingRight = false;
		}

		if (AllMembersDead()) {
			SpawnFULL();

		}

	}


	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
	}


	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount>0){
				return false;
			}
		}
		return true;
	}


	void SpawnEnemies(){
		foreach (Transform child in transform) {
		GameObject enemy = Instantiate (enemyprefab, child.transform.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = child;
		}	
			
	}
		

	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
	
		return null;
	
	}


	void SpawnFULL(){
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyprefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
		Invoke ("SpawnFULL", spawndelay);
	}
	
}

}
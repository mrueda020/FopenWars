using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ENEMYBEHAVIOUR : MonoBehaviour {
	public float vida=1000f;
	public GameObject LASER1;
	public float projectilespeed = 10;
	public float Disparosporsegundo = 0.5f;
	public int scoreValue = 50;
	public AudioClip firesound;
	public AudioClip deadsound;
    private ScoreKeeper scoreKeeper;
	public GameObject explosion;

	void Start () {
		
		scoreKeeper = GameObject.Find("score").GetComponent<ScoreKeeper>();	
	}



	void Update(){
		float probabilidad = Time.deltaTime * Disparosporsegundo;
		if (Random.value < probabilidad) {
			FIRE ();
		}
	}

	void FIRE(){
		Vector3 starpositon = transform.position + new Vector3 (0, -1.5f, 0);
		GameObject missile1 = Instantiate(LASER1, starpositon, Quaternion.identity) as GameObject;
		missile1.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectilespeed,0);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
	}


	void OnTriggerEnter2D(Collider2D collider){
		LASER beam = collider.gameObject.GetComponent<LASER>();
		if (beam) {
			vida -= beam.GetDamage();
			beam.Hit();
			if (vida <= 0) {
				AudioSource.PlayClipAtPoint (deadsound, transform.position);
				Instantiate(explosion, transform.position, transform.rotation);
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
			}
		}
	}




}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;
	public AudioClip start;
	public AudioClip game;
	public AudioClip end;
	private AudioSource music;


	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = start;
			music.loop = true;
			music.Play();

		}
		
	}

	void OnLevelWasLoaded(int level){
		Debug.Log ("Music player loaded level " + level);
		 //music.Stop();
		if (level == 0) {
			music.clip = start;
		} else if (level == 1) {
			music.clip = game;

		} else if (level == 2) {
			music.clip = end;
		} 
		music.loop = true;
		music.Play ();
	}


}

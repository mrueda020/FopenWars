using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PlayerController : MonoBehaviour {
	public float velocidad = 1;
	float xmin,ymin;
	float xmax,ymax;
	public float lim =1;
	public GameObject laser;
	public float direccion = 1;
	public float firingrate=15;
	public float vida = 1000f;
	public AudioClip firesound;
	public AudioClip dead;
	public AudioClip shield;
	public Text healtext;
	public GameObject explosion;
	private Quaternion calibrationQuaternion;
	public simpletouch touchpad;
	public SimpleTouchArea areaButton;
    
    private float nextFire;
   
    void Start () {
		CalibrateAccelerometer();
        float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin = leftmost.x + lim;
		xmax = rightmost.x - lim;
		ymin = -5f;
		ymax = -4f;
		healtext.text= "HEALTH: 650";
	}


	void Update () {
	    if (areaButton.CanFire()&& Time.time > nextFire) {
                nextFire = Time.time + firingrate;
                InvokeRepeating ("Fire", 0.001f, firingrate);
		} 
	    if(!areaButton.CanFire()) {
			    CancelInvoke ("Fire");
		}
	}

	void CalibrateAccelerometer(){
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}


	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}



		
	void Fire(){
		Vector3 position = transform.position + new Vector3 (0, 1f, 0);
		GameObject Laser= Instantiate (laser, position, Quaternion.identity) as GameObject;
		Laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, 10, 0);
		AudioSource.PlayClipAtPoint (firesound, transform.position);
	}

	

	void OnTriggerEnter2D(Collider2D collider){
		LASER beam = collider.gameObject.GetComponent<LASER>();
        LevelManager main = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (beam) {
			vida -= beam.GetDamage();
			beam.Hit ();
			AudioSource.PlayClipAtPoint (shield, transform.position);
			healtext.text = "HEALTH: " + vida.ToString ();

			if (vida <= 0 ) {
       
                Instantiate(explosion, transform.position, transform.rotation);
				Destroy (gameObject);
				AudioSource.PlayClipAtPoint(dead, transform.position);
                //StartCoroutine(Delay());  
                main.LoadLevel("Menu 3D");
            }
		}
	}

   IEnumerator Delay(){

        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
    }


    void FixedUpdate (){
		Vector2 direction = touchpad.GetDirection();
		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
		GetComponent<Rigidbody2D> ().velocity = movement * velocidad;
		float newX = Mathf.Clamp(transform.position.x, xmin,xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		float newY = Mathf.Clamp(transform.position.y, ymin,ymax);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
	}
} 


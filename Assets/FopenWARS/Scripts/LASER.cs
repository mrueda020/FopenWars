using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASER : MonoBehaviour {

	public float daño = 15f;

	public float GetDamage(){

		return daño;

	}





	public void Hit(){

		Destroy(gameObject);
	}

}

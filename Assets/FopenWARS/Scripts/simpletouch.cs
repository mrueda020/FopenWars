using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class simpletouch : MonoBehaviour, IPointerDownHandler,IDragHandler, IPointerUpHandler {
	public float smoothing;
	private Vector2 smoothdirection;
	private Vector2 origin;
	private Vector2 direction;
	private bool touched;
	private int pointerID;

	void Awake(){
		direction = Vector2.zero;
		touched = false;
	}


	public void OnPointerDown(PointerEventData data){
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}
	}

	public void OnDrag(PointerEventData data){
		if(data.pointerId==pointerID){
		Vector2 currentposition = data.position;
		Vector2 directionraw = currentposition - origin;
		direction = directionraw.normalized;
		//Debug.Log (direction);
		}
	}


	public void OnPointerUp(PointerEventData data){
		if (data.pointerId == pointerID) {
			direction = Vector3.zero;
			touched = false;
		}
	}


	public Vector2 GetDirection(){
		smoothdirection = Vector2.MoveTowards (smoothdirection, direction, smoothing);
		return smoothdirection;
	}


}

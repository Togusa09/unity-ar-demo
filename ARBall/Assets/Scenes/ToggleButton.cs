using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler{

	public bool IsDown { get; private set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		IsDown = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		IsDown = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

	private GameObject currentPlane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Plane") {
			currentPlane = collision.gameObject;
		}
	}

//	void OnTriggerEnter(Collider other) {
//		if (other.gameObject.name == "Plane") {
//			currentPlane = other.gameObject;
//		}
//	}


	public void GoForward()
	{
		var oldPosition = transform.position;
		transform.position += transform.forward * Time.deltaTime * 1f;

		if (currentPlane) {
			var rend = currentPlane.GetComponent<Renderer>();
			var testBound = new Bounds (rend.bounds.center, rend.bounds.size * 0.6f);
			var pos = transform.position;
			pos.y = rend.transform.position.y;
			if (!testBound.Contains (pos)) {
				transform.position = oldPosition;
			}
		}
	}

	public void GoBack()
	{
		var oldPosition = transform.position;
		transform.position -= transform.forward * Time.deltaTime * 1f;

		if (currentPlane) {
			var rend = currentPlane.GetComponent<Renderer>();
			var testBound = new Bounds (rend.bounds.center, rend.bounds.size * 0.6f);
			var pos = transform.position;
			pos.y = rend.transform.position.y;
			if (!testBound.Contains (pos)) {
				transform.position = oldPosition;
			}
		}
	}
	public void GoLeft()
	{
		transform.Rotate (new Vector3 (0, -1));
	}
	public void GoRight()
	{
		transform.Rotate (new Vector3 (0, 1));
	}
}

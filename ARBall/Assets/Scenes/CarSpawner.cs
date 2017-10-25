using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using System.Linq;
using UnityEngine.UI;

public class CarSpawner : HitTestListener {

	public ToggleButton forward;
	public ToggleButton back;
	public ToggleButton left;
	public ToggleButton right;
	public Button reset;

	public GameObject car;

	public GameObject carPrefab;
	public float createHeight;
	private MaterialPropertyBlock props;

	// Use this for initialization
	void Start () {
		props = new MaterialPropertyBlock ();
		reset.onClick.AddListener (Reset);
	}
	private bool IsCarSpawned;

	void Reset(){
		if (car) {
			Object.Destroy (car);
		}
		car = null;
		IsCarSpawned = false;
	}


	public override void OnHitTest (Vector3 position, Quaternion rotation)
	{
		if (!IsCarSpawned) {
			CreateCar (new Vector3 (position.x, position.y + createHeight, position.z));
		}
	}
		

	void CreateCar(Vector3 atPosition)
	{
		GameObject carGameObject = Instantiate (carPrefab, atPosition, Quaternion.identity);
		car = carGameObject;
		IsCarSpawned = true;

		float r = Random.Range(0.0f, 1.0f);
		float g = Random.Range(0.0f, 1.0f);
		float b = Random.Range(0.0f, 1.0f);

		props.SetColor("_InstanceColor", new Color(r, g, b));

		MeshRenderer renderer = carGameObject.GetComponent<MeshRenderer>();
		renderer.SetPropertyBlock(props);
	}

	// Update is called once per frame
	void Update () {

		if (car) {
			Debug.Log ("Car Position " + car.transform.position);
			forward.enabled = true;
			back.enabled = true;
			left.enabled = true;
			right.enabled = true;
		} else {
			forward.enabled = false;
			back.enabled = false;
			left.enabled = false;
			right.enabled = false;
		}

		if (car && car.transform.position.y < -100f) {
			Reset ();
		}
	}

	void FixedUpdate()
	{
		if (!car)
			return;

		var carScript = car.GetComponent<CarScript> ();

		if (forward.IsDown) {
			carScript.GoForward ();
		}
		if (back.IsDown) {
			carScript.GoBack ();
		}
		if (left.IsDown) {
			carScript.GoLeft ();
		}
		if (right.IsDown) {
			carScript.GoRight ();
		}
	}



}

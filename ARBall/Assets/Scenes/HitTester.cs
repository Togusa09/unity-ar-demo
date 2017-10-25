﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;


public class HitTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public HitTestListener hitTestListener;
	
	#if UNITY_EDITOR   //we will only use this script on the editor side, though there is nothing that would prevent it from working on device
	public float maxRayDistance = 30.0f;
	public LayerMask collisionLayerMask;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			//we'll try to hit one of the plane collider gameobjects that were generated by the plugin
			//effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
			if (Physics.Raycast (ray, out hit, maxRayDistance, collisionLayerMask)) {
				//we're going to get the position from the contact point
;
				transform.position = hit.point;
				Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", transform.position.x, transform.position.y, transform.position.z));

				//and the rotation from the transform of the plane collider
				transform.rotation = hit.transform.rotation;
				hitTestListener.OnHitTest (hit.point, hit.transform.rotation);
			}
		}
	}
	#else
	bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
	{
		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
		if (hitResults.Count > 0) {
			foreach (var hitResult in hitResults) {
				Debug.Log ("Got hit!");
				var pos = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
				var rot = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
				Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", pos.x, pos.y, pos.z));
				hitTestListener.OnHitTest (pos,rot);
				return true;
			}
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
			{
				var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};

				// prioritize reults types
				ARHitTestResultType[] resultTypes = {
					ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
					// if you want to use infinite planes use this:
					//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
					ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
					ARHitTestResultType.ARHitTestResultTypeFeaturePoint
				}; 

				foreach (ARHitTestResultType resultType in resultTypes)
				{
					if (HitTestWithResultType (point, resultType))
					{
						return;
					}
				}
			}
		}
	}

	#endif
}

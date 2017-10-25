using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public abstract class HitTestListener : MonoBehaviour{
	public abstract void OnHitTest (Vector3 position, Quaternion rotation);
}

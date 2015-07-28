using UnityEngine;
using System.Collections;

public class PCam : MonoBehaviour {
	public Vector3 pos2Follow;
	public Transform target;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	void FixedUpdate() {
		if (pos2Follow != null) {
			Vector3 targetPosition = target.TransformPoint (pos2Follow);
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);
		}
	}
}

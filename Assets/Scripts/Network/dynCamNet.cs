using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class dnyCamNet : NetworkBehaviour
{
	public Transform centerPoint;
	public List<GameObject> objects2Track = new List<GameObject> ();
	public float zoomSmooth = 0.5f;
	public float minZoom = 5f;
	public float maxZoom = 50f;
	private float size = 0;
	private Camera myCam;

	// Use this for initialization
	void Start ()
	{
		if (!isServer) {
			return;
		}
		objects2Track.AddRange (GameObject.FindGameObjectsWithTag ("Enemy"));
		objects2Track.AddRange (GameObject.FindGameObjectsWithTag ("Player"));
		myCam = gameObject.GetComponent<Camera> ();
	}
	void Zooming () {
		myCam.orthographicSize = Mathf.Lerp (myCam.orthographicSize, size, zoomSmooth);
		if (myCam.orthographicSize < minZoom)
			myCam.orthographicSize = minZoom;
		if (myCam.orthographicSize > maxZoom)
			myCam.orthographicSize = maxZoom;
		if(objects2Track.Count < 2)
			myCam.orthographicSize = minZoom;
	}
	Vector3 FindCenterPoint ()
	{
		if (objects2Track.Count == 0)
			return Vector3.zero;
		if (objects2Track.Count == 1)
			return objects2Track [0].transform.position;
		Bounds bounds = new Bounds (objects2Track [0].transform.position, Vector3.zero);
		for (var i = 1; i < objects2Track.Count; i++)
			bounds.Encapsulate (objects2Track [i].transform.position); 
		size = ((bounds.size.x + bounds.size.z) / 2);
		return bounds.center;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isServer) {
			return;
		}
		myCam.orthographicSize = Mathf.Lerp (myCam.orthographicSize, size, zoomSmooth);
		if (myCam.orthographicSize < minZoom)
			myCam.orthographicSize = minZoom;
		if (myCam.orthographicSize > maxZoom)
			myCam.orthographicSize = maxZoom;
		if(objects2Track.Count < 2)
			size = minZoom;
		for (int i = 0; i < objects2Track.Count; i++) {
			if (objects2Track [i] == null)
				objects2Track.RemoveAt (i);
		}
		centerPoint.position = FindCenterPoint ();
	}
}


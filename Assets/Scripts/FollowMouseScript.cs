using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseScript : MonoBehaviour {

    private float m_positionLerpAlpha = 0.25f;

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			Destroy(this);
		}
		
		Vector3 mousePosition = Input.mousePosition;
        Camera camera = Camera.main;
        float zShiftCloserToCamera = -1.0f;
        float zPosition = -camera.transform.position.z + zShiftCloserToCamera;
        Vector3 targetPosition = camera.ScreenToWorldPoint (new Vector3 (mousePosition.x, mousePosition.y, zPosition));
        Vector3 currentPosition = transform.position;
        transform.position = Vector3.Lerp (currentPosition, targetPosition, m_positionLerpAlpha);
	}
}

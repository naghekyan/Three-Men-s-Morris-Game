using UnityEngine;

public class FollowMouseScript : MonoBehaviour {
    private readonly float m_positionLerpAlpha = 0.25f;

    private void Start() {
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) Destroy(this);

        var mousePosition = Input.mousePosition;
        var camera = Camera.main;
        var zShiftCloserToCamera = -1.0f;
        var zPosition = -camera.transform.position.z + zShiftCloserToCamera;
        var targetPosition = camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zPosition));
        var currentPosition = transform.position;
        transform.position = Vector3.Lerp(currentPosition, targetPosition, m_positionLerpAlpha);
    }
}
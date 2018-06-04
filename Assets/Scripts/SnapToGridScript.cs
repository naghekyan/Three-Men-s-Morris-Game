using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGridScript : MonoBehaviour {
	
    private float m_snapRange = 1.7f;
    private float m_positionLerpAlpha = 0.35f;
	private bool m_isDropped = false;
	private float m_targetReachingThreshold = 0.01f;
	private GridPositionProvider m_gridPositionProvider;

	void Start () {
		GameObject[] listOfHoles = GameObject.FindGameObjectsWithTag("Hole");
        m_gridPositionProvider = new GridPositionProvider (listOfHoles);
	}

	void Update () {
        SnapToValidPosition();
		SnapToGridOnDrop();
	}

    private void SnapToValidPosition() {
        Vector3 position = transform.position;

        if (m_gridPositionProvider.IsValidPositionInRangeExists (position, m_snapRange)) {
            GridPlaceholder gridPlacholder = m_gridPositionProvider.GetGridPlacholderInRange (position, m_snapRange);
            Vector3 targetPosition = gridPlacholder.GetWorldPosition();
            transform.position = Vector3.Lerp (position, targetPosition, m_positionLerpAlpha);

            if (IsDroppedAndRichedToGridPosition(position, targetPosition)) {
                Destroy (this);
            }
        }
    }

	private bool IsDroppedAndRichedToGridPosition(Vector3 position, Vector3 targetPosition) {
		return m_isDropped && Vector3.Distance (position, targetPosition) < m_targetReachingThreshold;
	}

	void SnapToGridOnDrop()
	{
		if (Input.GetMouseButtonUp (0)) {
            m_isDropped = true;
			m_positionLerpAlpha = 2.0f;
        }
	}
}

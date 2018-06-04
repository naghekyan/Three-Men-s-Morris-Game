using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour 
{
    public float m_snapRange = 1.0f;

    enum BallColor {BlueBall, WhiteBall};

    private GameObject m_grabbedBall;
    private BallColor m_colorToMove;
    private float m_positionLerpAlpha = 0.25f;
    private GridPositionProvider m_gridPositionProvider;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.1F;

	void Start () 
    {
        m_colorToMove = BallColor.BlueBall;
        initializeValidPositions ();
	}

    private void initializeValidPositions()
    {
        GameObject[] listOfHoles = GameObject.FindGameObjectsWithTag("Hole");
        m_gridPositionProvider = new GridPositionProvider (listOfHoles);
    }
	
	void Update () 
    {
        if (Input.GetMouseButtonDown (0)) {
            tryToGrabBall ();
        } else if (Input.GetMouseButtonUp (0)) {
            if (m_grabbedBall != null) {
                StartCoroutine(SnapToValidPosition());
            }
        } else if (Input.GetMouseButton (0)) {
            moveGrabbedBall ();
        }
	}

    IEnumerator SnapToValidPosition() {
       while (true) {
            Vector3 grabbedBallPosition = m_grabbedBall.transform.position;

            if (m_gridPositionProvider.IsValidPositionInRangeExists (grabbedBallPosition, m_snapRange)) {
                GridPlaceholder gridPlacholder = m_gridPositionProvider.GetGridPlacholderInRange (grabbedBallPosition, m_snapRange);
                Vector3 targetPosition = gridPlacholder.GetWorldPosition();
                targetPosition.z = targetPosition.z - 0.2f;
                m_grabbedBall.transform.position = Vector3.SmoothDamp (grabbedBallPosition, targetPosition, ref velocity, smoothTime);

                if (Vector3.Distance (grabbedBallPosition, targetPosition) > 0.01f) {
                    yield return null;
                } else {
                    MakeMove(gridPlacholder);
                    break;
                }
            } else {
                break;
            }
        }

        ungrabBall ();
    }

    private void MakeMove(GridPlaceholder gridPlacholder)
    {
        Debug.Log("Indexes = " + gridPlacholder.GetRow().ToString() + " : " + gridPlacholder.GetColumn().ToString());
    }

    private void tryToGrabBall()
    {
        RaycastHit hitInfo = new RaycastHit ();
        bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
        if (hit) {
            GameObject hitGameObject = hitInfo.transform.gameObject;

            if (hitGameObject.tag == m_colorToMove.ToString ()) {
                m_grabbedBall = hitGameObject;
            }
        } 
    }

    private void ungrabBall() {
        m_grabbedBall = null;
    }

    private void moveGrabbedBall() {
        if (m_grabbedBall == null) {
            return;
        }
            
        moveToMouse ();
        snapToValidPositions ();
    }

    private void moveToMouse() {
        Vector3 mousePosition = Input.mousePosition;
        Camera camera = Camera.main;
        float zShiftCloserToCamera = -1.0f;
        float zPosition = -camera.transform.position.z + zShiftCloserToCamera;
        Vector3 targetPosition = camera.ScreenToWorldPoint (new Vector3 (mousePosition.x, mousePosition.y, zPosition));
        Vector3 currentPosition = m_grabbedBall.transform.position;
        m_grabbedBall.transform.position = Vector3.Lerp (currentPosition, targetPosition, m_positionLerpAlpha);
    }

    private void snapToValidPositions() {
        Vector3 grabbedBallPosition = m_grabbedBall.transform.position;

        if (m_gridPositionProvider.IsValidPositionInRangeExists (grabbedBallPosition, m_snapRange)) {
            GridPlaceholder gridPlacholder = m_gridPositionProvider.GetGridPlacholderInRange (grabbedBallPosition, m_snapRange);
            Vector3 targetPosition = gridPlacholder.GetWorldPosition();
            float strongLerpFactor = 1.5f * m_positionLerpAlpha;
            m_grabbedBall.transform.position = Vector3.Lerp (grabbedBallPosition, targetPosition, strongLerpFactor);
        }
    }
}

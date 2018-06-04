using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour 
{
    enum BallColor {BlueBall, WhiteBall};

    private GameObject m_grabbedBall;
    private BallColor m_colorToMove;

	void Start () 
    {
        m_colorToMove = BallColor.BlueBall;
	}

	void Update () 
    {
        if (Input.GetMouseButtonDown (0)) {
            TryToGrabBall ();
        } else if (Input.GetMouseButtonUp (0)) {
            if (m_grabbedBall != null) {
               UngrabBall ();
            }
        } 
	}

    private void TryToGrabBall()
    {
        RaycastHit hitInfo = new RaycastHit ();
        bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
        if (hit) {
            GameObject hitGameObject = hitInfo.transform.gameObject;

            if (hitGameObject.tag == m_colorToMove.ToString ()) {
                m_grabbedBall = hitGameObject;
                m_grabbedBall.AddComponent<FollowMouseScript>();
                m_grabbedBall.AddComponent<SnapToGridScript>();
            }
        } 
    }

    private void UngrabBall() {
        m_grabbedBall = null;
    }
}

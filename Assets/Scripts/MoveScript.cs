using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour 
{
    private GameObject m_grabbedBall = null;
    private Vector3 m_grabbedBallInitialPosition;
    private BoardCellColor m_colorToMove = BoardCellColor.Blue;
    private GridPositionProvider m_gridPositionProvider;
    private WinDetector m_winDetector = null;

    public GridPositionProvider GetGridPositionProvider() {
        return m_gridPositionProvider;
    }

	void Start () {
        var configScript = GetComponent<ConfigScript>();
        InitGridPositionProvider(configScript);
        InitWinDetector(configScript);
    }

    private void InitGridPositionProvider(ConfigScript configScript) {
        GameObject[] listOfHoles = GameObject.FindGameObjectsWithTag("Hole");
        float snapRange = configScript.GetSnapingRange();
        m_gridPositionProvider = new GridPositionProvider(listOfHoles, snapRange);
    }

    private void InitWinDetector(ConfigScript configScript) {
        int boardSize = configScript.GetBoardSize();
        m_winDetector = new WinDetector(boardSize);
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            TryToGrabBall ();
        } else if (Input.GetMouseButtonUp (0)) {
            if (m_grabbedBall != null) {
               MoveBall();
            }
        } 
	}

    private void TryToGrabBall() {
        RaycastHit hitInfo = new RaycastHit ();
        bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
        if (hit) {
            GameObject hitGameObject = hitInfo.transform.gameObject;

            if (hitGameObject.tag == m_colorToMove.ToString ()) {
                BackupBallData(hitGameObject);
                SetupBallToFollowMouse();
            }
        } 
    }

    private void SetupBallToFollowMouse() {
        m_grabbedBall.AddComponent<FollowMouseScript>();
        m_grabbedBall.AddComponent<SnapToGridScript>();
    }

    private void BackupBallData(GameObject hitGameObject) {
        m_grabbedBall = hitGameObject;
        m_grabbedBallInitialPosition = hitGameObject.transform.position;
    }

    private void MoveBall() {
        Vector3 position = m_grabbedBall.transform.position;
        if (m_gridPositionProvider.IsValidPositionInRangeExists(position))
        {
            GridCoordinate newCoordinates = GetCoordinatesByPosition(position);

            var previousCoordinates = m_grabbedBall.GetComponent<CoordinatesOnGridScript>();
            MoveData move = new MoveData();
            move.startCoordinate = previousCoordinates.GetBoardCoordinates();
            move.endCoordinate = new BoardCoordinate(true, newCoordinates);

            if (move.IsChangingLocation()) {
                m_winDetector.DoMove(move, m_colorToMove);
                m_grabbedBall.GetComponent<CoordinatesOnGridScript>().SetGridCoordinates(newCoordinates);
                m_colorToMove = m_colorToMove == BoardCellColor.Blue ? BoardCellColor.White : BoardCellColor.Blue;
            }
        }

        UngrabBall ();
    }

    private GridCoordinate GetCoordinatesByPosition(Vector3 position) {
        GridPlaceholder placholder = m_gridPositionProvider.GetGridPlacholderInRange(position);
        GridCoordinate newCoordinates = placholder.GetGridCoordinates();
        return newCoordinates;
    }

    private void UngrabBall() {
        m_grabbedBall = null;
    }
}

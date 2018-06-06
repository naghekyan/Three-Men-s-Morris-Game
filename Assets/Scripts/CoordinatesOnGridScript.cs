using System;
using UnityEngine;

public class CoordinatesOnGridScript : MonoBehaviour {
    private readonly BoardCoordinate m_coordinates = new BoardCoordinate(false, new GridCoordinate(-1, -1));

    public void SetGridCoordinates(GridCoordinate coordinates) {
        m_coordinates.m_isOnGrid = true;
        m_coordinates.m_gridCoordinate = coordinates;
    }

    public BoardCoordinate GetBoardCoordinates() {
        return m_coordinates;
    }
}
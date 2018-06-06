using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinatesOnGridScript : MonoBehaviour {
	private BoardCoordinate m_coordinates = new BoardCoordinate(false, new GridCoordinate(-1, -1));

	public bool IsOnGrid() {
		return m_coordinates.m_isOnGrid;
	}
	
    public void SetGridCoordinates(GridCoordinate coordinates) {
		m_coordinates.m_isOnGrid = true;
        m_coordinates.m_gridCoordinate = coordinates;
    }

	public GridCoordinate GetGridCoordinates() {
		if (m_coordinates.m_isOnGrid) {
			return m_coordinates.m_gridCoordinate;
		} else {
			throw new Exception("The item is not on grid yet.");
		}
	}

	public BoardCoordinate GetBoardCoordinates() {
		return m_coordinates;
	}
}

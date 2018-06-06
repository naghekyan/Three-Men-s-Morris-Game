using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCoordinate {
	public int m_row;
	public int m_column;

	public GridCoordinate(int row = 0, int column = 0) {
		m_row = row;
		m_column = column;
	}
}


public class BoardCoordinate {
	public bool m_isOnGrid = false;
	public GridCoordinate m_gridCoordinate = new GridCoordinate(-1, -1);

	public BoardCoordinate(bool isOnGrid, GridCoordinate gridCoordinate) {
		m_isOnGrid = isOnGrid;
		m_gridCoordinate = gridCoordinate;
	}
}

public class MoveData {
	public BoardCoordinate startCoordinate = new BoardCoordinate(false, new GridCoordinate(-1, -1));
	public BoardCoordinate endCoordinate = new BoardCoordinate(false, new GridCoordinate(-1, -1));
	public BoardCellColor color = BoardCellColor.Empty;

	public void SetupFirstMoveToGrid(GridCoordinate gridCoordinate) {
		startCoordinate.m_isOnGrid = false;
		endCoordinate.m_isOnGrid = true;
		endCoordinate.m_gridCoordinate = gridCoordinate;
	}

	public bool IsChangingLocation() {
		if (!startCoordinate.m_isOnGrid) {
			return true;
		} else {
			var start = startCoordinate.m_gridCoordinate;
			var end = endCoordinate.m_gridCoordinate;
			return start.m_row != end.m_row || start.m_column != end.m_column;
		}
	}
}

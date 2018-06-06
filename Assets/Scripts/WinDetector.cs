using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WinDetector {

	private int m_boardSize = 3;
	private List<List<BoardCellColor>> m_board = new List<List<BoardCellColor>>();

	public WinDetector (int boardSize) {
		m_boardSize = boardSize;
		ResetBoard();
	}

	void ResetBoard() {
		m_board.Clear();
		for (int i = 0; i < m_boardSize; ++i)
		{
			List<BoardCellColor> row = new List<BoardCellColor>();
			m_board.Add(row);
			for (int j = 0; j < m_boardSize; ++j)
			{
				row.Add(BoardCellColor.Empty);
			}
		}
	}

	private bool IsValidMove(GridCoordinate coordinates) {
		bool isOnBoard = IsOnGrid(coordinates);
		bool isFieldEmpty = GetFieldColor(coordinates) == BoardCellColor.Empty;
		return isOnBoard && isFieldEmpty;
	}

    private bool IsOnGrid(GridCoordinate coordinates) {
        bool isInRowRange = coordinates.m_row >= 0 && coordinates.m_row < m_boardSize;
		bool isInColumnRange = coordinates.m_column >= 0 && coordinates.m_column < m_boardSize;
		return isInRowRange && isInColumnRange;
    }

    private BoardCellColor GetFieldColor(GridCoordinate coordinates) {
		return m_board[coordinates.m_row][coordinates.m_column];
	}

	private BoardCellColor SetFieldColor(GridCoordinate coordinates, BoardCellColor color) {
		return m_board[coordinates.m_row][coordinates.m_column] = color;
	}

	public void DoMove(MoveData move, BoardCellColor color) {
		if (!move.startCoordinate.m_isOnGrid) {
			var newCoordinates = move.endCoordinate.m_gridCoordinate;
			Assert.IsTrue(move.endCoordinate.m_isOnGrid, "Shoudl be on the Grid.");
			Assert.IsTrue(IsValidMove(newCoordinates), "Move is not valid.");
			SetFieldColor(newCoordinates, color);
		} else {
			var startGridCoordinate = move.startCoordinate.m_gridCoordinate;
			var endGridCoordinate = move.endCoordinate.m_gridCoordinate;
			Assert.IsTrue(IsColorExistInCoordinate(move.startCoordinate, color), "Previous coordinate is not valid.");
			SetFieldColor(startGridCoordinate, BoardCellColor.Empty);
			SetFieldColor(endGridCoordinate, color);
		}

		Debug.Log("Is move resulted to WIN: " + IsWinDetected(color).ToString());
	}

    private bool IsColorExistInCoordinate(BoardCoordinate boardCoordinate, BoardCellColor color) {
			GridCoordinate gridCoordinate = boardCoordinate.m_gridCoordinate;
			if (!boardCoordinate.m_isOnGrid) {
				return true;
			} else {
				bool isOnGrid = IsOnGrid(gridCoordinate);
				bool isColorCorrect = m_board[gridCoordinate.m_row][gridCoordinate.m_column] == color;
				return isOnGrid && isColorCorrect;
			}
    }

    public bool IsWinDetected(BoardCellColor playerColor) {
		return IsWinByRow(playerColor) || IsWinByColumn(playerColor) || 
			IsWinByMainDiagonal(playerColor) || IsWinBySecondaryDiagonal(playerColor);
	}

	private bool IsWinByRow(BoardCellColor playerColor) {
		for (int row = 0; row < m_boardSize; ++row) {
			List<GridCoordinate> path = new List<GridCoordinate>();			
			for (int col = 0; col < m_boardSize; ++col) {
				path.Add(new GridCoordinate(row, col));
			}

			if (IsPathWinning(path, playerColor)) {
				return true;
			}
		}
		return false;
	}

	private bool IsPathWinning(List<GridCoordinate> path, BoardCellColor playerColor) {
		int matchCounter = 0;
		foreach (var cell in path) {
			if (playerColor == m_board[cell.m_row][cell.m_column]) {
				++matchCounter;
			}
		}

		return matchCounter == m_boardSize;
	}

	private bool IsWinByColumn(BoardCellColor playerColor) {

		for (int col = 0; col < m_boardSize; ++col) {
			List<GridCoordinate> path = new List<GridCoordinate>();
			for (int row = 0; row < m_boardSize; ++row) {
				path.Add(new GridCoordinate(row, col));
			}

			if (IsPathWinning(path, playerColor)) {
				return true;
			}
		}
		return false;
	}

	private bool IsWinByMainDiagonal(BoardCellColor playerColor) {
		List<GridCoordinate> path = new List<GridCoordinate>();
		for (int i = 0; i < m_boardSize; ++i) {
			path.Add(new GridCoordinate(i, i));
		}

		return IsPathWinning(path, playerColor);
	}

	private bool IsWinBySecondaryDiagonal(BoardCellColor playerColor) {
		List<GridCoordinate> path = new List<GridCoordinate>();
		for (int i = 0; i < m_boardSize; ++i) {
			int row = i;
			int col = m_boardSize - i - 1;
			path.Add(new GridCoordinate(row, col));
		}

		return IsPathWinning(path, playerColor);
	}
}

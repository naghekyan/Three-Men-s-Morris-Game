using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WinDetectionScript : MonoBehaviour {

	private int m_boardSize = 3;
	private List<List<BoardCellColor>> m_board = new List<List<BoardCellColor>>();

	void Start () {
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

	public bool IsValidMove(GridCoordinate coordinates)
	{
		bool isInRowRange = coordinates.row >= 0 && coordinates.row < m_boardSize;
		bool isInColumnRange = coordinates.column >= 0 && coordinates.column < m_boardSize;
		bool isFieldEmpty = GetFieldColor(coordinates) == BoardCellColor.Empty;
		return isInRowRange && isInColumnRange && isFieldEmpty;
	}

	private BoardCellColor GetFieldColor(GridCoordinate coordinates) {
		return m_board[coordinates.row][coordinates.column];
	}

	private BoardCellColor SetFieldColor(GridCoordinate coordinates, BoardCellColor color) {
		return m_board[coordinates.row][coordinates.column] = color;
	}

	public void DoMove(GridCoordinate coordinates, BoardCellColor color)
	{
		Assert.IsTrue(IsValidMove(coordinates), "Move is not valid.");
		SetFieldColor(coordinates, color);
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
			if (playerColor == m_board[cell.row][cell.column]) {
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Board {
    private readonly int m_boardSize = 3;
    private readonly List<List<BoardCellColor>> m_board = new List<List<BoardCellColor>>();

    public Board(int boardSize) {
        m_boardSize = boardSize;
        Reset();
    }

    private void Reset() {
        m_board.Clear();
        for (var i = 0; i < m_boardSize; ++i) {
            var row = new List<BoardCellColor>();
            m_board.Add(row);
            for (var j = 0; j < m_boardSize; ++j) {
                row.Add(BoardCellColor.Empty);
            }
        }
    }

    public int GetBoardSize() {
        return m_boardSize;
    }
    
    private bool IsValidMove(GridCoordinate coordinates) {
        var isOnBoard = IsOnGrid(coordinates);
        var isFieldEmpty = GetCellColor(coordinates) == BoardCellColor.Empty;
        return isOnBoard && isFieldEmpty;
    }

    private bool IsOnGrid(GridCoordinate coordinates) {
        var isInRowRange = coordinates.GetRow() >= 0 && coordinates.GetRow() < m_boardSize;
        var isInColumnRange = coordinates.GetColumn() >= 0 && coordinates.GetColumn() < m_boardSize;
        return isInRowRange && isInColumnRange;
    }

    public BoardCellColor GetCellColor(GridCoordinate coordinates) {
        return m_board[coordinates.GetRow()][coordinates.GetColumn()];
    }

    private void SetCellColor(GridCoordinate coordinates, BoardCellColor color) {
        m_board[coordinates.GetRow()][coordinates.GetColumn()] = color;
    }
    
    public void DoMove(MoveData move, BoardCellColor color) {
        if (!move.startCoordinate.m_isOnGrid) {
            var newCoordinates = move.endCoordinate.m_gridCoordinate;
            Assert.IsTrue(move.endCoordinate.m_isOnGrid, "Shoudl be on the Grid.");
            Assert.IsTrue(IsValidMove(newCoordinates), "Move is not valid.");
            SetCellColor(newCoordinates, color);
        }
        else {
            var startGridCoordinate = move.startCoordinate.m_gridCoordinate;
            var endGridCoordinate = move.endCoordinate.m_gridCoordinate;
            Assert.IsTrue(IsColorExistInCoordinate(move.startCoordinate, color), "Previous coordinate is not valid.");
            SetCellColor(startGridCoordinate, BoardCellColor.Empty);
            SetCellColor(endGridCoordinate, color);
        }
    }
    
    private bool IsColorExistInCoordinate(BoardCoordinate boardCoordinate, BoardCellColor color) {
        var gridCoordinate = boardCoordinate.m_gridCoordinate;
        if (!boardCoordinate.m_isOnGrid) {
            return true;
        }

        var isOnGrid = IsOnGrid(gridCoordinate);
        var isColorCorrect = m_board[gridCoordinate.GetRow()][gridCoordinate.GetColumn()] == color;
        return isOnGrid && isColorCorrect;
    }
}

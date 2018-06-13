using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;
using UnityEngine.Assertions;

public class Board {
    private readonly int m_boardSize = 3;
    private readonly List<List<BoardCellColor>> m_board = new List<List<BoardCellColor>>();
    private readonly MoveValidator m_moveValidator = null;
    
    public Board(int boardSize) {
        m_boardSize = boardSize;
        Reset();
        m_moveValidator = new MoveValidator(this);
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

    public BoardCellColor GetCellColor(GridCoordinate coordinates) {
        return m_board[coordinates.GetRow()][coordinates.GetColumn()];
    }

    private void SetCellColor(GridCoordinate coordinates, BoardCellColor color) {
        m_board[coordinates.GetRow()][coordinates.GetColumn()] = color;
    }
    
    public void DoMove(MoveData move) {
        m_moveValidator.Validate(move);

        if (move.m_startCoordinate.m_isOnGrid) {
            var startGridCoordinate = move.m_startCoordinate.m_gridCoordinate;
            SetCellColor(startGridCoordinate, BoardCellColor.Empty);
        }
        
        var color = move.GetMoveColor();
        var endGridCoordinate = move.m_endCoordinate.m_gridCoordinate;
        SetCellColor(endGridCoordinate, color);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WinDetector {
    private Board m_board = null;
    
    public WinDetector(Board board) {
        m_board = board;
    }

    public void DoMove(MoveData move) {
        m_board.DoMove(move);
    }
    
    public bool IsWinDetected(BoardCellColor playerColor) {
        return IsWinByRow(playerColor) || IsWinByColumn(playerColor) ||
               IsWinByMainDiagonal(playerColor) || IsWinBySecondaryDiagonal(playerColor);
    }
    
    private bool IsWinByRow(BoardCellColor playerColor) {
        var boardSize = m_board.GetBoardSize();
        for (var row = 0; row < boardSize; ++row) {
            var path = new List<GridCoordinate>();
            for (var col = 0; col < boardSize; ++col) {
                path.Add(new GridCoordinate(row, col));
            }

            if (IsPathWinning(path, playerColor)) {
                return true;
            }
        }

        return false;
    }

    private bool IsPathWinning(List<GridCoordinate> path, BoardCellColor playerColor) {
        var matchCounter = 0;
        foreach (var cell in path) {
            if (playerColor == m_board.GetCellColor(cell)) {
                ++matchCounter;
            }            
        }

        return matchCounter == m_board.GetBoardSize();
    }

    private bool IsWinByColumn(BoardCellColor playerColor) {
        var boardSize = m_board.GetBoardSize();
        for (var col = 0; col < boardSize; ++col) {
            var path = new List<GridCoordinate>();
            for (var row = 0; row < boardSize; ++row) {
                path.Add(new GridCoordinate(row, col));
            }

            if (IsPathWinning(path, playerColor)) {
                return true;
            }
        }

        return false;
    }

    private bool IsWinByMainDiagonal(BoardCellColor playerColor) {
        var path = new List<GridCoordinate>();
        for (var i = 0; i < m_board.GetBoardSize(); ++i) {
            path.Add(new GridCoordinate(i, i));
        }

        return IsPathWinning(path, playerColor);
    }

    private bool IsWinBySecondaryDiagonal(BoardCellColor playerColor) {
        var boardSize = m_board.GetBoardSize();
        var path = new List<GridCoordinate>();
        for (var i = 0; i < boardSize; ++i) {
            var row = i;
            var col = boardSize - i - 1;
            path.Add(new GridCoordinate(row, col));
        }

        return IsPathWinning(path, playerColor);
    }
}
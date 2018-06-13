
using System;

public class MoveValidator {
    private readonly Board m_board = null;
    
    public MoveValidator(Board board) {
        m_board = board;
    }

    public void Validate(MoveData move) {
        ValidateStartCoordinate(move);
        ValidateEndCoordinate(move);
    }

    private void ValidateStartCoordinate(MoveData move) {
        if (!move.IsStartOnGrid()) {
            return;
        }

        var startCoordinate = move.m_startCoordinate;
        var color = move.GetMoveColor();
        if (startCoordinate.IsOnGrid()) {
            var gridCoordinate = startCoordinate.m_gridCoordinate;
            ValidateGridIndexRange(gridCoordinate);
            ValidateIfStartColorMatches(color, gridCoordinate);
        }
    }

    private void ValidateIfStartColorMatches(BoardCellColor color, GridCoordinate gridCoordinate) {
        var isColorCorrect = m_board.GetCellColor(gridCoordinate) == color;
        if (!isColorCorrect) {
            throw new Exception("Start coordinate and start color don't match.");
        }
    }

    private void ValidateGridIndexRange(GridCoordinate coordinates) {
        var size = m_board.GetBoardSize();
        var isInRowRange = coordinates.GetRow() >= 0 && coordinates.GetRow() < size;
        var isInColumnRange = coordinates.GetColumn() >= 0 && coordinates.GetColumn() < size;
        if (!isInRowRange || !isInColumnRange) {
            throw new Exception("Out of Grid range.");
        }
    }
        
    private void ValidateEndCoordinate(MoveData move) {
        if (!move.IsEndOnGrid()) {
            throw new Exception("Move end coordinate should be on the Grid.");
        }

        var endGridCoordinate = move.m_endCoordinate.m_gridCoordinate;
        ValidateGridIndexRange(endGridCoordinate);
        ValidateIfFieldIsEmpty(endGridCoordinate);
    }
    
    private void ValidateIfFieldIsEmpty(GridCoordinate coordinates) {
        var isFieldEmpty = m_board.GetCellColor(coordinates) == BoardCellColor.Empty;
        if (!isFieldEmpty) {
            throw new Exception("Making a move to a non-empty field.");
        }
    }
}

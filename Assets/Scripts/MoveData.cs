public class GridCoordinate {
    private int m_column;
    private int m_row;

    public GridCoordinate(int row = 0, int column = 0) {
        m_row = row;
        m_column = column;
    }

    public void SetRow(int row) {
        m_row = row;
    }
    
    public void SetColumn(int column) {
        m_column = column;
    }

    public int GetRow() {
        return m_row;
    }

    public int GetColumn() {
        return m_column;
    }
}

public class BoardCoordinate {
    public GridCoordinate m_gridCoordinate = new GridCoordinate(-1, -1);
    public bool m_isOnGrid;

    public BoardCoordinate(bool isOnGrid, GridCoordinate gridCoordinate) {
        m_isOnGrid = isOnGrid;
        m_gridCoordinate = gridCoordinate;
    }
}

public class MoveData {
    public BoardCellColor color = BoardCellColor.Empty;
    public BoardCoordinate endCoordinate = new BoardCoordinate(false, new GridCoordinate(-1, -1));
    public BoardCoordinate startCoordinate = new BoardCoordinate(false, new GridCoordinate(-1, -1));

    public void SetupFirstMoveToGrid(GridCoordinate gridCoordinate) {
        startCoordinate.m_isOnGrid = false;
        endCoordinate.m_isOnGrid = true;
        endCoordinate.m_gridCoordinate = gridCoordinate;
    }

    public bool IsChangingLocation() {
        if (!startCoordinate.m_isOnGrid) {
            return true;
        }

        var start = startCoordinate.m_gridCoordinate;
        var end = endCoordinate.m_gridCoordinate;
        return start.GetRow() != end.GetRow() || start.GetColumn() != end.GetColumn();
    }
}
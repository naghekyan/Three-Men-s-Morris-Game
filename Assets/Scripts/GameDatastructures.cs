
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

    public bool IsOnGrid() {
        return m_isOnGrid;
    }
}

public class MoveData {
    private BoardCellColor m_color = BoardCellColor.Empty;
    public BoardCoordinate m_endCoordinate = new BoardCoordinate(false, new GridCoordinate(-1, -1));
    public BoardCoordinate m_startCoordinate = new BoardCoordinate(false, new GridCoordinate(-1, -1));

    public MoveData() {
    }

    public MoveData(BoardCellColor color, BoardCoordinate start, BoardCoordinate end) {
        m_color = color;
        m_startCoordinate = start;
        m_endCoordinate = end;
    }

    public BoardCellColor GetMoveColor() {
        return m_color;
    } 
    
    public void SetupFirstMoveToGrid(GridCoordinate gridCoordinate, BoardCellColor color) {
        m_color = color;
        m_startCoordinate.m_isOnGrid = false;
        m_endCoordinate.m_isOnGrid = true;
        m_endCoordinate.m_gridCoordinate = gridCoordinate;
    }

    public bool IsChangingLocation() {
        if (!m_startCoordinate.m_isOnGrid) {
            return true;
        }

        var start = m_startCoordinate.m_gridCoordinate;
        var end = m_endCoordinate.m_gridCoordinate;
        return start.GetRow() != end.GetRow() || start.GetColumn() != end.GetColumn();
    }

    public bool IsStartOnGrid() {
        return m_startCoordinate.IsOnGrid();
    }

    public bool IsEndOnGrid() {
        return m_endCoordinate.IsOnGrid();
    }
}
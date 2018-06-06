using UnityEngine;

public class GridPlaceholder {
    private readonly GridCoordinate m_coordinate = new GridCoordinate();
    private readonly Vector3 m_position;

    public GridPlaceholder(GameObject placholderGameObject) {
        m_position = placholderGameObject.transform.position;
        PopulateIndexes(placholderGameObject);
    }

    private void PopulateIndexes(GameObject placholderGameObject) {
        var name = placholderGameObject.name;
        var size = name.Length;
        m_coordinate.m_row = int.Parse(name[size - 2].ToString());
        m_coordinate.m_column = int.Parse(name[size - 1].ToString());
    }

    public GridCoordinate GetGridCoordinates() {
        return m_coordinate;
    }

    public Vector3 GetWorldPosition() {
        return m_position;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlaceholder
{
    private GridCoordinate m_coordinate;
    private Vector3 m_position;

    public GridPlaceholder(GameObject placholderGameObject) {
        m_position = placholderGameObject.transform.position;
        PopulateIndexes(placholderGameObject);
    }

    private void PopulateIndexes(GameObject placholderGameObject) {
        string name = placholderGameObject.name;
        int size = name.Length;
        m_coordinate.row = int.Parse(name[size - 2].ToString());
        m_coordinate.column = int.Parse(name[size - 1].ToString());
    }

    public GridCoordinate GetGridCoordinates() {
        return m_coordinate;
    }

    public Vector3 GetWorldPosition() {
        return m_position;
    }
};
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GridPlaceholder
{
    private int m_column;
    private int m_row;
    private Vector3 m_position;

    public GridPlaceholder(GameObject placholderGameObject) {
        m_position = placholderGameObject.transform.position;
        PopulateIndexes(placholderGameObject);
    }

    private void PopulateIndexes(GameObject placholderGameObject) {
        string name = placholderGameObject.name;
        int size = name.Length;
        m_row = int.Parse(name[size - 2].ToString());
        m_column = int.Parse(name[size - 1].ToString());
    }

    public int GetColumn() {
        return m_column;
    }

    public int GetRow() {
        return m_row;
    }

    public Vector3 GetWorldPosition() {
        return m_position;
    }
};
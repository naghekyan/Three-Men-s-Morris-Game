using System;
using System.Collections.Generic;
using UnityEngine;

public class GridPositionProvider {
    private readonly List<CombinedPosition> m_gridPositions = new List<CombinedPosition>();
    private readonly float m_snapRadius = 1.7f;
    private int m_lastMatchedPivotIndex = 0;

    public GridPositionProvider(GameObject[] listOfHoles, float snapRadius) {
        foreach (var hole in listOfHoles) {
            m_gridPositions.Add(new CombinedPosition(hole));
        }

        m_snapRadius = snapRadius;
    }

    public CombinedPosition GetCombinedPosition(Vector3 position) {
        if (IsPivotInRangeExist(position)) {
            return m_gridPositions[m_lastMatchedPivotIndex];
        }
        else {
            throw new Exception("No CombinedPosition is available for `position = `" + position.ToString());
        }
    }
    
    public bool IsPivotInRangeExist(Vector3 position) {
        if (IsLastMatchedPivotMatching(position)) {
            return true;
        }

        var isPivotInRangeExist = GetIndexOfMatchingPivot(position) != -1;
        return isPivotInRangeExist;
    }
    
    private bool IsLastMatchedPivotMatching(Vector3 position) {
        var pivotPosition = GetPivotPositionByIndex(m_lastMatchedPivotIndex);
        return IsPositionInPivotRange(position, pivotPosition);
    }

    private Vector3 GetPivotPositionByIndex(int index) {
        var latestReturnedCombinedPosition = m_gridPositions[index];
        var positionOnGrid = latestReturnedCombinedPosition.GetWorldPosition();
        return positionOnGrid;
    }
    
    private bool IsPositionInPivotRange(Vector3 position, Vector3 pivot) {
        var radiusSqr = m_snapRadius * m_snapRadius;
        var diffVector = position - pivot;
        var distanceSqr = diffVector.sqrMagnitude;
        return distanceSqr < radiusSqr;
    }
    
    private int GetIndexOfMatchingPivot(Vector3 position) {
        var size = m_gridPositions.Count;
        for (var i = 0; i < size; ++i) {
            var pivotPosition = GetPivotPositionByIndex(i);
            if (IsPositionInPivotRange(position, pivotPosition)) {
                m_lastMatchedPivotIndex = i;
                return i;
            }
        }

        return -1;
    }
}
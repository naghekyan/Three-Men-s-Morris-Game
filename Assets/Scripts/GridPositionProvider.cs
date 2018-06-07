using System.Collections.Generic;
using UnityEngine;

public class GridPositionProvider {
    private readonly List<CombinedPosition> m_gridCombinedPositions = new List<CombinedPosition>();
    private readonly RangeQueryCache m_rangeQueryCache = new RangeQueryCache();
    private readonly float m_snapRadius = 1.7f;

    public GridPositionProvider(GameObject[] listOfHoles, float snapRadius) {
        foreach (var hole in listOfHoles) {
            m_gridCombinedPositions.Add(new CombinedPosition(hole));
        }

        m_snapRadius = snapRadius;
    }

    public bool IsValidPositionInRangeExists(Vector3 position) {
        var snapRange = new Range(position, m_snapRadius);
        if (m_rangeQueryCache.IsEqualToChachedRange(snapRange)) {
            return true;
        }

        var radiusSqr = m_snapRadius * m_snapRadius;
        foreach (var gridCombinedPosition in m_gridCombinedPositions) {
            var positionOnGrid = gridCombinedPosition.GetWorldPosition();
            var diffVector = position - positionOnGrid;
            var distanceSqr = diffVector.sqrMagnitude;
            
            if (distanceSqr < radiusSqr) {
                m_rangeQueryCache.SetCombinedPositionForRange(snapRange, gridCombinedPosition);
                return true;
            }
        }

        return false;
    }

    public CombinedPosition GetGridCombinedPosition(Vector3 position) {
        var snapRange = new Range(position, m_snapRadius);
        if (m_rangeQueryCache.IsEqualToChachedRange(snapRange)) {
            return m_rangeQueryCache.GetCombinedPosition();
        }

        var radiusSqr = m_snapRadius * m_snapRadius;
        CombinedPosition result = null;
        foreach (var gridCombinedPosition in m_gridCombinedPositions) {
            var positionOnGrid = gridCombinedPosition.GetWorldPosition();
            var diffVector = position - positionOnGrid;
            var distanceSqr = diffVector.sqrMagnitude;

            if (distanceSqr < radiusSqr) {
                m_rangeQueryCache.SetCombinedPositionForRange(snapRange, gridCombinedPosition);
                result = gridCombinedPosition;
            }
        }

        return result;
    }

    private class RangeQueryCache {
        private readonly float m_epsilon = float.Epsilon;
        private Range m_range;
        private CombinedPosition m_combinedPosition;

        public void SetCombinedPositionForRange(Range range, CombinedPosition combinedPosition) {
            m_range = range;
            m_combinedPosition = combinedPosition;
        }

        public CombinedPosition GetCombinedPosition() {
            return m_combinedPosition;
        }

        public bool IsEqualToChachedRange(Range other) {
            var radius = m_range.radius;
            var center = m_range.center;
            var otherRadius = other.radius;
            var otherCenter = other.center;
            return Mathf.Abs(otherRadius - radius) < m_epsilon
                   && Mathf.Abs(otherCenter.x - center.x) < m_epsilon
                   && Mathf.Abs(otherCenter.y - center.y) < m_epsilon
                   && Mathf.Abs(otherCenter.z - center.z) < m_epsilon;
        }
    }
}